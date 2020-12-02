using AutoMapper;
using Store.App.Controllers.Api.Models;
using Store.DAL.Models;

namespace Store.App.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductResource>()
                .ForMember(pr => pr.Id, opt => opt.MapFrom(p => p.Id))
                .ForMember(pr => pr.CategoryId, opt => opt.MapFrom(dp => dp.CategoryId))
                .ForMember(pr => pr.Name, opt => opt.MapFrom(p => p.Name))
                .ForMember(pr => pr.Image, opt => opt.MapFrom(p => p.Image))
                .ForMember(pr => pr.Price, opt => opt.MapFrom(p => p.Price))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Cart, CartResource>()
                .ForMember(cr => cr.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(cr => cr.Total, opt => opt.MapFrom(c => c.Total))
                .ForMember(cr => cr.Items, opt => opt.MapFrom(c => c.CartItems))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<CartItem, CartItemResource>()
                .ForMember(cri => cri.Product, opt => opt.MapFrom(i => i.Product))
                .ForMember(cri => cri.Amount, opt => opt.MapFrom(i => i.Count))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Cart, Order>()
                .ForMember(o => o.Items, opt => opt.MapFrom(c => c.CartItems))
                .ForMember(o => o.Total, opt => opt.MapFrom(c => c.Total))
                .ForAllOtherMembers(opt => opt.Ignore());;

            CreateMap<CartItem, OrderItem>()
                .ForMember(oi => oi.ProductId, opt => opt.MapFrom(ci => ci.ProductId))
                .ForMember(oi => oi.Count, opt => opt.MapFrom(ci => ci.Count))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}
