using Shared.Data.Domain;

namespace Shared.Data.Repositories.Interfaces
{
	public interface IConfigurationParametersRepository
	{
		public ConfigurationParameters Get();
		public void Update(ConfigurationParameters configurationParameters);
        
	}
}
