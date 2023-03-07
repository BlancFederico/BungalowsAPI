namespace BungalowsAPI.Models;

public partial class TipoReserva
{
    public int IdTipoReserva { get; set; }

    public string? Nombre { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<Reserva> Reservas { get; } = new List<Reserva>();
}
