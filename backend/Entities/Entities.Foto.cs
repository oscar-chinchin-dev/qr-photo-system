namespace MiWebAPIFotos.Entities
{

    /// Representa una fotografía generada durante una visita.
    /// 
    /// Reglas:
    /// - Una foto siempre pertenece a una Visita.
    /// - Una foto puede existir sin estar seleccionada.
    /// - Una foto no puede ser marcada como pagada más de una vez.
    /// - El estado Pagada controla el acceso al recurso completo.
    
    public class Foto
    {
        public int Id { get; set; }
        public bool Pagada { get; set; }
        public bool Favorita { get; set; }
        public byte[] Imagen { get; set; }


        // FK
        public int VisitaId { get; set; }
        public Visita Visita { get; set; } = null!;

        public ICollection<SeleccionFoto> SeleccionFotos { get; set; } = new List<SeleccionFoto>();
    }
}
