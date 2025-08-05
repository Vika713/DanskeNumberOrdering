using BusinessServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NumberOrderingController : ControllerBase
    {
        private readonly ISortingService _sortingService;
        private readonly INumberStorageService _numberStorageService;
        private readonly ILogger<NumberOrderingController> _logger;

        public NumberOrderingController(
            ISortingService sortingService, 
            INumberStorageService numberStorageService,
            ILogger<NumberOrderingController> logger)
        {
            _sortingService = sortingService;
            _numberStorageService = numberStorageService;
            _logger = logger;
        }

        [HttpGet(Name = "Get")]
        public async Task<ActionResult<double[]>> Get()
        {
            try
            {
                var numbers = await _numberStorageService.ReadNumbers();
                return Ok(numbers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading numbers from storage");
                return StatusCode(500, Array.Empty<double>());
            }
        }

        [HttpPost(Name = "Post")]
        public async Task<HttpResponseMessage> Post(double[] numbers)
        {
            try
            {
                var orderedNumbers = _sortingService.SortNumbers(numbers);
                await _numberStorageService.SaveNumbers(orderedNumbers);
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing numbers");
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
