using ControleContas.Domain;
using ControleContasData.Domain;
using Microsoft.EntityFrameworkCore;
using Shared.Data.Domain;

namespace ControleContas.Data
{
	public class ApplicationContext: DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=controle_contas;User Id=postgres;Password=masterkey;");
			optionsBuilder.LogTo(Console.WriteLine);
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
		}
	    public DbSet<Account> Account { get; set; } = default!;
		public DbSet<Card> Card { get; set; } = default!;
		public DbSet<User> User { get; set; } = default!;
		public DbSet<Installment> Installment { get; set; } = default!;
		public DbSet<ConfigurationParameters> ConfigurationParameters { get; set; } = default!;
		
	}
}
