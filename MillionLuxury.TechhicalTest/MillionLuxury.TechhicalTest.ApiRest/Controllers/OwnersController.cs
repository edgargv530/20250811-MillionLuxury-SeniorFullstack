using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Get()
        {
            return Ok("Ok");
        }
    }
}
