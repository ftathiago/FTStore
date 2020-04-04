using AutoMapper;
using FTStore.Domain.Entities;
using FTStore.Domain.ValueObjects;
using FTStore.Infra.Table;

namespace FTStore.Infra.Mappings.Profiles
{
    public class OrderMapProfile : Profile
    {
        public OrderMapProfile()
        {
            MapOrderEntityToModel();
            CreateMap<OrderItemEntity, OrderItemTable>();
            MapOrderModelToEntity();
            CreateMap<OrderItemTable, OrderItemEntity>();
        }

        public void MapOrderEntityToModel()
        {
            CreateMap<OrderEntity, OrderTable>()
                .ForMember(
                    orderModel => orderModel.Street,
                    opt => opt.MapFrom(orderEntity =>
                        orderEntity.DeliveryAddress.Street)
                )
                .ForMember(
                    orderModel => orderModel.AddressNumber,
                    opt => opt.MapFrom(orderEntity =>
                        orderEntity.DeliveryAddress.AddressNumber)
                )
                .ForMember(
                    orderModel => orderModel.Neighborhood,
                    opt => opt.MapFrom(orderEntity =>
                        orderEntity.DeliveryAddress.Neighborhood)
                )
                .ForMember(
                    orderModel => orderModel.City,
                    opt => opt.MapFrom(orderEntity =>
                        orderEntity.DeliveryAddress.City)
                )
                .ForMember(
                    orderModel => orderModel.State,
                    opt => opt.MapFrom(orderentity =>
                        orderentity.DeliveryAddress.State)
                )
                .ForMember(
                    orderModel => orderModel.ZIPCode,
                    opt => opt.MapFrom(orderEntity =>
                        orderEntity.DeliveryAddress.ZIPCode)
                );
        }

        public void MapOrderModelToEntity()
        {
            CreateMap<OrderTable, OrderEntity>()
                .ForPath(
                    orderEntity => orderEntity.DeliveryAddress,
                    opt => opt.MapFrom(orderModel =>
                        new Address(
                            orderModel.Street,
                            orderModel.AddressNumber,
                            orderModel.Neighborhood,
                            orderModel.City,
                            orderModel.State,
                            orderModel.ZIPCode))
                );
        }
    }
}
