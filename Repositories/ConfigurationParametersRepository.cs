using ConsoleData.DTO;
using ControleContas.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Shared.Data.Data;
using Shared.Data.Domain;
using Shared.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Data.Repositories
{
	public class ConfigurationParametersRepository : IConfigurationParametersRepository
	{
		private readonly ApplicationContext _context;
		public ConfigurationParametersRepository(ApplicationContext context)
		{
			_context = context;
		}
		ConfigurationParameters IConfigurationParametersRepository.Get()
		{
			return _context.ConfigurationParameters.FirstOrDefault(x => x.Id == 1);
		}

		void IConfigurationParametersRepository.Update(ConfigurationParameters configurationParameters)
		{
			_context.ConfigurationParameters.Update(configurationParameters);
			_context.SaveChanges();	
		}
	}
}
