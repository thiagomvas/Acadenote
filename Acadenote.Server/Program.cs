using Acadenode.Core.Repositories;
using Acadenote.Server;
using Acadenote.Server.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(o =>
{
    o.AddDefaultPolicy(p =>
    {
        p.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AcadenoteDbContext>(o =>
{
    o.UseSqlite("Data Source=acadenote.db");
});
builder.Services.AddScoped<INoteRepository, NoteRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Acadenote API V1");
    c.RoutePrefix = string.Empty;
});


app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AcadenoteDbContext>();
    dbContext.Database.Migrate();
}

app.UseCors();

app.Run();
