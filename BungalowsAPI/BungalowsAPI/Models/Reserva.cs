namespace BungalowsAPI.Models;

public partial class Reserva
{
    public int IdReserva { get; set; }

    public string? NombreCompleto { get; set; }

    public int? IdTipoReserva { get; set; }

    public int? Costo { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaSalida { get; set; }

    public DateTime? FechaCreacion { get; set; }    

    public virtual TipoReserva? IdTipoReservaNavigation { get; set; }
}
