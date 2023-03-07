using AutoMapper;
using BungalowsAPI.DTOs;
using BungalowsAPI.Models;
using System.Globalization;


namespace BungalowsAPI.Utilidades
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region TipoReserva
            CreateMap<TipoReserva, TipoReservaDTO>().ReverseMap();

            #endregion

            #region Reserva
            CreateMap<Reserva, ReservaDTO>()
                .ForMember(destino =>
                destino.NombreTipoReserva,
                opt => opt.MapFrom(origen => origen.IdTipoReservaNavigation.Nombre)
                )
                 .ForMember(destino =>
                destino.FechaInicio,
                opt => opt.MapFrom(origen => origen.FechaInicio.Value.ToString("dd/MM/yyyy"))
                )
                 .ForMember(destino =>
                destino.FechaSalida,
                opt => opt.MapFrom(origen => origen.FechaSalida.Value.ToString("dd/MM/yyyy"))
                );

            CreateMap<ReservaDTO, Reserva>()
                .ForMember(destino =>
                destino.IdTipoReservaNavigation,
                opt => opt.Ignore()
                )
                .ForMember(destino =>
                destino.FechaInicio,
                opt => opt.MapFrom(origen => DateTime.ParseExact(origen.FechaInicio, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                )
                .ForMember(destino =>
                destino.FechaSalida,
                opt => opt.MapFrom(origen => DateTime.ParseExact(origen.FechaSalida, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                );


            #endregion
        }
    }
}
