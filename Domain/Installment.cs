using ControleContas.Domain;
using System.Text.Json.Serialization;

namespace ControleContasData.Domain
{
	public class Installment
	{
        public int Id { get; set; }
        public int? AccountId { get; set; }
        public int NumeberInstalment { get; set; }
        [JsonIgnore]
        public Account Account { get; set; }
        public Decimal Price { get; set; }
        public DateTime Due { get; set; }
		public bool IsPaid { get; set; }
		public int? BankSlipId { get; set; }
        
	}
}
