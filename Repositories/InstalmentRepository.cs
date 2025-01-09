using AutoMapper;
using ConsoleData.DTO;
using ConsoleData.Profiles;
using ControleContas.Data;
using ControleContas.Domain;
using ControleContasData.Domain;
using ControleContasData.DTO;
using Npgsql;
using Shared.Data.Data;
using Shared.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Shared.Data.Repositories
{
	public class InstalmentRepository : IInstalmentRepository
	{
		private ApplicationContext _context;
		private IAccountRepository _accountRepository;
		private readonly IBankSlipRepository _bankSlipRepository;
        private readonly IMapper _mapper;
		public InstalmentRepository(ApplicationContext context, 
            IAccountRepository accountRepository,
            IBankSlipRepository bankSlipRepository)
		{
			var config = new MapperConfiguration(cfg => cfg.AddProfile<InstallmentProfile>());
			_mapper = config.CreateMapper();
			_context = context;
			_accountRepository = accountRepository;
			_bankSlipRepository = bankSlipRepository;
		}

		public void Save(InstallmentDTO installmentDTO)
		{
			_context.Installment.Add(_mapper.Map<Installment>(installmentDTO));
			_context.SaveChanges();
		}
		void IInstalmentRepository.Update(InstallmentDTO installmentDTO)
        { 
            using (var connection = DatabaseConnection.Connection())
            {
                connection.Open();
                string sql = "Update public.\"Installments\" set \"Price\" = @Price ,\"Due\" = @Due" +
                    " where \"Id\" = @Id";
                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", installmentDTO.Id);
                    cmd.Parameters.AddWithValue("@Price", installmentDTO.Price);
                    cmd.Parameters.AddWithValue("@Due", installmentDTO.Due);
                    using (var reader = cmd.ExecuteReader());
                }
                connection.Close();
            }

		}

		void IInstalmentRepository.Save(IEnumerable<InstallmentDTO> listInstallmentDTO)
        {
            if (listInstallmentDTO != null)
            {
                foreach (var item in listInstallmentDTO)
                {
                    _context.Add(_mapper.Map<Installment>(item));
                }
                _context.SaveChanges();
            }
        }

        InstallmentDTO IInstalmentRepository.GetById(int id) 
        {
			var installment = new InstallmentDTO();

			using (var connection = DatabaseConnection.Connection())
			{
				connection.Open();
				string sql = "select * from public.\"Installments\" where \"Id\" = @Id";
				using (var cmd = new NpgsqlCommand(sql, connection))
				{
					cmd.Parameters.AddWithValue("@Id", id);
					using (var reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							installment.Id = Convert.ToInt32(reader["Id"]);
							installment.AccountId = Convert.ToInt32(reader["AccountId"]);
							installment.Price = Convert.ToDecimal(reader["Price"]);
							installment.IsPaid = Convert.ToBoolean(reader["IsPaid"]);
							installment.Due = Convert.ToDateTime(reader["Due"]).ToLocalTime();
							installment.NumeberInstalment = Convert.ToInt32(reader["NumeberInstalment"]);
						}
					}
				}
				connection.Close();
			}
			return installment;
		}
        IEnumerable<InstallmentDTO> IInstalmentRepository.GetByAccount(int id)
        {
            var listaInstallment = new List<InstallmentDTO>();

            using (var connection = DatabaseConnection.Connection())
            {
                connection.Open();
                string sql = "select * from public.\"Installments\" where \"AccountId\" = @AccountId "+
				"order by \"Installments\".\"Due\"";
				using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@AccountId", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            InstallmentDTO installment = new InstallmentDTO();
                            installment.Id = Convert.ToInt32(reader["Id"]);
                            installment.AccountId = Convert.ToInt32(reader["AccountId"]);
                            installment.Price = Convert.ToDecimal(reader["Price"]);
							installment.IsPaid = Convert.ToBoolean(reader["IsPaid"]);
							installment.Due = Convert.ToDateTime(reader["Due"]);
                            installment.NumeberInstalment = Convert.ToInt32(reader["NumeberInstalment"]);
                            listaInstallment.Add(installment);
                        }
                    }
                }
                connection.Close();
            }
            return listaInstallment;

        }

        IEnumerable<InstallmentDTO> IInstalmentRepository.GetByMonthMaturity(int month, int year)
        {
			var listaInstallment = new List<InstallmentDTO>();

			using (var connection = DatabaseConnection.Connection())
			{
				connection.Open();
				string sql = "select \"Installments\".* from public.\"Installments\" " +
							"left Join \"Bankslip\"  on \"BankSlipId\" = \"public\".\"Bankslip\".\"Id\"" +
							" WHERE EXTRACT(MONTH FROM \"Installments\".\"Due\") = @month " +
							"AND EXTRACT(YEAR FROM \"Installments\".\"Due\") = @year " +
				            "and \"AccountId\" is null " +
							"order by \"public\".\"Bankslip\".\"Date\" desc";

				using (var cmd = new NpgsqlCommand(sql, connection))
				{
					cmd.Parameters.AddWithValue("@month", month);
					cmd.Parameters.AddWithValue("@year", year);
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							InstallmentDTO installment = new InstallmentDTO();
							installment.Id = Convert.ToInt32(reader["Id"]);
							installment.BankSlipId = Convert.ToInt32(reader["BankSlipId"]);
							installment.BankSlip = _bankSlipRepository.BankSlipsById(installment.BankSlipId);
							installment.Price = Convert.ToDecimal(reader["Price"]);
							installment.IsPaid = Convert.ToBoolean(reader["IsPaid"]);
							installment.Due = Convert.ToDateTime(reader["Due"]);
							installment.NumeberInstalment = Convert.ToInt32(reader["NumeberInstalment"]);
							listaInstallment.Add(installment);
						}
					}
				}
				connection.Close();
			}
			return listaInstallment;

		}
		IEnumerable<InstallmentDTO> IInstalmentRepository.GetByMonthMaturity(int month, int year,int card)
        {
            var listaInstallment = new List<InstallmentDTO>();

            using (var connection = DatabaseConnection.Connection())
            {
                connection.Open();
                string sql = "select \"Installments\".* from public.\"Installments\" " +
                            "left Join \"Accounts\"  on \"AccountId\" = \"public\".\"Accounts\".\"Id\"" +
                            "left Join \"Cards\"  on \"Cards\".\"Id\" = \"public\".\"Accounts\".\"CardId\"" +
                            " WHERE EXTRACT(MONTH FROM \"Due\") = @month " +
                            "AND EXTRACT(YEAR FROM \"Due\") = @year ";
                if (card > 0)
                    sql += "AND \"Cards\".\"Id\" = @CardId ";
				sql += "AND EXTRACT(YEAR FROM \"Due\") = @year and \"AccountId\" is not null "+
							"order by \"public\".\"Accounts\".\"Date\" desc";

                using (var cmd = new NpgsqlCommand(sql, connection))
                {
					if (card > 0)
						cmd.Parameters.AddWithValue("@CardId", card);
                    cmd.Parameters.AddWithValue("@month", month);
                    cmd.Parameters.AddWithValue("@year", year);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            InstallmentDTO installment = new InstallmentDTO();
                            installment.Id = Convert.ToInt32(reader["Id"]);
                            installment.AccountId = Convert.ToInt32(reader["AccountId"]);
                            installment.Account = _accountRepository.AccountById(installment.AccountId);
							installment.Price = Convert.ToDecimal(reader["Price"]);
							installment.IsPaid = Convert.ToBoolean(reader["IsPaid"]);
							installment.Due = Convert.ToDateTime(reader["Due"]);
                            installment.NumeberInstalment = Convert.ToInt32(reader["NumeberInstalment"]);
                            listaInstallment.Add(installment);
                        }
                    }
                }
                connection.Close();
            }
            return listaInstallment;
        }
    }
}
