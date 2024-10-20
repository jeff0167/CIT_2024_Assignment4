﻿using DataLayer;
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



    public CategoriesController(DataService dataService, LinkGenerator linkGenerator)
    {
        _dataService = dataService;
        _linkGenerator = linkGenerator; //Dependency injection
    }

    [HttpGet] //custom action
    public IActionResult GetCategories()
    {

        var categories = _dataService.GetCategories()
            .Select(CreateCategoryModel); //Generate URL for all Categories
        if (categories != null)
        {
            return Ok(categories);
        }
        return NotFound();


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



    [HttpPost]
    public IActionResult PostCategory(CreateCategoryModel model)
    {

        var category = _dataService.CreateCategory(model.Name, model.Description);

        var categoryModel = CreateCategoryModel(category);

        //    = category.Adapt<CategoryModel>(); //Using Mapster to map the Category object to a CategoryModel object, dependency added via nugget (for WebAPI). Less boilercode
        //categoryModel.Url = GetUrl(category.Id);

        if (category != null)
        {
            return Created(categoryModel.Url, categoryModel);
        }
        return BadRequest();

    }

    [HttpPut("{id}")]
    public IActionResult PutCategory(int id, CreateCategoryModel model)
    {
        var category = _dataService.UpdateCategory(id, model.Name, model.Description);

        if (category)
        {
            return Ok();
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


    //HELPER METHODS:
    private string? GetUrl(int id)
    {
        return _linkGenerator.GetUriByName(HttpContext, nameof(GetCategory), new { id }); //Generates the URL for the category with the given id.
    }

    private CategoryModel? CreateCategoryModel(Category? category)
    {
        if (category == null) return null;

        var categoryModel = category.Adapt<CategoryModel>(); //Using Mapster to map the Category object to a CategoryModel object, dependency added via nugget (for WebAPI). Less boilercode
        categoryModel.Url = GetUrl(category.Id);
        return categoryModel;


    }

}
