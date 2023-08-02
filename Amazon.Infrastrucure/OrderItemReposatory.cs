using Amazon.Application.Contracts;
using Amazon.Context;
using Amazon.Domain;
using Amazon.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Infrastrucure
{
    public class OrderItemReposatory : Reposatory<OrderItem, int>, IOrderItemReposatory
    {
        private readonly ApplicationContext context;
        private readonly DbSet<OrderItem> dbset ;

        public OrderItemReposatory(ApplicationContext context) : base(context)
        {
            this.context = context;
            dbset = this.context.Set<OrderItem>();
        }

        public async Task<bool> deleteOrderItemsByOrderId(int id)
        {
            var orderItems = await dbset.Where(O => O.OrderId == id).ToListAsync();
            foreach(var item in orderItems)
            {
                dbset.Remove(item);
                await SaveChangesAsync();
            }
            return true;
        }

        public async Task<List<OrderItem>> getOrderItemsByOrderId(int id)
        {
            var orderItems = await dbset.Where(O => O.OrderId == id).ToListAsync();
            return orderItems;
        }
    }
}
