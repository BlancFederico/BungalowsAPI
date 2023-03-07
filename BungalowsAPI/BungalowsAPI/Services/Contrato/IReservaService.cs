using BungalowsAPI.Models;


namespace BungalowsAPI.Services.Contrato
{
    public interface IReservaService
    {
        Task<List<Reserva>> GetList();
        Task<Reserva> Get(int idReserva);
        Task<Reserva> Add(Reserva modelo);
        Task<bool> Update(Reserva modelo);
        Task<bool> Delete(Reserva modelo);
    }
}
