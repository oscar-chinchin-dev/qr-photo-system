namespace MiWebAPIFotos.DTOs.Selecciones
{
    ///CrearSeleccionDto: "De todas las fotos que me diste en la VisitaDto, quiero estas en particular".
    /// <summary>
    /// Estructura de entrada para formalizar la selección de fotos.
    /// Define qué imágenes de una sesión específica pasan al estado de "procesadas" o "pagadas".
    /// </summary>
    public class CrearSeleccionDto
    {
        // Vincula la selección a una sesión de fotografía específica.
        public int VisitaId { get; set; }

        // Listado de recursos que el usuario ha decidido elegir/adquirir.
        // Se inicializa vacío para evitar referencias nulas en el procesamiento.
        public List<int> FotoIds { get; set; } = [];
    }
}
