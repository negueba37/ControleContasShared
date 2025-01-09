using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Data.Data
{
	internal class DatabaseConnection
	{
		public static NpgsqlConnection Connection() 
		{ 
			return new NpgsqlConnection("Server=127.0.0.1;Port=5432;Database=controle_contas;User Id=postgres;Password=masterkey;");
		}
	}
}
