using ElasticcSearchSample.Service;
using ElasticSearchSample.Models;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearchSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CostumerController(ICostumerService costumerService) : ControllerBase
    {
        private readonly ICostumerService _costumerService = costumerService;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var costumers = await _costumerService.GetAllAsync();
                return Ok(costumers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var costumer = await _costumerService.GetAsync(id);
                return Ok(costumer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(Costumer costumer)
        {
            try
            {
                var result = await _costumerService.AddAsync(costumer);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("create-index")]
        public async Task<IActionResult> CreateIndex(Costumer costumer)
        {
            try
            {
                var result = await _costumerService.CreateIndexAsync(costumer);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(string id, Costumer costumer)
        {
            try
            {
                var result = await _costumerService.UpdateAsync(id, costumer);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("search/{term}")]
        public async Task<IActionResult> SearchInAllFields(string term)
        {
            try
            {
                var result = await _costumerService.SearchInAllFields(term);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _costumerService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
