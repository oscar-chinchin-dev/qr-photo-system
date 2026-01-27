using Microsoft.AspNetCore.Mvc;
using MiWebAPIFotos.Data;
using MiWebAPIFotos.DTOs.Selecciones;
using MiWebAPIFotos.Entities;
using Microsoft.EntityFrameworkCore;

namespace MiWebAPIFotos.Controllers
{
    /// Gestiona el proceso de selección de fotografías por parte de los clientes
    /// y la actualización del estado comercial de las imágenes.
    [ApiController]
    [Route("api/selecciones")]
    public class SeleccionesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SeleccionesController(AppDbContext context)
        {
            _context = context;
        }
        
        /// Registra una nueva selección de fotos y marca las imágenes elegidas como pagadas.
        
        
        /// Este proceso es transaccional: vincula fotos a una visita y cambia su estado de disponibilidad/pago.
        
        [HttpPost]
        public async Task<IActionResult> CrearSeleccion(CrearSeleccionDto dto)
        {
            // Validamos que la visita exista y cargamos sus fotos relacionadas 
            // para poder modificar sus estados en memoria antes de persistir.
            var visita = await _context.Visitas
                .Include(v => v.Fotos)
                .FirstOrDefaultAsync(v => v.Id == dto.VisitaId);

            if (visita == null)
                return NotFound("Visita no encontrada");


            // Creamos el encabezado de la selección para generar el ID necesario 
            // en la relación de las fotos individuales.
            var seleccion = new Seleccion
            {
                Fecha = DateTime.Now,
                VisitaId = visita.Id
            };

            _context.Selecciones.Add(seleccion);
            await _context.SaveChangesAsync();

            // Procesamos el listado de fotos para establecer el vínculo Many-to-Many
            // y actualizar el estado de negocio (Pagada) de cada recurso.
            foreach (var fotoId in dto.FotoIds)
            {
                // Registramos la relación en la tabla intermedia
                _context.SeleccionFotos.Add(new SeleccionFoto
                {
                    SeleccionId = seleccion.Id,
                    FotoId = fotoId
                });

                // Buscamos la foto dentro de la colección de la visita para marcarla como procesada.
                // Esto asegura que solo se modifiquen fotos que pertenecen legalmente a esta visita.
                var foto = visita.Fotos.FirstOrDefault(f => f.Id == fotoId);
                if (foto != null)
                    foto.Pagada = true;
            }

            // Consolidamos todos los cambios (vínculos y estados de pago) en una sola operación de base de datos.
            await _context.SaveChangesAsync();

            return Ok(new { seleccion.Id });
        }
    }
}
