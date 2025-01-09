using AutoMapper;
using ConsoleData.DTO;
using ControleContas.Domain;
using ControleContasData.Domain;
using ControleContasData.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleData.Profiles
{
	internal class InstallmentProfile: Profile
	{
        public InstallmentProfile()
        {
            CreateMap<Installment, InstallmentDTO>().ForMember(dest => dest.Account,opt => opt.MapFrom(src => src.Account));
			CreateMap<Account, AccountDTO>().ForMember(dest => dest.Card, opt => opt.MapFrom(src => src.Card));
			CreateMap<Card, CardDTO>();

			CreateMap<InstallmentDTO, Installment>().ForMember(dest => dest.Account, opt => opt.MapFrom(src => src.Account)); ;
			CreateMap<AccountDTO, Account>().ForMember(dest => dest.Card, opt => opt.MapFrom(src => src.Card));
			CreateMap<CardDTO, Card>();
		}
	}
}
