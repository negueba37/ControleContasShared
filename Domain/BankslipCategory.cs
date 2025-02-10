using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Data.Domain
{
	public class BankslipCategory
	{
		public int Id { get; set; }
        public int BankslipId { get; set; }
        public int CategoryId { get; set; }
    }
}
