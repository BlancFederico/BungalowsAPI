using BungalowsAPI.Models;
using BungalowsAPI.Services.Contrato;
using BungalowsAPI.Services.Implementacion;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BungalowsAPI.DTOs;
using BungalowsAPI.Utilidades;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<DbbungalowContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL"));
});

builder.Services.AddScoped<ITipoReservaService, TipoReservaService>();
builder.Services.AddScoped<IReservaService, ReservaService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
   {
       app.AllowAnyOrigin()
       .AllowAnyHeader()
       .AllowAnyMethod();
   });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region PETICIONES API REST
app.MapGet("/tiporeserva/lista", async (
    ITipoReservaService _tiporeservaServicio,
    IMapper _mapper
    ) =>
{
    var listaTipoReserva = await _tiporeservaServicio.GetList();
    var listaTipoReservaDTO = _mapper.Map<List<TipoReservaDTO>>(listaTipoReserva);

    if (listaTipoReservaDTO.Count > 0)
        return Results.Ok(listaTipoReservaDTO);
    else
        return Results.NotFound();

});

app.MapGet("/reserva/lista", async (
    IReservaService _reservaServicio,
    IMapper _mapper
    ) =>
{
    var listaReserva = await _reservaServicio.GetList();
    var listaReservaDTO = _mapper.Map<List<ReservaDTO>>(listaReserva);

    if (listaReservaDTO.Count > 0)
        return Results.Ok(listaReservaDTO);
    else
        return Results.NotFound();

});

app.MapPost("/reserva/guardar", async (
    ReservaDTO modelo,
    IReservaService _reservaServicio,
    IMapper _mapper
    ) => {
        var _reserva = _mapper.Map<Reserva>(modelo);
        var _reservaCreada = await _reservaServicio.Add(_reserva);

        if (_reservaCreada.IdReserva != 0)
            return Results.Ok(_mapper.Map <ReservaDTO>(_reservaCreada));
        else
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
    });

app.MapPut("/reserva/actualizar/{idReserva}", async (
    int idReserva,
    ReservaDTO modelo,
    IReservaService _reservaServicio,
    IMapper _mapper
    ) => { 
        var _encontrado = await _reservaServicio.Get(idReserva);

        if(_encontrado is null) return Results.NotFound();

        var _reserva = _mapper.Map<Reserva>(modelo);

        _encontrado.NombreCompleto = _reserva.NombreCompleto;
        _encontrado.IdTipoReserva = _reserva.IdTipoReserva;
        _encontrado.Costo = _reserva.Costo;
        _encontrado.FechaInicio = _reserva.FechaInicio;
        _encontrado.FechaSalida = _reserva.FechaSalida;

        var respuesta = await _reservaServicio.Update(_encontrado);
        
        if(respuesta)
            return Results.Ok(_mapper.Map<ReservaDTO>(_encontrado));
        else
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
    });


app.MapDelete("/reserva/eliminar/{idReserva}", async(
    int idReserva,
    IReservaService _reservaServicio
    ) => {

        var _encontrado = await _reservaServicio.Get(idReserva);

        if (_encontrado is null) return Results.NotFound();

        var respuesta = await _reservaServicio.Delete(_encontrado);

        if (respuesta)
            return Results.Ok();
        else
            return Results.StatusCode(StatusCodes.Status500InternalServerError);

    });

#endregion


app.UseCors("NuevaPolitica");
app.Run();

