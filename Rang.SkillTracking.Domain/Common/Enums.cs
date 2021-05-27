using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rang.SkillTracking.Domain.Common
{
    public enum OperationStatusCode
    {
        NotSet,
        Success,
        MissingRomanSymbol,
        InvalidArabicSymbol,
        DuplicatedSymbol
    }

    public enum SkillLevel
    {
        NotSet,
        Noob,
        Average,
        Advanced,
        Expert,
        Guru
    }
}
