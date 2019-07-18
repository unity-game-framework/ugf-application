using System;
using System.Collections.Generic;
using UGF.Initialize.Runtime;

namespace UGF.Application.Runtime
{
    /// <summary>
    /// Represents an application with modules.
    /// </summary>
    public interface IApplication : IInitialize
    {
        /// <summary>
        /// Gets the collection of the all modules.
        /// </summary>
        IReadOnlyDictionary<Type, IApplicationModule> Modules { get; }

        /// <summary>
        /// Adds the specified module by the register type.
        /// </summary>
        /// <param name="registerType">The type to register module.</param>
        /// <param name="module">The module to register.</param>
        void AddModule(Type registerType, IApplicationModule module);

        /// <summary>
        /// Removes a module by the specified register type.
        /// </summary>
        /// <param name="registerType">The type that has been used to register module, which required to remove.</param>
        bool RemoveModule(Type registerType);

        /// <summary>
        /// Removes all modules.
        /// </summary>
        void ClearModules();

        /// <summary>
        /// Gets the module by the specified register type.
        /// </summary>
        T GetModule<T>() where T : IApplicationModule;

        /// <summary>
        /// Tries to get module by the specified register type.
        /// </summary>
        /// <param name="module">The found module.</param>
        bool TryGetModule<T>(out T module) where T : IApplicationModule;

        /// <summary>
        /// Gets the type that has been used to register the specified module.
        /// </summary>
        /// <param name="module">The module.</param>
        Type GetRegisterType(IApplicationModule module);

        /// <summary>
        /// Tries to get the type that has been used to register the specified module.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <param name="registerType">The found register type.</param>
        bool TryGetRegisterType(IApplicationModule module, out Type registerType);
    }
}
