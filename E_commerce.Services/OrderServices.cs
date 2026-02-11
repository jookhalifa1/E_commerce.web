using AutoMapper;
using E_commerce.Sahred.CommonResult;
using E_commerce.Sahred.OrderDtos;
using E_commerce.Services.Specifications;
using E_commerce.Services_Abstraction;
using E_Commerce.Domain.Contract;
using E_Commerce.Domain.Contract.GenericRepository;
using E_Commerce.Domain.Entity.IdentityModule;
using E_Commerce.Domain.Entity.OrderModule;
using E_Commerce.Domain.Entity.ProductEntity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IMapper mapper;
        private readonly IBasketRepository basketRepository;
        private readonly IUnitOfWork unitOfWork;

        public OrderServices( IMapper mapper,IBasketRepository basketRepository,IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.basketRepository = basketRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<OrderToReturnDto>> CreateOrderAsync(OrderDto orderDto, string Email)
        {
            var OrderAddress = mapper.Map<AddressDto,  ShippingAddress>(orderDto.AddressDto);
            var basket = await basketRepository.GetBasketByIdAsync(orderDto.BasketId);
            if (basket == null) return Error.NotFound("Basket.NotFound", $"The Basket Has id {orderDto.BasketId} is not Found");

            List<ItemsOfOrder> ordersItems= new List<ItemsOfOrder>();

            foreach (var item in basket.items)
            {
                var product = await unitOfWork.GetRepo<Product, int>().GetById(item.Id);
                if (product == null) return  Error.NotFound("Product Not found", $"Product Has id {item.Id} is not Found");

                ordersItems.Add(CreateOrderItem(item, product));


            }
            var deliveryMethod = await unitOfWork.GetRepo<DeliveryMethod, int>().GetById(orderDto.DeliveryMethodId);

                if (deliveryMethod == null) return Error.NotFound("Product Not found", $"Product Has id {deliveryMethod.Id} is not Found");
            var subTotal = ordersItems.Sum(x => x.Price * x.Quantity);

            var oredr = new Order()
            {
                Address = OrderAddress,
                deliveryMethod = deliveryMethod,
                items = ordersItems,
                SubTotal = subTotal,
                UserEmail = Email


            };
            await unitOfWork.GetRepo<Order,Guid>().AddAsync(oredr);
           var result=await   unitOfWork.saveChangeRepository();
            if (result == 0) return Error.Failure("Order.Failure");
            return mapper.Map<Order, OrderToReturnDto>(oredr);

            




        }


        public async Task< Result< IEnumerable<OrderToReturnDto>>>  GetAllOrdersAsync( string userEmail)
        {

            var OrderSpec = new  OrderSpecifications(userEmail);

            
            var Orders=await unitOfWork.GetRepo<Order,Guid>().GetAllAsync(OrderSpec);

            if (Orders == null || !Orders.Any())
                return Result<IEnumerable<OrderToReturnDto>>.Fail(
                    Error.NotFound("Order.NotFound", "No orders found")
                );

            var result = mapper.Map<IEnumerable<Order>, IEnumerable<OrderToReturnDto>>(Orders);
            var x = Result<IEnumerable<OrderToReturnDto>>.Ok(result);

            return x;
        }   

        public async Task<Result<OrderToReturnDto>> GetOrderByIdAsync(Guid id)
        {
            var spec = new OrderSpecifications(id);
            var Order = await unitOfWork.GetRepo<Order, Guid>().GetById(spec);

            if (Order is null) return Error.NotFound("Order.NotFound", $"Order has {id} is Not Found");
            
            var result=mapper.Map<Order, OrderToReturnDto>(Order);
            return result;
        }

        private static ItemsOfOrder CreateOrderItem(E_Commerce.Domain.Entity.BasketModule.BasketItem item, Product? product)
        {
             return new ItemsOfOrder()
            {
                productItemOrder = new productItemOrder() { Id = product.Id, PictureUrl = product.PictureUrl, Name = product.Name },
                Price = product.Price,
                Quantity = item.Quantity

            };
        }

        public async Task<Result<IEnumerable<DeliveryMethodDto>>> GetAllDeliveriesAsync()
        {
            var Delivery =await unitOfWork.GetRepo<DeliveryMethod,int>().GetAllAsync();
            if (Delivery is null) return Error.NotFound("Deliveries Not Found");
            var result = mapper.Map<IEnumerable< DeliveryMethod>, IEnumerable< DeliveryMethodDto>>(Delivery);


            return Result<IEnumerable<DeliveryMethodDto>>.Ok(result);
        }
    }
}
