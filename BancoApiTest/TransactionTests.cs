using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;

namespace BancoApiTest
{
    public class TransactionTests
    {
        private HttpClient client = new HttpClient() { BaseAddress = new Uri("https://localhost:7093/api") };

        [Fact]
        public async Task GetAccount()
        {
            var account = await client.GetAsync("/clientes/1");

            Assert.Equal(HttpStatusCode.OK, account.StatusCode);
            Assert.Equal(1, JToken.Parse(await account.Content.ReadAsStringAsync())["id"]);
        }

        [Fact]
        public async Task CreateNewTransaction()
        {
            var transaction = JsonConvert.SerializeObject(new
            {
                valor = 100,
                tipo = "d",
                descricao = "teste"
            });

            var response = await client.PostAsync("clientes/1/transacoes", new StringContent(transaction, Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(transaction, await response.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task GetTransactions()
        {
            var response = await client.GetAsync("clientes/1/transacoes");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(JToken.Parse(await response.Content.ReadAsStringAsync()).Count() > 0);
        }

        [Fact]
        public async Task GetTransaction()
        {
            var transactionJson = JsonConvert.SerializeObject(new
            {
                valor = 1500,
                tipo = "d",
                descricao = "Uma transacao"
            });

            var response = await client.GetAsync("clientes/1/transacoes/1");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(transactionJson, await response.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task UpdateTransaction()
        {
            var transactionJsonOld = JsonConvert.SerializeObject(new
            {
                valor = 1,
                tipo = "d",
                descricao = "Uma transacao"
            });

            var transactionJsonNew = JsonConvert.SerializeObject(new
            {
                valor = 2,
                tipo = "d",
                descricao = "Uma transacao"
            });

            var response = await client.PostAsync("clientes/1/transacoes", new StringContent(transactionJsonOld, Encoding.UTF8, "application/json"));
            response = await client.PutAsync("clientes/1/transacoes/1", new StringContent(transactionJsonNew, Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(transactionJsonNew, await response.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task DeleteTransaction()
        {
            var response = await client.DeleteAsync("clientes/1/transacoes/3");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}