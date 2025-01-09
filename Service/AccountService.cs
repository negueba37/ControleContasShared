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
	public class AccountService
	{
		private readonly IMapper _mapper;		
		private readonly BaseDAO<Account> _baseDAO;
		public AccountService() 
		{		
			var config = new MapperConfiguration(cfg => cfg.AddProfile<AccountProfile>());
			_mapper = config.CreateMapper();
			_baseDAO = new BaseDAO<Account>();
		}
		private bool FindCard(int id) {
			var _cardDAO = new BaseDAO<Card>();
			var card = _cardDAO.GetById(id) ;
			return card != null;
		}
		public void Save(AccountDTO dto)
		{
			if (!FindCard(dto.CardId)) {
				throw new ArgumentException("Cartão de crédito não encontrado");
			}
			Account account = _mapper.Map<Account>(dto);
			if (account.InstallmentQuantity > 0)
			{
				var serviceInstallment = new InstallmentService();
				var priceInstallment = account.Price / account.InstallmentQuantity;
				for (int i = 0; i < account.InstallmentQuantity; i++)
				{
					InstallmentDTO installment = new InstallmentDTO();
					installment.Price = priceInstallment;
					installment.Account = dto;
					installment.Due = DateTime.Now;
					installment.NumeberInstalment = (i + 1);
					serviceInstallment.Save(installment);
				}
			}
			else {
				_baseDAO.Save(account);
			}


		}
		public AccountDTO Update(AccountDTO dto)
		{
			Account account = _mapper.Map<Account>(dto);
			var accountUpdate = _baseDAO.update(account);
			return _mapper.Map<AccountDTO>(accountUpdate);
		}
		public void Delete(int id)
		{
			var accountDTO = GetById(id);
			if (accountDTO != null) {
				_baseDAO.Delete(_mapper.Map<Account>(accountDTO));
			}
		}

		public ICollection<AccountDTO> Get()
		{			
			return _mapper.Map<List<AccountDTO>>(_baseDAO.Get());
		}
		public AccountDTO GetById(int id)
		{
			return _mapper.Map<AccountDTO>(_baseDAO.GetById(id));
		}


	}
}
