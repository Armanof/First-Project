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
    public class ProductValidator
        :BaseValidator<ProductDTO>
    {

        public ProductValidator(IUnitOfWork uow) : base(uow)
        {
            RuleFor(x => x.Title)
               .NotNull().NotEmpty()
                   .WithErrorCodeAndMessage(GeneralMessages.NotNullOrEmpty("Title"));

            RuleFor(x => x.ISBN)
                .NotNull().NotEmpty()
                    .WithErrorCodeAndMessage(GeneralMessages.NotNullOrEmpty("ISBN"));

            RuleFor(x => x.Author)
                .NotNull().NotEmpty()
                    .WithErrorCodeAndMessage(GeneralMessages.NotNullOrEmpty("Author"));

            RuleFor(x => x.Price)
                .NotNull().NotEmpty()
                    .WithErrorCodeAndMessage(GeneralMessages.NotNullOrEmpty("Price"))

                .InclusiveBetween(1, 1000)
                    .WithErrorCodeAndMessage(GeneralMessages.MustBeBetween("Price", 1, 1000));
        }
    }
}
