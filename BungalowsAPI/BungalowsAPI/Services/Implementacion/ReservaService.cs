using BungalowsAPI.Models;
using BungalowsAPI.Services.Contrato;
using Microsoft.EntityFrameworkCore;

namespace BungalowsAPI.Services.Implementacion
{
    public class ReservaService : IReservaService
    {
        private DbbungalowContext _dbContext;

        public ReservaService(DbbungalowContext DbContext)
        {
            _dbContext = DbContext;
        }

        public async Task<List<Reserva>> GetList()
        {
            try
            {
                List<Reserva> lista = new List<Reserva>();
                lista = await _dbContext.Reservas.Include(dpt => dpt.IdTipoReservaNavigation).ToListAsync();

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            };
        }
        public async Task<Reserva> Get(int idReserva)
        {
            try
            {
                Reserva? encontrado = new Reserva();
                encontrado = await _dbContext.Reservas.Include(dpt => dpt.IdTipoReservaNavigation)
                    .Where(e => e.IdReserva == idReserva).FirstOrDefaultAsync();

                return encontrado;
            }
            catch (Exception ex)
            {
                throw ex;
            };
        }
        public async Task<Reserva> Add(Reserva modelo)
        {
            try
            {
                _dbContext.Reservas.Add(modelo);
                await _dbContext.SaveChangesAsync();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            };
        }
        public async Task<bool> Update(Reserva modelo)
        {
            try
            {
                _dbContext.Reservas.Update(modelo);
                await _dbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            };
        }
        public async Task<bool> Delete(Reserva modelo)
        {
            try
            {
                _dbContext.Reservas.Remove(modelo);
                await _dbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            };
        }
    }
}
