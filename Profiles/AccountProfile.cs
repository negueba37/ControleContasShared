using AutoMapper;
using ConsoleData.DTO;
using ControleContas.Domain;
using ControleContasData.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleData.Profiles
{
	internal class AccountProfile: Profile
	{
        public AccountProfile()
        {
            CreateMap<Account, AccountDTO>().ForMember(dest => dest.Card, opt => opt.MapFrom(src => src.Card));
            CreateMap<Card, CardDTO>();
            CreateMap<AccountDTO, Account>().ForMember(dest => dest.Card, opt => opt.MapFrom(src => src.Card));
            CreateMap<CardDTO, Card>();
        }
	}
}
