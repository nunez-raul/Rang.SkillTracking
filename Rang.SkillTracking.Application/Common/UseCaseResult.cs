using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rang.SkillTracking.Application.Common
{
    public class UseCaseResult<TOutputModel>
    {
        public UseCaseResultStatusCode StatusCode { get; set; }
        //public IDictionary<ModelValidationStatusCode, List<string>> ModelValidationErrors { get; set; }
        public TOutputModel OutputModel { get; internal set; }
    }
}
