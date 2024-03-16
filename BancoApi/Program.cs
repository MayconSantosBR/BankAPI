using BancoApi.Models;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

var app = builder.Build();

#region Setup Accounts
var cache = app.Services.GetRequiredService<IMemoryCache>();
cache.Set("Accounts", new List<Account> {
    new() {
        id = 1,
        nome = "Silvio Santos",
        valor_na_conta = 10000,
        transacoes = new()
        {
            new()
            {
                valor = 1500,
                tipo = "d",
                descricao = "Uma transacao"
            }
        }
    },
    new()
    {
        id = 2,
        nome = "Fausto Silva",
        valor_na_conta = 10,
        transacoes = new()
        {
            new()
            {
                valor = 1500,
                tipo = "c",
                descricao = "Uma transacao"
            },
            new()
            {
                valor = 32,
                tipo = "d",
                descricao = "Uma transacao"
            }
        }
    },
    new()
    {
        id = 3,
        nome = "Gugu liberato",
        transacoes = []
    }
});
#endregion

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
