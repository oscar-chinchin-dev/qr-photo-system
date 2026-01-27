using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiWebAPIFotos.Data;
using MiWebAPIFotos.DTOs.Visitas;
using MiWebAPIFotos.Entities;



namespace MiWebAPIFotos.Controllers

{
    /// <summary>
    /// Gestiona el ciclo de vida de una visita, desde su creación inicial 
    /// hasta la recuperación de contenido multimedia mediante identificadores QR.
    /// </summary>
    

    [ApiController]
    [Route("api/[controller]")]
    public class VisitasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VisitasController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Recupera toda la información de una visita y su galería fotográfica asociada 
        /// utilizando el código QR como llave de acceso pública.
        /// </summary>
        /// <param name="qr">El identificador único impreso o generado para el cliente.</param>
        

        // GET api/visitas/{qr}
        [HttpGet("{qr}")]
        public async Task<ActionResult<VisitaDto>> GetByQr(string qr)
        {

            // Realizamos un Eager Loading de las fotos para evitar el problema de N+1 consultas
            // y obtener toda la galería en un solo viaje a la base de datos.

            var visita = await _context.Visitas
                .Include(v => v.Fotos)
                .FirstOrDefaultAsync(v => v.QrCode == qr);

            if (visita == null)
                return NotFound();


            // Proyectamos la entidad a un DTO estructurado para la vista de galería,
            // transformando los binarios de imagen en recursos listos para el consumo del frontend.

            var dto = new VisitaDto
            {
                Id = visita.Id,
                QrCode = visita.QrCode,
                Fecha = visita.Fecha,
                Fotos = visita.Fotos.Select(f => new FotoDto
                {
                    Id = f.Id,
                    Pagada = f.Pagada,
                    Favorita = f.Favorita,
                    // Conversión on-the-fly para previsualización inmediata sin endpoints de imagen extra.
                    ImagenBase64 = f.Imagen != null
        ? $"data:image/jpeg;base64,{Convert.ToBase64String(f.Imagen)}"
        : null
                }).ToList()
            };

            return Ok(dto);
        }


        /// <summary>
        /// Registra la apertura de una nueva sesión o visita en el sistema.
        /// </summary>
        /// <remarks>
        /// Se utiliza principalmente para dar de alta el QR antes de empezar a asociarle fotografías.
        /// </remarks>

        // POST api/visitas
        [HttpPost]
        public async Task<ActionResult> Create(CreateVisitaDto dto)
        {
            var visita = new Visita
            {
                QrCode = dto.QrCode,
                // Estandarizamos a UTC para evitar conflictos de zona horaria entre el servidor y los clientes.
                Fecha = DateTime.UtcNow


            };

            _context.Visitas.Add(visita);
            await _context.SaveChangesAsync();

            return Ok();
        }

       

    }
}
