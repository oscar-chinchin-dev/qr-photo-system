namespace MiWebAPIFotos.DTOs.Visitas
{
    ///CreateVisitaDto: "Hola, alguien escaneó este QR, prepárame una sesión".
    
    /// <summary>
    /// Contrato mínimo para dar de alta una nueva interacción con un cliente.
    /// Solo requiere el identificador externo (QR) para iniciar el seguimiento.
    /// </summary>
    public class CreateVisitaDto
    {
        // El código único que vincula el objeto físico impreso con el registro digital.
        public string QrCode { get; set; } = null!;
    }
}
