using System;
using System.Threading.Tasks;
using API.Core.Application.Basket.Commands.Create;
using API.Core.Application.Basket.Queries;
using API.Core.Application.BasketItem.Command.Add;
using API.Core.Application.BasketItem.Command.Remove;
using API.Core.Application.Common.Dtos;
using API.Core.Application.Product.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "GetBasket")]
        public async Task<ActionResult<BasketDto>> Get()
        {
            if (Request.Cookies["basketId"] == null) return NotFound();
            
            var basketId = Guid.Parse(Request.Cookies["basketId"]);
            var result = await _mediator.Send(new GetBasketQuery {Id = basketId });

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<BasketDto>> AddItemToBasket(Guid productId, int quantity)
        {
            Guid basketId = Request.Cookies["basketId"] == null ? Guid.Empty : Guid.Parse(Request.Cookies["basketId"]);
            var basket = await _mediator.Send(new GetBasketQuery {Id = basketId}) ?? await CreateBasket();
            
            var product = await _mediator.Send(new GetProductByIdQuery {Id = productId});
            if (product == null) return NotFound();
            
            var result = await _mediator.Send(new AddItemBasketCommand{ ProductId = product.Id, BasketId = basket.Id, Quantity = quantity}) > 0;

            if (result) return CreatedAtRoute("GetBasket", basket);

            return BadRequest(new ProblemDetails{Title = "Problem saving item to basket"});
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveBasketItem(Guid productId, int quantity)
        {
            if (Request.Cookies["basketId"] != null)
            {
                var basket = await _mediator.Send(new GetBasketQuery {Id = Guid.Parse(Request.Cookies["basketId"])});
                if (basket == null) return NotFound();
                
                var result = await _mediator.Send(new RemoveItemCommand
                    {BasketId = basket.Id, ProductId = productId, Quantity = quantity}) > 0;
                if (result) return Ok();
                
                return BadRequest(new ProblemDetails{Title = "Problem removing item from the basket"});

            }
            return NotFound();
        }

        private async Task<BasketDto> CreateBasket()
        {
            var basketId = Guid.NewGuid();
            var cookieOptions = new CookieOptions {IsEssential = true, Expires = DateTime.UtcNow.AddDays(15)};
            Response.Cookies.Append("basketId", basketId.ToString(), cookieOptions);
            return await _mediator.Send(new CreateBasketCommand {Id = basketId});
        }
    }
}