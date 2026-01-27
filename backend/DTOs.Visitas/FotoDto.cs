namespace MiWebAPIFotos.DTOs.Visitas
{
    ///FotoDto: "Soy el elemento individual que el usuario ve y decide si le gusta o no".
    /// <summary>
    /// Representación visual de una fotografía optimizada para el consumo del cliente.
    /// Transforma datos binarios de almacenamiento en recursos renderizables. Representación visual de una fotografía optimizada para el frontend.
    /// </summary>
    public class FotoDto
    {
        public int Id { get; set; }

        // Indica si el recurso ya ha sido liberado para su descarga o visualización completa.
        public bool Pagada { get; set; }

        // Marca de preferencia del usuario para filtrado en la galería.
        public bool Favorita { get; set; }

        // Contenido de la imagen formateado como Data URI para visualización directa 
        // en navegadores sin peticiones adicionales al servidor de archivos.
        public string? ImagenBase64 { get; set; }
    }
}
