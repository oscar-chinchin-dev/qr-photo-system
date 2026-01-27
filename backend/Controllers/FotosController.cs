using Microsoft.AspNetCore.Mvc;
using MiWebAPIFotos.Data;
using MiWebAPIFotos.DTOs.Visitas;

namespace MiWebAPIFotos.Controllers
{
    /// Controlador responsable de la recuperación y 
    /// exposición de recursos fotográficos del sistema.

    [ApiController]
    [Route("api/[controller]")]
    public class FotosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FotosController(AppDbContext context)
        {
            _context = context;
        }


        /// Obtiene una fotografía específica procesada 
        /// para su visualización directa en el cliente.

        /// <param name="id">Identificador único de 
        /// la fotografía.</param>
        /// <returns>Un DTO con el estado de pago
        /// y la imagen en formato Data URI.</returns>

        [HttpGet("{id}")]
        public IActionResult GetFoto(int id)
        {
            // Se busca el registro original en la base de datos
            var foto = _context.Fotos.FirstOrDefault(f => f.Id == id);

            if (foto == null)
                return NotFound();

            // Transformamos la entidad de persistencia
            // a un objeto de transferencia (DTO) 
            // para evitar exponer la estructura
            // interna de la base de datos.

            var dto = new FotoDto
            {
                Id = foto.Id,
                Pagada = foto.Pagada,
                // Convertimos el flujo binario a Base64
                // con el prefijo MIME correspondiente 
                // para que el navegador pueda renderizar
                // la imagen sin peticiones adicionales.
                ImagenBase64 = foto.Imagen != null
                    ? $"data:image/jpeg;base64,{Convert.ToBase64String(foto.Imagen)}"
                    : null
            };

            return Ok(dto);
        }
    }
}
