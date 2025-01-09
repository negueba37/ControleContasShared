using AutoMapper;
using ConsoleData.DTO;
using ConsoleData.Profiles;
using ConsoleData.Service;
using ControleContas.Domain;
using ControleContasData.DAO;
using ControleContasData.Domain;
using ControleContasData.DTO;

namespace ControleContasData.Service
{
	public class CardService
	{
		private readonly IMapper _mapper;		
		private readonly BaseDAO<Card> _baseDAO;
		public CardService() 
		{		
			var config = new MapperConfiguration(cfg => cfg.AddProfile<CardProfile>());
			_mapper = config.CreateMapper();
			_baseDAO = new BaseDAO<Card>();
		}
		public void Save(CardDTO dto)
		{
			Card card = _mapper.Map<Card>(dto);			
			_baseDAO.Save(card);
		}
		public CardDTO Update(CardDTO dto)
		{
			Card card = _mapper.Map<Card>(dto);
			var cardUpdate = _baseDAO.update(card);
			return _mapper.Map<CardDTO>(cardUpdate);
		}
		public void Delete(int id)
		{
			_baseDAO.Delete(_baseDAO.GetById(id));
		}

		public ICollection<CardDTO> Get()
		{
			return _mapper.Map<List<CardDTO>>(_baseDAO.Get());
		}
		public CardDTO GetById(int id)
		{
			return _mapper.Map<CardDTO>(_baseDAO.GetById(id));
		}


	}
}
