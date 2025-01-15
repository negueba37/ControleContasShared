using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Data.Domain
{
	public class DashboardMonthCard
	{
        public int Id { get; set; }
        public string Description { get; set; }
        public Decimal Value { get; set; }		
		public string ValueFormated => Value.ToString("N2", new System.Globalization.CultureInfo("pt-BR"));
	}
}
