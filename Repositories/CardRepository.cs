using AutoMapper;
using ConsoleData.DTO;
using ConsoleData.Profiles;
using ControleContas.Data;
using ControleContas.Domain;
using ControleContasData.DTO;
using Microsoft.EntityFrameworkCore;
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
	public class CardRepository : ICardRepository
	{
		private ApplicationContext _context;
        private readonly IMapper _mapper;
        public CardRepository(ApplicationContext context)
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<CardProfile>());
            _mapper = config.CreateMapper();
            _context = context;
        }
        IEnumerable<CardDTO> ICardRepository.Cards()
        {
			var listaCards = new List<CardDTO>();

			using (var connection = DatabaseConnection.Connection())
			{
				connection.Open();
				string sql = "select * from public.\"Cards\" order by \"Description\"";
				using (var cmd = new NpgsqlCommand(sql, connection))
				using (var reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						var card = new CardDTO();
						card.Id = Convert.ToInt32(reader["Id"]);
						card.Description = reader["Description"].ToString();
						card.Limit = Convert.ToDecimal(reader["Limit"]);
						card.BestPurchaseDay = Convert.ToInt32(reader["BestPurchaseDay"]);
						card.MaturityDay = Convert.ToInt32(reader["MaturityDay"]);						
						listaCards.Add(card);
					}

				}
				connection.Close();
			}

			return listaCards;

		}

		CardDTO ICardRepository.CardById(int id)
		{
			var card = new CardDTO();

			using (var connection = DatabaseConnection.Connection())
			{
				connection.Open();
				string sql = "select * from public.\"Cards\" where \"Id\" = @Id";
				using (var cmd = new NpgsqlCommand(sql, connection))
				{
					cmd.Parameters.AddWithValue("@Id", id);

					using (var reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							card.Id = Convert.ToInt32(reader["Id"]);
							card.Description = reader["Description"].ToString();
							card.Limit = Convert.ToDecimal(reader["Limit"]);
							card.BestPurchaseDay = Convert.ToInt32(reader["BestPurchaseDay"]);
							card.MaturityDay = Convert.ToInt32(reader["MaturityDay"]);
						}

					}
				}
				connection.Close();
			}

			return card;
			
		}
	}
}
