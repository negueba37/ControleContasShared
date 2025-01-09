using ControleContas.Domain;
using System.Text.Json.Serialization;


namespace Shared.Data.Domain
{
	public class User
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
		
        [JsonIgnore]
		public ICollection<Account> Accounts { get; set; }
    }
}
