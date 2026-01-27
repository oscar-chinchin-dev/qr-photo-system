namespace MiWebAPIFotos.Entities
{

    /// Entidad de relación entre Selección y Foto.
    /// 
    /// Reglas:
    /// - Una Foto no puede estar asociada dos veces a la misma Selección.
    /// - Permite desacoplar la selección del almacenamiento de la foto.
    public class SeleccionFoto
    {
        public int SeleccionId { get; set; }
        public Seleccion Seleccion { get; set; } = null!;

        public int FotoId { get; set; }
        public Foto Foto { get; set; } = null!;
    }
}
