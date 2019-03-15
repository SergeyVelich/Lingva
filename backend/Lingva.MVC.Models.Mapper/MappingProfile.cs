﻿using AutoMapper;
using Lingva.DAL.Entities;
using Lingva.MVC.Dto;

namespace Lingva.MVC
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<DictionaryRecord, DictionaryRecordViewDTO>()
            //.ForMember("Language", opt => opt.MapFrom(c => c.LanguageName))
            //.ForMember("Word", opt => opt.MapFrom(c => c.WordName));

            //CreateMap<DictionaryRecordCreatingDTO, DictionaryRecord>()
            //.ForMember("UserId", opt => opt.MapFrom(c => c.UserId))
            //.ForMember("WordName", opt => opt.MapFrom(c => c.Word))
            //.ForMember("LanguageName", opt => opt.MapFrom(c => c.Language))
            //.ForMember("Translation", opt => opt.MapFrom(c => c.Translation))
            //.ForMember("Context", opt => opt.MapFrom(c => c.Context))
            //.ForMember("Picture", opt => opt.MapFrom(c => c.Picture))
            //.ForAllOtherMembers(opt => opt.Ignore());

            //CreateMap<DictionaryRecord, WordViewDTO>()
            //.ForMember("Word", opt => opt.MapFrom(c => c.WordName));

            CreateMap<Group, GroupDTO>();
            CreateMap<GroupDTO, Group>();

            CreateMap<Group, Group>();
        }
    }
}
