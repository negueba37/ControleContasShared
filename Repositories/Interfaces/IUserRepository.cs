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
	public interface IUserRepository
	{
        IEnumerable<UserDTO> Users();
	}
}
