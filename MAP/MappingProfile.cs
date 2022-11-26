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
            CreateMap<Common.DTO.JobTableDTO, DOMAIN.Models.JobTable>()
                .ForMember(
                    dest => dest.CustomUserId,
                    opt => opt.MapFrom(src => src.CustomUserId)
                ).ForMember(
                    dest => dest.JobTypeId,
                    opt => opt.MapFrom(src => src.JobTypeId)
                ).ForMember(
                    dest => dest.DAY,
                    opt => opt.MapFrom(src => src.DAY)
                ).ForMember(
                    dest => dest.JOB_TIME,
                    opt => opt.MapFrom(src => src.JOB_TIME)
                ).ForMember(
                    dest => dest.JOB_KEY,
                    opt => opt.MapFrom(src => src.JOB_KEY)
                ).ForMember(
                    dest => dest.IS_ACTIVE,
                    opt => opt.MapFrom(src => src.IS_ACTIVE)
                ).ForMember(
                    dest => dest.DESCRIPTION,
                    opt => opt.MapFrom(src => src.DESCRIPTION)
                );           
        }
    }
}
