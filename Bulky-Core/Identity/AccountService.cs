using Bulky_Core.Base;
using Bulky_Core.Messages;
using Bulky_Core.Utilities;
using Bulky_Core.Utilities.Enums;
using Bulky_Core.Validators.Identity;
using Bulky_DTO.Account;
using Bulky_Infrastructure.Interfaces;
using Bulky_Models.Identity;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky_Core.Identity
{
    public class AccountService
        : BaseService
    {
        private readonly IUnitOfWork uow;
        private readonly ILogger<AccountService> logger;

        public AccountService(IUnitOfWork uow,ILogger<AccountService> logger)
        {
            this.uow = uow;
            this.logger = logger;
        }

        public async Task<bool> Register(RegisterUserDTO input)
        {
            try
            {
                if (input == null)
                    return Failure(false, GeneralMessages.DefaultError());

                var validator = new RegisterUserValidator(uow);
                var result = validator.Validate(input);
                if (!result.IsValid)
                    return Failure(false, "", string.Join("<br>", result.Errors.Select(x => x.ErrorMessage)));

                if (input.ConfirmPassword != input.Password)
                    return Failure(false, GeneralMessages.MustEqualsTo("رمز عبور", "تاییدیه رمز عبور"));

                if (PasswordHelper.CheckPasswordStrength(input.Password) == PasswordStrengthEnum.Weak)
                    return Failure(false, GeneralMessages.PasswordIsWeak);

                var hashResult = HashTools.GetHashRfc2898(input.Password);

                var user = new User()
                {
                    Email = input.Email,
                    Name = input.Name,
                    Password = hashResult.hash,
                    PhoneNumber = input.PhoneNumber,
                    SaltPassword = hashResult.salt
                };

                uow.Repository<User>().Add(user);

                await uow.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError($"exception Accured in Registering user with error {ex.Message}");
                return Failure(false, GeneralMessages.DefaultError());
            }
        }
    }
}
