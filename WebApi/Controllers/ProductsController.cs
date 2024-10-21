using DataLayer;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    DataService _dataService;
    private readonly LinkGenerator _linkGenerator; //Used for generating URLs

    //DataService _dataService = new DataService();

    public ProductsController(DataService dataService, LinkGenerator linkGenerator)
    {
        _dataService = dataService;
        _linkGenerator = linkGenerator;
    }

    [HttpGet("category/{categoryId}", Name = nameof(GetProductByCategory))] //Nice to have, don't hardcode path
    public IActionResult GetProductByCategory(int categoryId)
    {
        var productList = _dataService.GetProductByCategory(categoryId)
            .Select(CreateProductModel); //Generate URL for all Products



        if (productList.Count() > 0)
        {
            return Ok(productList);
        }
        return NotFound(productList);
    }

    [HttpGet]
    public IActionResult GetProductByName(string name)
    {
        var productList = _dataService.GetProductByName(name)
            .Select(CreateProductModel);
        if (productList.Count() > 0)
        {
            return Ok(productList);
        }
        return NotFound(productList);
    }

    [HttpGet("{id}", Name = nameof(GetProduct))] //Name parameter gives this route its name, we can thus reference this route by that name in GetUrl method. 

    public IActionResult GetProduct(int id)
    {
        var product = _dataService.GetProduct(id);

        if (product != null)
        {
            var productModel = CreateProductModel(product);
            return Ok(productModel);
        }
        return NotFound();

    }

    private ProductModel? CreateProductModel(Product? product)
    {
        if (product == null) return null;

        var productModel = product.Adapt<ProductModel>(); //Using Mapster to map the Product object to a ProductModel object, dependency added via nugget (for WebAPI). Less boilercode
        productModel.Url = GetUrl(product.Id);
        return productModel;

    }

    private string? GetUrl(int id)
    {
        return _linkGenerator.GetUriByName(HttpContext, nameof(GetProduct), new { id }); //Generates the URL for the product with the given id.
    }
}

