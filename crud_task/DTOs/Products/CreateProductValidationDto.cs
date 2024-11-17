using crudTask.DTOs.Product;
using FluentValidation;

namespace crud_task.DTOs.Products
{
    public class CreateProductValidationDto:AbstractValidator<CreateProductDto>
    {
        public CreateProductValidationDto()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required!");
            RuleFor(x => x.Name).MaximumLength(30).WithMessage("the maximum length for the name is 20!");
            RuleFor(x => x.Name).MinimumLength(5).WithMessage("the Minimum length for the name is 20!");

            RuleFor(x => x.Price).NotEmpty().WithMessage("Price is required!");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be positive number!");
            RuleFor(x => x.Price).ExclusiveBetween(20,3000).WithMessage("the Price must be a number between 20 and 3000");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required!");
            RuleFor(x => x.Description).MinimumLength(10).WithMessage("the minimum length for the Description is 10!");

        }
    }
}
