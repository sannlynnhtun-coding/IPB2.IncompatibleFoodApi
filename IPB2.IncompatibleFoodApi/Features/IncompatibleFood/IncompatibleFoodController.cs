using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PB2.IncompatibleFood.Domain.Features.IncompatibleFood;

namespace IPB2.IncompatibleFoodApi.Features.IncompatibleFood;

[ApiController]
[Route("api/[controller]")]
public class IncompatibleFoodController : ControllerBase
{
    private readonly IncompatibleFoodService _service;

    public IncompatibleFoodController(IncompatibleFoodService service)
    {
        _service = service;
    }

    [HttpGet("List")]
    public async Task<IActionResult> GetList([FromQuery] IncompatibleFoodListRequest request)
    {
        var response = await _service.GetListAsync(request);
        return Ok(response);
    }

    [HttpGet("Search")]
    public async Task<IActionResult> Search([FromQuery] IncompatibleFoodSearchRequest request)
    {
        var response = await _service.SearchAsync(request);
        return Ok(response);
    }

    [HttpGet("CategoryList")]
    public async Task<IActionResult> GetCategories()
    {
        var response = await _service.GetCategoriesAsync(new IncompatibleFoodCategoryListRequest());
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await _service.GetByIdAsync(new GetIncompatibleFoodByIdRequest { Id = id });
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateIncompatibleFoodRequest request)
    {
        var response = await _service.CreateAsync(request);
        return Ok(response);
    }

    [HttpPut("{id}")]
    [HttpPut("Update/{id}")]
    public async Task<IActionResult> Update(int id, UpdateIncompatibleFoodRequest request)
    {
        request.Id = id;
        var response = await _service.UpdateAsync(request);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _service.DeleteAsync(new DeleteIncompatibleFoodRequest { Id = id });
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }
}
