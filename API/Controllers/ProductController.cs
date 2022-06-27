using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Core.Application.Common.Dtos;
using API.Core.Application.Product.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        
        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAllAsync()
        {
            return Ok(await _mediator.Send(new GetAllProductQuery()));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductDto>> GetByIdAsync(Guid id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery() {Id = id});
            
            if (product == null) return NotFound();
            
            return Ok(product);
        }
    }
}