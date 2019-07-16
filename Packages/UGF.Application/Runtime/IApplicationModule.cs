using System;
using UGF.Initialize.Runtime;

namespace UGF.Application.Runtime
{
    public interface IApplicationModule : IInitialize
    {
        Type RegisterType { get; }
    }
}
