using BungalowsAPI.Models;

namespace BungalowsAPI.Services.Contrato
{
    public interface ITipoReservaService
    {
        Task<List<TipoReserva>> GetList();
    }
}
