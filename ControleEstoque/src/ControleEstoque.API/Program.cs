using ControleEstoque.API.Data;
using ControleEstoque.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var passwordService = scope.ServiceProvider.GetRequiredService<IPasswordService>();

    if(!context.Gerentes.Any())
    {
        var admin = new ControleEstoque.API.Models.Gerente
        {
            Nome = "Administrador",
            Email = "admin@mail.com",
            Setor = "TI",
            Perfil = ControleEstoque.API.Models.PerfilUsuario.Gerente,
            SenhaHash = passwordService.HashPassword("admin123")
        };
        context.Gerentes.Add(admin);
        context.SaveChanges();
    }
}

app.Run();
