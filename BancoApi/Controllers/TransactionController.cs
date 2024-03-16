using BancoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BancoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ILogger<TransactionController> logger;
        private readonly IMemoryCache cache;
        private List<Account> accounts;

        public TransactionController(ILogger<TransactionController> logger, IMemoryCache cache)
        {
            this.logger = logger;
            this.cache = cache;
            this.accounts = this.cache.Get<List<Account>>("Accounts") ?? throw new Exception("Não há contas cadastradas!");
        }

        [HttpGet("/clientes/{id}")]
        public ActionResult<Account> GetAccount([FromRoute] int id)
        {
            var account = this.accounts.FirstOrDefault(a => a.id == id);

            if (account == null)
                return NotFound();

            return Ok(account);
        }

        [HttpPost("/clientes/{id}/transacoes")]
        public ActionResult<Transaction> NewTransaction([FromRoute] int id, [FromBody] Transaction transaction)
        {
            try
            {
                var response = (OkObjectResult)GetAccount(id).Result;

                if(ResponseValidator(response, out Account account))
                    return NotFound();

                account.transacoes.Add(transaction);

                UpdateAccount(id, account);

                return Ok(transaction);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("/clientes/{id}/transacoes")]
        public ActionResult<List<Transaction>> GetTransactions([FromRoute] int id)
        {
            try
            {
                var response = (OkObjectResult)GetAccount(id).Result;

                if (ResponseValidator(response, out Account account))
                    return NotFound();

                return Ok(account.transacoes);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("/clientes/{id}/transacoes/{transacao_id}")]
        public ActionResult<Transaction> GetTransaction([FromRoute] int id, [FromRoute] int transacao_id)
        {
            try
            {
                transacao_id--;

                var response = (OkObjectResult)GetAccount(id).Result;

                if (ResponseValidator(response, out Account account))
                    return NotFound();

                var transaction = account.transacoes.ElementAt(transacao_id);

                if (transaction == null)
                    return NotFound();

                return Ok(transaction);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPut("/clientes/{id}/transacoes/{transacao_id}")]
        public ActionResult<Transaction> UpdateTransaction([FromRoute] int id, [FromRoute] int transacao_id, [FromBody] Transaction new_transaction)
        {
            var response = (OkObjectResult)GetAccount(id).Result;

            if (ResponseValidator(response, out Account account))
                return NotFound();

            response = (OkObjectResult)GetTransaction(id, transacao_id).Result;

            if (ResponseValidator(response, out Transaction transaction))
                return NotFound();

            account.transacoes.RemoveAll(t => t == transaction);
            account.transacoes.Add(new_transaction);
            
            UpdateAccount(id, account);

            return Ok(new_transaction);
        }

        [HttpDelete("/clientes/{id}/transacoes/{transacao_id}")]
        public ActionResult DeleteTransaction([FromRoute] int id, [FromRoute] int transacao_id)
        {
            var response = (OkObjectResult)GetAccount(id).Result;

            if (ResponseValidator(response, out Account account))
                return NotFound();

            response = (OkObjectResult)GetTransaction(id, transacao_id).Result;

            if (ResponseValidator(response, out Transaction transaction))
                return NotFound();

            account.transacoes.RemoveAll(t => t == transaction);

            UpdateAccount(id, account);

            return NoContent();
        }

        private bool ResponseValidator<T>(OkObjectResult? response, out T entity)
        {
            if (response.Value == null)
            {
                entity = default;
                return true;
            }

            entity = (T)response.Value;
            return false;
        }

        private void UpdateAccount(int id, Account account)
        {
            this.accounts.RemoveAll(a => a.id == id);
            this.accounts.Add(account);
            this.cache.Set("Accounts", this.accounts);
        }
    }
}
