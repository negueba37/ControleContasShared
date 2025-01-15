using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Data.Domain
{
	public class ConfigurationParameters
	{
        public int Id { get; set; }
		public int DashboardPrincipalMes { get; set; }
		public int DashboardPrincipalAno { get; set; }
		public int DashboardPrincipalCartao { get; set; }
		public int AccountMes { get; set; }
		public int AccountAno { get; set; }
		public int BankslipMes { get; set; }
		public int BankslipAno { get; set; }
    }
}
