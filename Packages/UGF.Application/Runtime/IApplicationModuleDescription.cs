using System;
using UGF.Description.Runtime;

namespace UGF.Application.Runtime
{
    public interface IApplicationModuleDescription : IDescription
    {
        Type RegisterType { get; }
    }
}
