using System;
using System.Threading.Tasks;
using API.Core.Application.Basket.Commands.Create;
using API.Core.Application.Basket.Commands.Update;
using API.Core.Application.Basket.Queries;
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

        [HttpGet]
        public async Task<ActionResult<BasketDto>> Get()
        {
            if (Request.Cookies["buyerId"] == null) return NotFound();
            
            var buyerId = Guid.Parse(Request.Cookies["buyerId"]);
            var result = await _mediator.Send(new GetBasketQuery {BuyerId = buyerId });

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddItemToBasket(Guid productId, int quantity)
        {
            BasketDto basket;
            if (Request.Cookies["buyerId"] == null) basket = await CreateBasket();
            basket = await _mediator.Send(new GetBasketQuery {BuyerId = Guid.Parse(Request.Cookies["buyerId"]!)}) ?? await CreateBasket();
            var product = await _mediator.Send(new GetProductByIdQuery {Id = productId});
            if (product == null) return NotFound();
            var result = await _mediator.Send(new AddItemBasketCommand{ ProductId = product.Id, BasketId = basket.Id, Quantity = quantity}) > 0;

            if (result) return StatusCode(201);
            
            return BadRequest(new ProblemDetails{Title = "Problem saving item to basket"});
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveBasketItem(Guid productId, int quantity)
        {
            return Ok();
        }

        private async Task<BasketDto> CreateBasket()
        {
            var buyerId = Guid.NewGuid();
            var cookieOptions = new CookieOptions {IsEssential = true, Expires = DateTime.UtcNow.AddDays(30)};
            Response.Cookies.Append("buyerId", buyerId.ToString(), cookieOptions);
            return await _mediator.Send(new CreateBasketCommand {BuyerId = buyerId});
        }
    }
}