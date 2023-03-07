namespace BungalowsAPI.DTOs
{
    public class ReservaDTO
    {

        public int IdReserva { get; set; }
        public string? NombreCompleto { get; set; }
        public int? IdTipoReserva { get; set; }
        public string? NombreTipoReserva { get; set; }
        public int? Costo { get; set; }
        public string? FechaInicio { get; set; }
        public string? FechaSalida { get; set; }
    }
}
