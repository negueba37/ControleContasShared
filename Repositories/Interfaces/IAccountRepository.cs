using ConsoleData.DTO;
using ControleContas.Domain;
using ControleContasData.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Data.Repositories.Interfaces
{
	public interface IAccountRepository
	{
        IEnumerable<AccountDTO> Accounts();
        IEnumerable<AccountDTO> AccountsByMonthAndYear(int month, int year);
        AccountDTO AccountById(int Id);
        
        AccountDTO Update(AccountDTO accountDTO);
        void Save(AccountDTO accountDTO);
        void Delete(int id);
	}
}
