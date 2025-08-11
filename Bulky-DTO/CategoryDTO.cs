using Bulky_DTO.Base;
using Bulky_DTO.Utilities;
using FluentValidation;

namespace Bulky_DTO
{
    public class CategoryDTO
        : BaseDTO
    {
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime InsertedDateTime { get; set; }
    }
}
