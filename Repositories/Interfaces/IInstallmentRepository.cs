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
	public interface IInstalmentRepository
	{
        void Save(InstallmentDTO installmentDTO);
        void Save(IEnumerable<InstallmentDTO> listInstallmentDTO);
        IEnumerable<InstallmentDTO> GetByAccount(int id);
        IEnumerable<InstallmentDTO> GetByMonthMaturity(int month, int year, int card);
        IEnumerable<InstallmentDTO> GetByMonthMaturity(int month, int year);
		InstallmentDTO GetById(int id);
		void Update(InstallmentDTO installmentDTO);

	}
}
