
using ControleContas.Domain;
using ControleContasData.DTO;

namespace ConsoleData.DTO
{
    public class InstallmentDTO
    {
		public int Id { get; set; }
		public int AccountId { get; set; }
        public int NumeberInstalment { get; set; }
        public AccountDTO Account { get; set; }
		public Decimal Price { get; set; }
		public DateTime Due { get; set; }
		public bool IsPaid { get; set; }
		public int BankSlipId { get; set; }
        public BankSlipDTO BankSlip { get; set; }
    }
}
