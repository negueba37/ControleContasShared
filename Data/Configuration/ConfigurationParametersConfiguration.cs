using ControleContas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Data.Domain;

namespace ControleContas.Data.Configuration
{
	internal class ConfigurationParametersConfiguration : IEntityTypeConfiguration<ConfigurationParameters>
	{
		public void Configure(EntityTypeBuilder<ConfigurationParameters> builder)
		{
			builder.ToTable("ConfigurationParameters");
			builder.HasKey(p => p.Id);
			builder.Property(p => p.Id)
				.ValueGeneratedOnAdd()
				.UseIdentityColumn();
			builder.Property(p => p.DashboardPrincipalMes);
			builder.Property(p => p.DashboardPrincipalAno);			
			builder.Property(p => p.DashboardPrincipalCartao);
			builder.Property(p => p.AccountMes);
			builder.Property(p => p.AccountAno);
		}
	}
}
