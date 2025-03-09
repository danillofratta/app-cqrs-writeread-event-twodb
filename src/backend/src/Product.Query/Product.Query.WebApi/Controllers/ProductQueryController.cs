using AutoMapper;
using Base.WebApi;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Query.Application.GetAll;
using Product.Query.Application.GetById;
using Product.Query.Application.GetByName;
using Product.Query.Domain.Repository;
using System.Threading;

namespace ApiStock.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductQueryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductQueryController(IMapper mapper, IMediator mediator)
        {
            this._mediator = mediator;
            this._mapper = mapper;
        }

        /// <summary>
        /// TODO return with ApiResponseWithData
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                var response = await _mediator.Send(new GetAllProductQuery(), cancellationToken);

                return Ok(new ApiResponseWithData<List<GetAllProductQueryResult>>
                {
                    Success = true,
                    Message = "Product retrieved successfully",
                    Data = response
                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponseWithData<GetAllProductQueryResult>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        /// <summary>
        /// TODO return with ApiResponseWithData
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            try
            {                
                var response = await _mediator.Send(new GetProductByIdQuery(id), cancellationToken);

                return Ok(new ApiResponseWithData<GetProductByIdQueryResult>
                {
                    Success = true,
                    Message = "Product retrieved successfully",
                    Data = _mapper.Map<GetProductByIdQueryResult>(response)
                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponseWithData<GetProductByIdQueryResult>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        /// <summary>
        /// TODO return with ApiResponseWithData
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("getbyname/{name}")]
        public async Task<IActionResult> GetByName([FromRoute] string name, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _mediator.Send(new GetProductByNameQuery(name), cancellationToken);

                return Ok(new ApiResponseWithData<List<GetProductByNameQueryResult>>
                {
                    Success = true,
                    Message = "Product retrieved successfully",
                    Data = _mapper.Map<List<GetProductByNameQueryResult>>(response)
                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponseWithData<GetProductByNameQueryResult>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }
    }
}
