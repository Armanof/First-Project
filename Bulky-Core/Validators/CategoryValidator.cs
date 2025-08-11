using Bulky_Core.Interfaces;
using Bulky_Core.Messages;
using Bulky_DTO;
using Bulky_DTO.Utilities;
using Bulky_Infrastructure.Interfaces;
using Bulky_Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky_Core.Validators
{
    public class CategoryValidator 
        : BaseValidator<CategoryDTO>
    {
        public CategoryValidator(IUnitOfWork uow) : base(uow)
        {

            RuleFor(x => x.Name)
                .NotNull().NotEmpty()
                    .WithErrorCodeAndMessage(GeneralMessages.NotNullOrEmpty("عنوان"))

                .MaximumLength(100)
                    .WithErrorCodeAndMessage(GeneralMessages.MaxLength("عنوان", 100));

            RuleFor(x => x.DisplayOrder)
                .NotNull().NotEmpty()
                    .WithErrorCodeAndMessage(GeneralMessages.NotNullOrEmpty("ترتیب نمایش"))

                .GreaterThan(0)
                    .WithErrorCodeAndMessage(GeneralMessages.CantBeLessThan("ترتیب نمایش", 0))

                .MustAsync(async (dto,displayOrder, cancellation) =>
                            await DisplayOrderMustBeUnique(displayOrder,dto))
                    .WithErrorCodeAndMessage(GeneralMessages.MustBeUnique("ترتیب نمایش"));
        }

        public async Task<bool> DisplayOrderMustBeUnique(int displayOrder,CategoryDTO input)
        {
            return !await uow.Repository<Category>().Any(x => x.DisplayOrder == displayOrder && x.Id != input.Id);
        }
    }
}
