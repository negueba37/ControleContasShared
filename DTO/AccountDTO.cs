	
using ConsoleData.DTO;
using ControleContas.Domain;
using ControleContasData.Domain;
using Shared.Data.Domain;
using System.Text.Json.Serialization;

namespace ControleContasData.DTO
{
    public class AccountDTO
	{
		public int Id { get; set; }
		public string Description { get; set; }
		public Decimal Price { get; set; }
		public DateTime Date { get; set; }
		public int CardId { get; set; }		
		public CardDTO Card { get; set; } = new CardDTO();
		public int UserId { get; set; }
		public UserDTO User { get; set; } = new UserDTO();
		public IEnumerable<InstallmentDTO> Installments { get; set; } = [];
		public int InstallmentQuantity { get; set; }
	}
}
