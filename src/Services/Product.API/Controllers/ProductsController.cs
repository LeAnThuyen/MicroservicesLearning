using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product.API.Entities;
using Product.API.Repositories.Interfaces;
using Shared.DTOs.Products;
using System.ComponentModel.DataAnnotations;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var products = await _repository.FindAll().ToListAsync();
            var result = _mapper.Map<IEnumerable<ProductDto>>(products);
            return result;
        }
        [HttpGet("{Id:long}")]
        public async Task<IActionResult> GetProduct(long Id)
        {
            var product = await _repository.GetProductAsync(Id);
            if (product == null)
            {
                return NotFound();
            }
            var result = _mapper.Map<ProductDto>(product);
            return Ok(result);
        }
        [HttpPost]

        public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductDto createProductDto)
        {
            var product = _mapper.Map<CatalogProduct>(createProductDto);
            await _repository.CreateProductAsync(product);
            await _repository.SaveChangesAsync();
            var result = _mapper.Map<ProductDto>(product);
            return Ok(result);

        }

        [HttpPut("{Id:long}")]
        public async Task<IActionResult> UpdateProductAsync(int Id, [FromBody] UpdateProductDto updateProductDto)
        {
            var product = await this._repository.GetByIdAsync(Id);
            if (product == null)
            {
                return NotFound();
            }
            var updateProducts = _mapper.Map(updateProductDto, product);
            await _repository.UpdateProductAsync(product);
            await _repository.SaveChangesAsync();
            var result = _mapper.Map<ProductDto>(product);
            return Ok(result);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteProduct([Required] long id)
        {
            var product = await _repository.GetProductAsync(id);
            if (product == null)
                return NotFound();

            await _repository.DeleteProductAsync(id);
            await _repository.SaveChangesAsync();
            return NoContent();
        }





        [HttpGet("get-product-by-no/{productNo}")]
        public async Task<IActionResult> GetProductByNo([Required] string productNo)
        {
            var product = await _repository.GetProductByNoAsync(productNo);
            if (product == null)
                return NotFound();

            var result = _mapper.Map<ProductDto>(product);
            return Ok(result);
        }


    }
}

