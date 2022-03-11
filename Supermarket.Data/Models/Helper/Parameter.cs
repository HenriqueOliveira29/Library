using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Data.Models.Helper
{
    public class Parameter
    {
        public string Name { get; set; }

        public string? Value { get; set; }

        public Parameter(string Name, string Value)
        {
            this.Name = Name;
            this.Value = Value;
        }

        public static List<Parameter> VerParametros(string[] necessaryParameters, List<Parameter> searchParameters) {
            List<Parameter> list = new List<Parameter>();

            if (necessaryParameters == null) return list;

            foreach (var parameter in necessaryParameters) {
                Parameter? parameterExist = null;

                if (searchParameters != null) {
                    parameterExist = searchParameters.FirstOrDefault(p => p.Name == parameter);
                }
                list.Add(new Parameter(parameter, parameterExist != null ? parameterExist.Value : null));
            }

            return list;
        }
    }
}
