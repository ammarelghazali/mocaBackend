using MOCA.Core.Interfaces.MeetingSpaceReservations.Repositories;
using MOCA.Core.Interfaces.Shared.Services;
using MOCA.Presistence.Repositories.MeetingSpaceReservations;
using MOCA.Services.Implementation.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMeetingSpaceReservationRepository, MeetingSpaceReservationsRepository>();
builder.Services.AddScoped<IReservationsStatusService, ReservationsStatusService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
