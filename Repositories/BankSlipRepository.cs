using ControleContasData.DTO;
using Npgsql;
using Shared.Data.Data;
using Shared.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Data.Repositories
{
	public class BankSlipRepository : IBankSlipRepository
	{
		public IEnumerable<BankSlipDTO> BankSlips()
		{
			var listaBankSlip = new List<BankSlipDTO>();

			using (var connection = DatabaseConnection.Connection())
			{
				connection.Open();
				string sql = "select * from public.\"Bankslip\"";
				using (var cmd = new NpgsqlCommand(sql, connection))
				using (var reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						var account = new BankSlipDTO();
						account.Id = Convert.ToInt32(reader["Id"]);
						account.Description = reader["Description"].ToString();
						account.Price = Convert.ToDecimal(reader["Price"]);
						account.Date = Convert.ToDateTime(reader["Date"]);
						account.InstallmentQuantity = Convert.ToInt32(reader["InstallmentQuantity"]);
						account.UserId = Convert.ToInt32(reader["UserId"]);
						listaBankSlip.Add(account);
					}

				}
				connection.Close();
			}

			return listaBankSlip;
		}

		public void Save(BankSlipDTO bankSlipDTO)
		{
			object idbankSlip;
			using (var connection = DatabaseConnection.Connection())
			{

				connection.Open();
				string sql = "INSERT INTO public.\"Bankslip\"" +
					"(\"Price\", \"Date\", \"Due\", \"InstallmentQuantity\", \"Description\",\"UserId\")\r\n" +
					"VALUES(@Price, @Date, @Due, @InstallmentQuantity, @Description, @UserId) RETURNING \"Id\";";
				using (var cmd = new NpgsqlCommand(sql, connection))
				{
					cmd.Parameters.AddWithValue("@Price", bankSlipDTO.Price);
					cmd.Parameters.AddWithValue("@Date", bankSlipDTO.Date);					
					cmd.Parameters.AddWithValue("@Due", bankSlipDTO.Due);					
					cmd.Parameters.AddWithValue("@InstallmentQuantity", bankSlipDTO.InstallmentQuantity);
					cmd.Parameters.AddWithValue("@Description", bankSlipDTO.Description);
					cmd.Parameters.AddWithValue("@UserId", bankSlipDTO.UserId);
					idbankSlip = cmd.ExecuteScalar();
				}


				if (bankSlipDTO.InstallmentQuantity > 0)
				{					
					var vencimentoBase = bankSlipDTO.Due;
					sql = "INSERT INTO public.\"Installments\" " +
						"(\"AccountId\", \"Price\", \"Due\", \"NumeberInstalment\", \"BankSlipId\")\r\n" +
						"VALUES(@AccountId, @Price, @Due, @NumeberInstalment, @BankSlipId)";

					for (int i = 0; i < bankSlipDTO.InstallmentQuantity; i++)
					{

						using (var cmdInstallment = new NpgsqlCommand(sql, connection))
						{
							cmdInstallment.Parameters.AddWithValue("@Price", bankSlipDTO.Price);
							cmdInstallment.Parameters.AddWithValue("@Due", vencimentoBase);
							cmdInstallment.Parameters.AddWithValue("@NumeberInstalment", (i + 1));
							cmdInstallment.Parameters.AddWithValue("@BankSlipId", idbankSlip);
							cmdInstallment.Parameters.AddWithValue("@AccountId", DBNull.Value);
							cmdInstallment.ExecuteNonQuery();
						}

						vencimentoBase = vencimentoBase.AddMonths(1);
					}

				}

				connection.Close();
			}

		}

		IEnumerable<BankSlipDTO> IBankSlipRepository.AccountsByMonthAndYear(int month, int year)
		{
			var listaBankSlip = new List<BankSlipDTO>();

			using (var connection = DatabaseConnection.Connection())
			{
				connection.Open();
				string sql = "select b.* from public.\"Bankslip\" b" +
					" left join \"Installments\" i on i.\"BankSlipId\" = b.\"Id\" " +
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
							var bankSlip = new BankSlipDTO();
							bankSlip.Id = Convert.ToInt32(reader["Id"]);
							bankSlip.Description = reader["Description"].ToString();
							bankSlip.Price = Convert.ToDecimal(reader["Price"]);
							bankSlip.Date = Convert.ToDateTime(reader["Date"]);
							bankSlip.InstallmentQuantity = Convert.ToInt32(reader["InstallmentQuantity"]);
							listaBankSlip.Add(bankSlip);							
						}

					}
				}
				connection.Close();
			}

			return listaBankSlip;

		}

		BankSlipDTO IBankSlipRepository.BankSlipsById(int id)
		{
			var bankSlip = new BankSlipDTO();

			using (var connection = DatabaseConnection.Connection())
			{
				connection.Open();
				string sql = "select * from public.\"Bankslip\" where \"Id\" = @Id";
				using (var cmd = new NpgsqlCommand(sql, connection))
				{
					cmd.Parameters.AddWithValue("Id", id);
					using (var reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							bankSlip.Id = Convert.ToInt32(reader["Id"]);
							bankSlip.Description = reader["Description"].ToString();
							bankSlip.Price = Convert.ToDecimal(reader["Price"]);
							bankSlip.Date = Convert.ToDateTime(reader["Date"]);
							bankSlip.InstallmentQuantity = Convert.ToInt32(reader["InstallmentQuantity"]);
							bankSlip.UserId = Convert.ToInt32(reader["UserId"]);

						}

					}
				}
				connection.Close();
			}

			return bankSlip;
		}			
	}
}
