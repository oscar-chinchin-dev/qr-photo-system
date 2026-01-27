namespace MiWebAPIFotos.Entities
{

    /// Representa la selección realizada por el cliente sobre una visita.
    /// 
    /// Reglas:
    /// - Una Selección pertenece a una Visita.
    /// - Una Visita puede tener una o más selecciones (si aplica).
    /// - La creación de una Selección modifica el estado de las Fotos asociadas.
    public class Seleccion
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }

        // FK
        public int VisitaId { get; set; }
        public Visita Visita { get; set; } = null!;

        public ICollection<SeleccionFoto> SeleccionFotos { get; set; } = new List<SeleccionFoto>();
    }
}
