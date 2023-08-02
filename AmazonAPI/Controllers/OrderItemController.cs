using Amazon.Application.Contracts;
using Amazon.Application.Services;
using Amazon.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace AmazonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService orderService;

        public OrderItemController(IOrderItemService orderService)
        {
            this.orderService = orderService;
        }
        [HttpGet]
        [Route("AllItemsByOrderId")]
        public async Task<IActionResult> AllItems(int id )
        {
            List<OrderItemShow> orderItems = await orderService.getOrderItemsByOrderId(id);
            return Ok(orderItems);
        }
        [HttpPost]
        [Route("CreateItem")]
        public async Task<IActionResult> Create(OrderItemShow orderItemShow)
        {
            var res = await orderService.CreateAsync(orderItemShow);
            return Ok(res);
        }
        [HttpPut]
        [Route("UpdateItem")]
        public async Task<IActionResult> Update(int id,OrderItemShow orderItemShow)
        {
            var res = await orderService.Update(id, orderItemShow);
            return Ok(res);
        }
        [HttpDelete]
        [Route("DeleteItem/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await orderService.Delete(id);
            return Ok(res);
        }
    }
}
