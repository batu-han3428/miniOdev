using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Common.DTO.VeriGirisiDTO, DOMAIN.Models.HastaBilgileri>();
            CreateMap<DOMAIN.Models.HastaBilgileri, Common.ViewModel.ListeyiGorViewModel>();
        }
    }
}
