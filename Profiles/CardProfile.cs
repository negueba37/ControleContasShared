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
	internal class CardProfile: Profile
	{
        public CardProfile()
        {
			CreateMap<Card, CardDTO>();
			CreateMap<CardDTO, Card>();
		}
    }
}
