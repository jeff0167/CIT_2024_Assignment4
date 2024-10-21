using DataLayer;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    DataService _dataService;
    private readonly LinkGenerator _linkGenerator; //Used for generating URLs

    //DataService _dataService = new DataService();

    public CategoriesController(DataService dataService, LinkGenerator linkGenerator)
    {
        _dataService = dataService;
        _linkGenerator = linkGenerator;
    }

    [HttpGet] //custom action
    public IActionResult GetCategories()
    {

        var categories = _dataService.GetCategories()
            .Select(CreateCategoryModel); //Generate URL for all Categories
        return Ok(categories);
    }

    [HttpGet("{id}", Name = nameof(GetCategory))] //Name parameter gives this route its name, we can thus reference this route by that name in GetUrl method. 

    public IActionResult GetCategory(int id)
    {
        var category = _dataService.GetCategory(id);

        if (category != null)
        {
            var categoryModel = CreateCategoryModel(category);
            return Ok(categoryModel);
        }
        return NotFound();

    }

    private CategoryModel? CreateCategoryModel(Category? category)
    {
        if (category == null) return null;

        var categoryModel = category.Adapt<CategoryModel>(); //Using Mapster to map the Category object to a CategoryModel object, dependency added via nugget (for WebAPI). Less boilercode
        categoryModel.Url = GetUrl(category.Id);
        return categoryModel;


    }

    [HttpPost]
    public IActionResult PostCategory([FromBody] CreateCategoryModel model)
    {

        var category = _dataService.CreateCategory(model.Name, model.Description);

        var categoryModel = category.Adapt<CategoryModel>(); //Using Mapster to map the Category object to a CategoryModel object, dependency added via nugget (for WebAPI). Less boilercode
        categoryModel.Url = GetUrl(category.Id);
        //return Created(CreateCategoryModel(category));
        return Created(categoryModel.Url, categoryModel);
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


    [HttpPut("{id}")]
    public IActionResult UpdateCategory(int id, UpdateCategoryModel model)
    {
        var category = _dataService.GetCategory(id);

        if (category == null)
        {
            return NotFound();
        }



        category.Name = model.Name;
        category.Description = model.Description;


        _dataService.UpdateCategory(id, model.Name, model.Description);

        var categoryModel = category.Adapt<CategoryModel>(); //Using Mapster to map the Category object to a CategoryModel object, dependency added via nugget (for WebAPI). Less boilercode
        categoryModel.Url = GetUrl(category.Id);

        return Ok(categoryModel);



    }

    private string? GetUrl(int id)
    {
        return _linkGenerator.GetUriByName(HttpContext, nameof(GetCategory), new { id }); //Generates the URL for the category with the given id.
    }


}
