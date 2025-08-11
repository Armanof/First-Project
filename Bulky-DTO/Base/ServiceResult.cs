using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky_DTO.Base
{
    public class ServiceResult<T>
    {
        public T Data { get; set; }
        public ServiceError Error { get; set; }
        public bool IsSuccess { get; set; }

        public static ServiceResult<T> Success(T data) => new ServiceResult<T> { Data = data };
        public static ServiceResult<T> Failure(string code, string message) => new ServiceResult<T> { Error = new ServiceError(code, message) };
    }
}
