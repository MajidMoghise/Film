using Film.Application.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film.Infrastructure.Persistance.Base
{
    public class PersisException : BusinessException
    {
        public PersisException(string message, BusinessExceptionType exceptionType) : base(message, exceptionType)
        {
           
        }
    }
}
