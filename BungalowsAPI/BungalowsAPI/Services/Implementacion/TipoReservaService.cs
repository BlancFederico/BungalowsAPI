using BungalowsAPI.Models;
using BungalowsAPI.Services.Contrato;
using Microsoft.EntityFrameworkCore;

namespace BungalowsAPI.Services.Implementacion
{
    public class TipoReservaService : ITipoReservaService
    {
        private DbbungalowContext _dbContext;

        public TipoReservaService(DbbungalowContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TipoReserva>> GetList()
        {
            try
            {
                List<TipoReserva> lista = new List<TipoReserva>();
                lista = await _dbContext.TipoReservas.ToListAsync();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
