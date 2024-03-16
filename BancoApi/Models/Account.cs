namespace BancoApi.Models
{
    public class Account
    {
        public int id { get; set; }
        public string nome { get; set; }
        public int valor_na_conta { get; set; }
        public List<Transaction> transacoes { get; set; }
    }
}
