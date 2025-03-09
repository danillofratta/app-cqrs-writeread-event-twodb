using AutoMapper;
using Base.WebApi;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Command.Application.Create;
using Product.Command.Application.Delete;
using Product.Command.Application.Modify;
using Product.Command.Repository;

namespace ApiStock.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductCommandController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductCommandController(IMapper mapper, IMediator mediator)
        {
            this._mediator = mediator;
            this._mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);

                return Created(string.Empty, new ApiResponseWithData<CreateProductResult>
                {
                    Success = true,
                    Message = "Product created successfully",
                    Data = _mapper.Map<CreateProductResult>(response)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseWithData<CreateProductResult>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(ModifyProductCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);

                return Created(string.Empty, new ApiResponseWithData<ModifyProductResult>
                {
                    Success = true,
                    Message = "Product modified successfully",
                    Data = _mapper.Map<ModifyProductResult>(response)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseWithData<ModifyProductResult>
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var obj = new DeleteProductCommand { Id = id };
            var result = await _mediator.Send(obj);
            return Ok(result);
        }
    }
}
