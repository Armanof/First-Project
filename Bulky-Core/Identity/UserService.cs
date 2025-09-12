using Azure;
using Bulky_Core.Base;
using Bulky_Core.Messages;
using Bulky_Core.Utilities;
using Bulky_Core.Utilities.Enums;
using Bulky_Core.Validators.Identity;
using Bulky_DTO.Identity;
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
    public class UserService
        : BaseService
    {
        private readonly IUnitOfWork uow;
        private readonly ILogger<UserService> logger;

        public UserService(IUnitOfWork uow,ILogger<UserService> logger)
        {
            this.uow = uow;
            this.logger = logger;
        }

        public async Task<User?> Register(RegisterUserDTO input)
        {
            try
            {
                if (input == null)
                    return Failure(null as User, GeneralMessages.DefaultError());

                var validator = new RegisterUserValidator(uow);
                var result = validator.Validate(input);
                if (!result.IsValid)
                    return Failure(null as User, "", string.Join("<br>", result.Errors.Select(x => x.ErrorMessage)));

                if (input.ConfirmPassword != input.Password)
                    return Failure(null as User, GeneralMessages.MustEqualsTo("رمز عبور", "تاییدیه رمز عبور"));

                if (PasswordHelper.CheckPasswordStrength(input.Password) == PasswordStrengthEnum.Weak)
                    return Failure(null as User, GeneralMessages.PasswordIsWeak);

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

                return user;
            }
            catch (Exception ex)
            {
                logger.LogError($"exception Accured in Registering user with error {ex.Message}");
                return Failure(null as User, GeneralMessages.DefaultError());
            }
        }

        public async Task<User?> Login(LoginUserDTO input)
        {
            try
            {
                if (input == null)
                    return Failure(null as User, GeneralMessages.DefaultError());

                var user = await uow.Repository<User>().FirstOrDefault(x => x.PhoneNumber == input.EmailOrPhone || x.Email == input.EmailOrPhone);
                if (user == null)
                    return Failure(null as User, GeneralMessages.UserNotFound);

                var hashResult = HashTools.GetHashRfc2898(input.Password, user.SaltPassword);

                if (user.Password != hashResult.hash)
                    return Failure(null as User, GeneralMessages.PasswordIsNotCorrect);

                return user;
            }
            catch (Exception ex)
            {
                logger.LogError($"exception Accured in SignIn user with error {ex.Message}");
                return Failure(null as User, GeneralMessages.DefaultError());
            }
        }
    }
}
