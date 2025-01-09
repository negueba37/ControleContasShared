using ConsoleData.DTO;
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
	public class UserRepository : IUserRepository
	{
		IEnumerable<UserDTO> IUserRepository.Users()
		{
			var listaUsers = new List<UserDTO>();

			using (var connection = DatabaseConnection.Connection())
			{
				connection.Open();
				string sql = "select * from public.\"User\"";
				using (var cmd = new NpgsqlCommand(sql, connection))
				using (var reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						var user = new UserDTO();
						user.Id = Convert.ToInt32(reader["Id"]);
						user.Name = reader["Name"].ToString();
						user.Login = reader["Login"].ToString();
						user.Password = reader["Password"].ToString();

						listaUsers.Add(user);
					}

				}
				connection.Close();
			}

			return listaUsers;
		}
	}
}
