using AspectInjector.Broker;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Film.Application.Contract.Attributes
{
    [Aspect(AspectInjector.Broker.Scope.Global)]
    [Injection(typeof(DangerousCharacter))]
    [AttributeUsage(AttributeTargets.All)]
    public class DangerousCharacter : Attribute
    {
        readonly List<string> _characters = new List<string> { "'" };
        [Advice(Kind.Before)]
        public void Validate([Argument(Source.Arguments)] object[] objects)
        {
            foreach (var item in objects)
            {

                if (item is not null && item.GetType() == typeof(string))
                {
                    if (_characters.Any(w => item.ToString().Contains(w)))
                    {
                        throw new ValidationException("Bad Request, Request Consist of dangerous character");
                    }
                }
            }
        }
    }
    [Aspect(AspectInjector.Broker.Scope.Global)]
    [Injection(typeof(DangerousCharacter))]
    [AttributeUsage(AttributeTargets.All)]
    public class RequiredValidation : Attribute
    {
        [Advice(Kind.Before)]
        public void Validate([Argument(Source.Arguments)] object[] objects)
        {
            foreach (var item in objects)
            {
                if(item is null )
                {
                    throw new ValidationException("Bad Request,"+nameof(item)+" Value is null");
                }
                if (item is not null && item.GetType() == typeof(string))
                {
                    if (String.IsNullOrEmpty(item.ToString()))
                    {
                        throw new ValidationException("Bad Request, String Value is null or empty");
                    }
                }
                if(item is not null && (item.GetType()==typeof(int)|| item.GetType() == typeof(long)))
                {
                    if(Convert.ToInt64(item)==0)
                    throw new ValidationException("Bad Request, numeric value is 0");
                }
            }
        }
    }

}
