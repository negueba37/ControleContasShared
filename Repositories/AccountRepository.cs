using AutoMapper;
using ConsoleData.DTO;
using ConsoleData.Profiles;
using ConsoleData.Service;
using ControleContas.Data;
using ControleContas.Domain;
using ControleContasData.DAO;
using ControleContasData.Domain;
using ControleContasData.DTO;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Shared.Data.Data;
using Shared.Data.Repositories.Interfaces;
using System.Data.Common;
using System.Security.AccessControl;
using System.Security.Principal;


namespace Shared.Data.Repositories
{
	public class AccountRepository : IAccountRepository
	{
		private readonly IMapper _mapper;
		private readonly ApplicationContext _context;
		private readonly ICardRepository _cardRepository;
		public AccountRepository(ApplicationContext context, ICardRepository cardRepository)
		{
			var config = new MapperConfiguration(cfg => cfg.AddProfile<AccountProfile>());
			_mapper = config.CreateMapper();
			_context = context;
			_cardRepository = cardRepository;
		}

		IEnumerable<AccountDTO> IAccountRepository.Accounts()
		{

			var listaAccount = new List<AccountDTO>();

			using (var connection = DatabaseConnection.Connection())
			{
				connection.Open();
				string sql = "select * from public.\"Accounts\"";
				using (var cmd = new NpgsqlCommand(sql, connection))
				using (var reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						var account = new AccountDTO();
						account.Id = Convert.ToInt32(reader["Id"]);
						account.Description = reader["Description"].ToString();
						account.Price = Convert.ToDecimal(reader["Price"]);
						account.Date = Convert.ToDateTime(reader["Date"]);
						account.CardId = Convert.ToInt32(reader["CardId"]);
						account.InstallmentQuantity = Convert.ToInt32(reader["InstallmentQuantity"]);
						listaAccount.Add(account);
					}

				}
				connection.Close();
			}

			return listaAccount;
		}

		public AccountDTO AccountById(int id)
		{
			var account = new AccountDTO();

			using (var connection = DatabaseConnection.Connection())
			{
				connection.Open();
				string sql = "select * from public.\"Accounts\" where \"Id\" = @Id";
				using (var cmd = new NpgsqlCommand(sql, connection))
				{
					cmd.Parameters.AddWithValue("@Id", id);
					using (var reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							account.Id = Convert.ToInt32(reader["Id"]);
							account.Description = reader["Description"].ToString();
							account.Date = Convert.ToDateTime(reader["Date"]);
							account.CardId = Convert.ToInt32(reader["CardId"]);
							account.Price = Convert.ToDecimal(reader["Price"]);
							account.InstallmentQuantity = Convert.ToInt32(reader["InstallmentQuantity"]);
						}
					}
				}
				sql = "select * from public.\"Cards\" where \"Id\" = @Id";
				using (var cmdCard = new NpgsqlCommand(sql, connection))
				{
					cmdCard.Parameters.AddWithValue("@Id", account.CardId);
					using (var reader = cmdCard.ExecuteReader())
					{
						if (reader.Read())
						{
							account.Card.Id = Convert.ToInt32(reader["Id"]);
							account.Card.Description = reader["Description"].ToString();
							account.Card.Limit = Convert.ToDecimal(reader["Limit"]);
							account.Card.MaturityDay = Convert.ToInt32(reader["MaturityDay"]);
							account.Card.BestPurchaseDay = Convert.ToInt32(reader["BestPurchaseDay"]);
						}
					}
				}
				connection.Close();
			}

			return account;
		}

		public AccountDTO Update(AccountDTO accountDTO)
		{
			var account = _mapper.Map<Account>(accountDTO);
			_context.Account.Update(account);
			_context.SaveChanges();
			return _mapper.Map<AccountDTO>(account);
		}

		void IAccountRepository.Save(AccountDTO accountDTO)
		{
			object idAccount;
			using (var connection = DatabaseConnection.Connection())
			{

				connection.Open();
				string sql = "INSERT INTO public.\"Accounts\"" +
					"(\"Price\", \"Date\", \"CardId\", \"InstallmentQuantity\", \"Description\",\"UserId\")\r\n" +
					"VALUES(@Price, @Date, @CardId, @InstallmentQuantity, @Description, @UserId) RETURNING \"Id\";";
				using (var cmd = new NpgsqlCommand(sql, connection))
				{

					cmd.Parameters.AddWithValue("@Price", accountDTO.Price);
					cmd.Parameters.AddWithValue("@Date", accountDTO.Date);
					cmd.Parameters.AddWithValue("@CardId", accountDTO.CardId);
					cmd.Parameters.AddWithValue("@InstallmentQuantity", accountDTO.InstallmentQuantity);
					cmd.Parameters.AddWithValue("@Description", accountDTO.Description);
					cmd.Parameters.AddWithValue("@UserId", accountDTO.UserId);					
					idAccount = cmd.ExecuteScalar();
				}


				if (accountDTO.InstallmentQuantity > 0)
				{
					var priceInstallment = accountDTO.Price / accountDTO.InstallmentQuantity;
					var vencimentoBase = DateTime.ParseExact($"{accountDTO.Card.MaturityDay:d2}-{accountDTO.Date.Month:d2}-{accountDTO.Date.Year}", "dd-MM-yyyy", null);
					if (accountDTO.Date.Day > accountDTO.Card.MaturityDay || accountDTO.Date.Day >= accountDTO.Card.BestPurchaseDay)
					{
						vencimentoBase = vencimentoBase.AddMonths(1);
					}

					sql = "INSERT INTO public.\"Installments\"" +
						"(\"AccountId\", \"Price\", \"Due\", \"NumeberInstalment\")\r\n" +
						"VALUES(@AccountId, @Price, @Due, @NumeberInstalment)";

					for (int i = 0; i < accountDTO.InstallmentQuantity; i++)
					{

						using (var cmdInstallment = new NpgsqlCommand(sql, connection))
						{
							cmdInstallment.Parameters.AddWithValue("@AccountId", idAccount);
							cmdInstallment.Parameters.AddWithValue("@Price", priceInstallment);
							cmdInstallment.Parameters.AddWithValue("@Due", vencimentoBase);
							cmdInstallment.Parameters.AddWithValue("@NumeberInstalment", (i + 1));
							cmdInstallment.ExecuteNonQuery();
						}

						vencimentoBase = vencimentoBase.AddMonths(1);
					}
					
				}

				connection.Close();
			}
		}


		void IAccountRepository.Delete(int id)
		{
			var accountDTO = AccountById(id);
			if (accountDTO == null)
				throw new NotImplementedException("Conta não encontrada");
			using (var connection = DatabaseConnection.Connection())
			{
				connection.Open();
				string sql = "delete from public.\"Accounts\" where \"Id\" = @Id";
				using (var cmd = new NpgsqlCommand(sql, connection))
				{
					cmd.Parameters.AddWithValue("@Id", id);
					using (var reader = cmd.ExecuteReader()) ;
				}
				connection.Close();
			}

		}

		IEnumerable<AccountDTO> IAccountRepository.AccountsByMonthAndYear(int month, int year)
		{
			var listaAccount = new List<AccountDTO>();

			using (var connection = DatabaseConnection.Connection())
			{
				connection.Open();
				string sql = "select a.* from public.\"Accounts\" a" +
					" left join \"Installments\" i on i.\"AccountId\" = a.\"Id\" and i.\"NumeberInstalment\" = 1 " +
					"WHERE EXTRACT(MONTH FROM i.\"Due\") = @month " +
					"AND EXTRACT(YEAR FROM i.\"Due\") = @year " +
					"Order by \"Date\" desc";
				using (var cmd = new NpgsqlCommand(sql, connection))
				{
					cmd.Parameters.AddWithValue("@month", month);
					cmd.Parameters.AddWithValue("@year", year);
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var account = new AccountDTO();
							account.Id = Convert.ToInt32(reader["Id"]);
							account.Description = reader["Description"].ToString();
							account.Price = Convert.ToDecimal(reader["Price"]);
							account.Date = Convert.ToDateTime(reader["Date"]);
							account.CardId = Convert.ToInt32(reader["CardId"]);
							account.InstallmentQuantity = Convert.ToInt32(reader["InstallmentQuantity"]);
							account.Card = _cardRepository.CardById(account.CardId);
							listaAccount.Add(account);
						}

					}
				}
				connection.Close();
			}

			return listaAccount;
		}
	}
}




