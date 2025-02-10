using ControleContasData.Domain;
using Shared.Data.Domain;
using System.Text.Json.Serialization;

namespace ControleContas.Domain
{
	public class Account
	{
        public int Id { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }
        public DateTime Date { get; set; }
        public int? CardId { get; set; }
        public Card? Card { get; set; }
        public int UserId { get; set; }
		public User? User { get; set; }
        public ICollection<Installment>? Installments { get; set; } = [];
        public int InstallmentQuantity { get; set; }
        

	}
}
