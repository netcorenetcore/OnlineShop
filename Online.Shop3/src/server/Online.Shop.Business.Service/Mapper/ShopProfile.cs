using AutoMapper;
using Online.Shop.Business.Contracts;
using Online.Shop.Business.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Online.Shop.Business.Service.Mapper
{
    public class ShopProfile:Profile
    {
        public ShopProfile()
        {
            CreateMap<Product, ProductDto>()
           .ReverseMap();

            CreateMap<Store, StoreDto>()
          .ReverseMap();



            CreateMap<Sale, SaleDto>()
           .ForMember(opt => opt.ProductBarcodeNumber, conf => conf.MapFrom(x => x.Product == null ? (long?)null : x.Product.BarcodeNumber))
           .ForMember(opt => opt.StoreName, conf => conf.MapFrom(x => x.Store == null ? null : x.Store.Name))
           .ReverseMap()
           .ForMember(opt => opt.Store, conf => conf.Ignore());

           
        }
    }
}
