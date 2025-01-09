using ControleContas.Domain;
using ControleContasData.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleData.DTO
{
	public class CardDTO
	{
		public int Id { get; set; }
		public string Description { get; set; }
		public decimal Limit { get; set; }
        public int MaturityDay { get; set; }
        public int BestPurchaseDay { get; set; }

        public ICollection<AccountDTO>? Accounts { get; set; }
	}
}
