using AutoMapper;
using Store.App.Controllers.Api.Models;
using Store.DAL.Models;

namespace Store.App.Mapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductResource>();
        }
    }
}