using DataLayer;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    string URL = "api/categories";

    DataService _dataService = new DataService();

    [HttpGet]
    public IActionResult GetCategories()
    {
        var categories = _dataService.GetCategories();
        if (categories != null)
        {
            return Ok(categories);
        }
        return NotFound();
    }

    [HttpGet("{id}")]
    public IActionResult GetCategory(int id)
    {
        var category = _dataService.GetCategory(id);

        if (category != null)
        {
            return Ok(category);
        }
        return NotFound();
    }

    [HttpPost]
    public IActionResult PostCategory([FromBody] CreateCategoryModel model)
    {
        var categories = _dataService.CreateCategory(model.Name, model.Description);

        if (categories != null)
        {
            return Created(URL + "/" + categories.Id, categories); // ctrl + shift + space     to show all overloads
        }
        return BadRequest();
    }

    [HttpPut("{id}")]
    public IActionResult PutCategory(int id, [FromBody] CreateCategoryModel model)
    {
        var category = _dataService.UpdateCategory(id, model.Name, model.Description);

        if (category)
        {
            return Ok(category);
        }
        return NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCategory(int id)
    {
        var category = _dataService.DeleteCategory(id);

        if (category)
        {
            return Ok(category);
        }
        return NotFound();
    }
}
