namespace MiWebAPIFotos.DTOs.Visitas
{

    ///VisitaDto: "Aquí tienes todo lo que hay en esta sesión: tus datos y tus fotos".
    /// <summary>
    /// Vista consolidada de la experiencia del cliente. 
    /// Agrupa la sesión (Visita) con su galería fotográfica completa.
    /// </summary>
    public class VisitaDto
    {
        public int Id { get; set; }

        // Identificador de acceso público utilizado por el cliente.
        public string QrCode { get; set; } = null!;

        // Momento en que se generó la interacción inicial.
        public DateTime Fecha { get; set; }

        // Colección de recursos asociados a esta sesión. 
        // Permite al frontend iterar y mostrar la galería completa en una sola carga.
        public List<FotoDto> Fotos { get; set; } = new();
    }
}
