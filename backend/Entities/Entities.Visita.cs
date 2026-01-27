namespace MiWebAPIFotos.Entities
{

    /// Representa una sesión de fotografía iniciada por el escaneo de un QR.
    /// 
    /// Reglas:
    /// - Una Visita puede tener múltiples Fotos.
    /// - El QR es único y actúa como identificador público.
    /// - Las selecciones siempre se asocian a una Visita existente.
    public class Visita
    {
        public int Id { get; set; }
        public string QrCode { get; set; } = null!;
        public DateTime Fecha { get; set; }

        // Navegación
        public ICollection<Foto> Fotos { get; set; } = new List<Foto>();
        public ICollection<Seleccion> Selecciones { get; set; } = new List<Seleccion>();
    }
}
