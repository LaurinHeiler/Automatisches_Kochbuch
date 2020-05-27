using AutoMapper;
using Automatisches_Kochbuch.Dtos;
using Automatisches_Kochbuch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Automatisches_Kochbuch
{
    public class RezeptProfile : Profile
    {
        public RezeptProfile()
        {
            CreateMap<TabRezepte, RezeptReadDto>();
        }
    }
}
