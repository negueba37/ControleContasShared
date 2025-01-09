using System.Text.Json.Serialization;

namespace ControleContas.Domain
{
	public class Card
	{
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Limit { get; set; }
        public int MaturityDay { get; set; }
        public int BestPurchaseDay { get; set; }
        [JsonIgnore]
        public ICollection<Account>? Accounts { get; set; }
    }
}
