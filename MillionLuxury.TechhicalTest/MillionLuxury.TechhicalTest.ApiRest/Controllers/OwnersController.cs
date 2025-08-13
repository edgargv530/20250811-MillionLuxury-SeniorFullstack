using Microsoft.AspNetCore.Mvc;
using MillionLuxury.TechhicalTest.ApiRest.Extensions;
using MillionLuxury.TechhicalTest.Application.Models.Owners;
using MillionLuxury.TechhicalTest.Application.UseCases.Owners;

namespace MillionLuxury.TechhicalTest.ApiRest.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route($"api/[controller]")]
    public class OwnersController : ControllerMillionBase
    {
        private readonly IOwnersUseCase _ownersUseCase;

        public OwnersController(
            ILoggerFactory loggerFactory,
            IOwnersUseCase ownersUseCase) : base(loggerFactory)
        {
            _ownersUseCase = ownersUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Add(OwnerModel ownerModel)
        {
            try
            {
                var ownerAdded = await _ownersUseCase.Add(ownerModel);
                return Ok(ownerAdded);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            try
            {
                var queryRequest = Request.Query.GetODataRequest();
                var owners = await _ownersUseCase.GetData(queryRequest);
                return Ok(owners);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var owner = await _ownersUseCase.GetById(id);
                return Ok(owner);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(OwnerModel ownerModel)
        {
            try
            {
                var ownerAdded = await _ownersUseCase.Update(ownerModel);
                return Ok(ownerAdded);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _ownersUseCase.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}
