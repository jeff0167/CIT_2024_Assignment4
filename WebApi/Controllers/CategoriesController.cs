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
//    [Route("api/categories")]
    public IActionResult GetCategories() {

    var categories= _dataService.GetCategories();    
    return Ok(categories);
    }

    [HttpGet]
    [Route("/{id}")]
    public IActionResult GetCategory(int id) { 
    var category = _dataService.GetCategory(id);
        return Ok(category);
    }

        
    
}
