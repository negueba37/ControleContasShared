using ControleContas.Domain;
using ControleContasData.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleData.DTO
{
	public class ConfigurationParametersDTO
	{
		public int Id { get; set; }
		public string DashboardPrincipalMes { get; set; }
		public string DashboardPrincipalAno { get; set; }
		public string DashboardPrincipalCartao { get; set; }
		public string AccountMes { get; set; }
		public string AccountAno { get; set; }
		public string BankslipAno { get; set; }
		public string BankslipMes { get; set; }
	}
}
