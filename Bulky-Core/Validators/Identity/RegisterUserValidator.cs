using Bulky_Core.Messages;
using Bulky_Core.Utilities;
using Bulky_DTO.Account;
using Bulky_Infrastructure.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bulky_Core.Validators.Identity
{
    public class RegisterUserValidator
        :BaseValidator<RegisterUserDTO>
    {
        public RegisterUserValidator(IUnitOfWork uow) : base(uow)
        {
            RuleFor(x => new { x.Email, x.PhoneNumber })
                .Must(x =>
                {
                    if (string.IsNullOrEmpty(x.Email) && !string.IsNullOrEmpty(x.PhoneNumber))
                    {
                        return Regex.IsMatch(x.PhoneNumber, "^(0|0098|\\+98)?9(0[1-5]|[1 3]\\d|2[0-3]|9[0-9]|41)\\d{7}$\r\n");
                    }

                    return true;
                }).WithErrorCodeAndMessage(GeneralMessages.MustMatch("شماره همراه", "^(0|0098|\\+98)?9(0[1-5]|[1 3]\\d|2[0-3]|9[0-9]|41)\\d{7}$\r\n"))
                .Must(x =>
                {
                    if (!string.IsNullOrEmpty(x.Email) && string.IsNullOrEmpty(x.PhoneNumber))
                    {
                        return Regex.IsMatch(x.Email, "^[^\\s@]+@[^\\s@]+\\.[^\\s@]{2,}$\r\n");
                    }

                    return true;
                }).WithErrorCodeAndMessage(GeneralMessages.MustMatch("ایمیل", "^[^\\s@]+@[^\\s@]+\\.[^\\s@]{2,}$\r\n"))
                .Must(x =>
                {
                    if (!string.IsNullOrEmpty(x.Email) && !string.IsNullOrEmpty(x.PhoneNumber))
                    {
                        return false;
                    }

                    return true;
                }).WithErrorCodeAndMessage(GeneralMessages.PhoneNumberAndEmailCantBeEmpty);

            RuleFor(x => x.Name)
                .NotEmpty().WithErrorCodeAndMessage(GeneralMessages.NotNullOrEmpty("نام کامل"))
                .MaximumLength(512).WithErrorCodeAndMessage(GeneralMessages.MaxLength("نام کامل", 512));
        }
    }
}
