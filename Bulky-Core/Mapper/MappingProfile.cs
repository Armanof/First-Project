using AutoMapper;
using Bulky_DTO;
using Bulky_DTO.Identity;
using Bulky_Models;
using Bulky_Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Bulky_Core.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Category, SelectListItem>()
                .ForMember(x => x.Value, it => it.MapFrom(t => t.Id))
                .ForMember(x => x.Text, it => it.MapFrom(t => t.Name))
                .ReverseMap();


            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<Permission, PermissionDTO>().ReverseMap();
            CreateMap<PermissionEndPoint, PermissionEndPointDTO>().ReverseMap();
        }
    }
}
