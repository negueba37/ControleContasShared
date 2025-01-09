using ControleContasData.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Data.Repositories.Interfaces
{
	public interface IBankSlipRepository
	{
		IEnumerable<BankSlipDTO> BankSlips();
		BankSlipDTO BankSlipsById(int id);
		void Save(BankSlipDTO bankSlipDTO);
		IEnumerable<BankSlipDTO> AccountsByMonthAndYear(int month, int year);
	}
}
