using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky_Core.Base
{
    public abstract class BaseService
    {
        public string errorCode;
        public string errorMessage;

        protected T Failure<T>(T result, string errorCode, string errorMessage)
        {
            this.errorCode = errorCode;
            this.errorMessage = errorMessage;
            return result;
        }

        protected T Failure<T>(T result, (string errorCode, string errorMessage) error)
        {
            this.errorCode = error.errorCode;
            this.errorMessage = error.errorMessage;
            return result;
        }
    }
}
