	
using ConsoleData.DTO;
using ControleContas.Domain;
using ControleContasData.Domain;
using Shared.Data.Domain;
using System.Text.Json.Serialization;

namespace ControleContasData.DTO
{
    public class BankSlipDTO
	{
		public int Id { get; set; }
		public string Description { get; set; }
		public Decimal Price { get; set; }
		public DateTime Date { get; set; }
		public DateTime Due { get; set; }
		public int UserId { get; set; }
		public UserDTO User { get; set; } = new UserDTO();
		public IEnumerable<InstallmentDTO> Installments { get; set; } = [];
		public int InstallmentQuantity { get; set; }
	}
}
