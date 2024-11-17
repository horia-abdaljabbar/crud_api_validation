using crud_task.Data;
using crud_task.Data.Models;
using crudTask.DTOs.Product;
using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace crud_task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ProductsController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var products = await context.Products.ToListAsync();
            var response =  products.Adapt<IEnumerable<GetAllProductsDto>>();
            return Ok(response);
        }


        [HttpGet("Details")]
        public async Task<IActionResult>GetByIdAsync(int id)
        {
            var product = await context.Products.FindAsync(id);
            if (product is null)
            {
                return NotFound();
            }
            var response = product.Adapt<GetProductByIdDto>();

            return Ok(response);
        }


        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync(CreateProductDto productDto,
    [FromServices] IValidator<CreateProductDto> validator)
        {
            var validationResult = validator.Validate(productDto);
            if (!validationResult.IsValid)
            {
                var modelState = new ModelStateDictionary();
                foreach (var error in validationResult.Errors)
                {
                    modelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return ValidationProblem(modelState);
            }

            var product = productDto.Adapt<Product>(); // Remove the `await`

            context.Products.Add(product);
            await context.SaveChangesAsync(); // Save changes asynchronously

            return Ok(product);
        }



        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateProductDto productDto)
        {
            var current =  await context.Products.FindAsync(id);
            if (current is null)
            {
                return NotFound("product not found");

            }
            current.Name = productDto.Name;
            current.Price = productDto.Price;
            current.Description = productDto.Description;
            current.Adapt(productDto);

            context.SaveChanges();

            var updatedProductDto = current.Adapt<UpdateProductDto>();
            return Ok(updatedProductDto);
        }


        [HttpDelete("Remove")]
        public async Task<IActionResult> Remove(int id)
        {
            var product =await context.Products.FindAsync(id);
            if (product is null)
            {
                return NotFound("employee not found");
            }
            context.Products.Remove(product);
            product.Adapt<DeleteProductDto>();
            context.SaveChanges();
            return Ok("the product deleted successfully");
        }
    }
}
