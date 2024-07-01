using Acadenode.Core.Repositories;
using Acadenote.Server;
using Acadenote.Server.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AcadenoteDbContext>(o => o.UseSqlite("Data Source=acadenote.db"));
builder.Services.AddScoped<INoteRepository, NoteRepository>();
var app = builder.Build();


app.Urls.Add("http://*:80");


app.UseAuthorization();

app.MapControllers();


app.Run();