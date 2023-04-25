using MySql.Data.MySqlClient;
using SmartAPI.Data;
using SmartAPI.Data.Repositories;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var mySQLCOnfiguration = new MySQLConfiguration(builder.Configuration.GetConnectionString("MySqlConnection"));
builder.Services.AddSingleton(mySQLCOnfiguration);
//builder.Services.AddSingleton(new MySqlConnection(builder.Configuration.GetConnectionString("MySqlConnection")))
builder.Services.AddScoped<IWorkerRepository, WorkerRepository>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseCors("NuevaPolitica");
app.UseAuthorization();
app.MapControllers();
app.Run();