using AutoMapper;
using ConsoleData.DTO;
using ConsoleData.Profiles;
using ControleContas.Domain;
using ControleContasData.DAO;
using ControleContasData.Domain;
using ControleContasData.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleData.Service
{
	internal class InstallmentService
	{
		private readonly IMapper _mapper;
		private readonly BaseDAO<Installment> _baseDAO;
		public InstallmentService()
		{
			var config = new MapperConfiguration(cfg => cfg.AddProfile<InstallmentProfile>());
			_mapper = config.CreateMapper();			
			_baseDAO = new BaseDAO<Installment>();
		}
		public void Save(InstallmentDTO dto)
		{
			Installment installment = _mapper.Map<Installment>(dto);

			_baseDAO.Save(installment);
		}
		public InstallmentDTO Update(InstallmentDTO dto)
		{
			Installment installment = _mapper.Map<Installment>(dto);
			var installmentUpdate = _baseDAO.update(installment);
			return _mapper.Map<InstallmentDTO>(installmentUpdate);
		}
		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public ICollection<InstallmentDTO> Get()
		{
			return new List<InstallmentDTO>();
		}



	}
}
