using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film.Application.Base
{
    public class BusinessException:Exception
    {
        public BusinessExceptionType ExceptionType{ get; private set; }
        public BusinessException(string message, BusinessExceptionType exceptionType):base(message) {
            ExceptionType = exceptionType;
        }
    }
}
