using DataLayer;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController:ControllerBase
{
    
    DataService _dataService = new DataService();
    [HttpGet]
    public IActionResult GetCategories() {

    var categories= _dataService.GetCategories();    
    return Ok(categories);
    }

    [HttpGet("{id}")]

    public IActionResult GetCategory(int id) { 
    var category = _dataService.GetCategory(id);

        if (category != null) {
            return Ok(category);
        }
        return NotFound();
        
    }



    [HttpPost]
    public IActionResult PostCategory([FromBody]CreateCategoryModel model) {

        _dataService.CreateCategory(model.Name, model.Description);
        return Created();

    }
        
    
}
