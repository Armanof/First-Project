using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky_Core.Messages
{
    public class GeneralMessages
    {
        public static (string Code, string Message) NotNullOrEmpty(string fieldName)
        {
            return ("G-0001", $"فیلد {fieldName} نمی تواند خالی باشد.");
        }

        public static (string Code, string Message) OnlyNumbers(string fieldName)
        {
            return ("G-0002", $"فیلد {fieldName} فقط کاراکتر اعداد میپذیرد");
        }

        public static (string Code, string Message) OnlyAlphabets(string fieldName)
        {
            return ("G-0003", $"فیلد {fieldName} فقط کاراکتر حروف میپذیرد");
        }

        public static (string Code, string Message) MaxLength(string fieldName, int maxLength)
        {
            return ("G-0005", $"تعداد کاراکترهای وارد شده بیشتر از حد مجاز {maxLength} برای فیلد {fieldName} میباشد. ");
        }
        public static (string Code, string Message) MinLength(string fieldName, int maxLength)
        {
            return ("G-0006", $"تعداد کاراکترهای وارد شده کمتر از حد مجاز {maxLength} برای فیلد {fieldName} میباشد. ");
        }

        public static (string Code, string Message) DefaultError()
        {
            return ("G-0007", "خطا در انجام فرایند");
        }

        public static (string Code, string Message) CantBeLessThan(string fieldName,int number)
        {
            return ("G-0008", $"فیلد {fieldName} نمی تواند از {number} کوچکتر باشد.");
        }

        public static (string Code, string Message) MustBeUnique(string fieldName)
        {
            return ("G-0009", $"فیلد {fieldName} میبایستی یکتا باشد.");
        }

        public static (string Code, string Message) MustBeBetween(string fieldName, int min,int max)
        {
            return ("G-0010", $"فیلد {fieldName} میبایستی بین {min} و {max} باشد.");
        }

        public static (string Code, string Message) MustMatch(string fieldName, string? pattern)
        {
            return ("G-0011", $"فیلد {fieldName} از الگو {pattern} تبعیت نمی کند");
        }

        public static (string Code, string Message) MustEqualsTo(string fieldName, string expression)
        {
            return ("G0012", $"فیلد {fieldName} باید با {expression} برابر باشد");
        }

        public static (string Code,string Message) PasswordIsWeak { get => ("G-0013", "رمز عبور مقاوم نیست."); }
        public static (string Code,string Message) PhoneNumberAndEmailCantBeEmpty { get => ("G-0014", "ایمیل یا شماره تلفن میبایستی خالی نباشد"); }
        public static (string Code, string Message) Success { get => ("G-0015", "عملیات با موفقیت انجام شد"); }
        public static (string Code, string Message) UserNotFound { get => ("G-0016", "کاربر یافت نشد"); }
        public static (string Code, string Message) PasswordIsNotCorrect { get => ("G-0017", "رمز عبور اشتباه میباشد"); }
        public static (string Code, string Message) UnAuthorized { get => ("G-0018", "عملیات انتخاب شده قابل دسترسی نمیباشد"); }
    }
}
