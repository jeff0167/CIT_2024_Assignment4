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
    private readonly LinkGenerator _linkGenerator;

    public ProductsController(DataService dataService, LinkGenerator linkGenerator)
    {
        _dataService = dataService;
        _linkGenerator = linkGenerator;
    }

    [HttpGet("category/{categoryId}", Name = nameof(GetProductByCategory))]
    public IActionResult GetProductByCategory(int categoryId)
    {
        var productList = _dataService.GetProductByCategory(categoryId)
            .Select(CreateProductModel);
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

    [HttpGet("{id}", Name = nameof(GetProduct))]

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
        var productModel = product.Adapt<ProductModel>();
        productModel.Url = GetUrl(product.Id);
        return productModel;

    }

    private string? GetUrl(int id)
    {
        return _linkGenerator.GetUriByName(HttpContext, nameof(GetProduct), new { id });
    }
}

