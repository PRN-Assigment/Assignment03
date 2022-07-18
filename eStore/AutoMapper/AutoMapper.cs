﻿using AutoMapper;
using DataLayerDB.DataBaseScaffold;
using eStore.Models;

namespace eStore.AutoMapper
{
    public class AutoMapper : Profile
    {
        IMapper _mapper;

        public AutoMapper()
        {
            CreateMap<Member,MemberViewModel>().ReverseMap();
        }
    }
}
