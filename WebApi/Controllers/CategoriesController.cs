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
    public IActionResult Index() {

    var categories= _dataService.GetCategories();    
    return Ok(categories);
    }
        
    
}
