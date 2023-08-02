using Amazon.Application.Contracts;
using Amazon.Domain;
using Amazon.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Application.Services
{
    public class OrderItemServices : IOrderItemService
    {
        private readonly IOrderItemReposatory _repository;
        private readonly IMapper _Mapper;
        private readonly IProductServices productServices;

        public OrderItemServices(IOrderItemReposatory repository,IMapper mapper,
            IProductServices productServices)
        {
            _Mapper = mapper;
            this.productServices = productServices;
            _repository = repository;
        }

        public async Task<List<OrderItemShow>> getOrderItemsByOrderId(int id)
        {
            var res =await _repository.getOrderItemsByOrderId(id);
            
            return _Mapper.Map<List<OrderItemShow>>(res);
        }

        public async Task<OrderItemShow> CreateAsync(OrderItemShow orderItemDto)
        {
            OrderItem orderItemModel = _Mapper.Map<OrderItem>(orderItemDto);
            var res = await _repository.CreateAsync(orderItemModel);
            await _repository.SaveChangesAsync();
            await productServices.decreamnteUnitInStock(res.ProductId,res.Count);
            return  _Mapper.Map<OrderItemShow>(res);
        }

        public async Task<bool> Delete(int id)
        {
            var orderItem=await _repository.GetByIdAsync(id);
            var res = await _repository.DeleteAsync(id);
            if (res)
            {
                await _repository.SaveChangesAsync();
                await productServices.IncreamnteUnitInStock(orderItem.ProductId, orderItem.Count);
                return true;
            }else { return false; }
        }

        public async Task<bool> Update(int id, OrderItemShow orderItemDto)
        {
            OrderItem orderItem = _Mapper.Map<OrderItem>(orderItemDto);
            var res=await _repository.UpdateAsync(orderItem, id);
            if (res)
            {
                await _repository.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
