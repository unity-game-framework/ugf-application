using System;
using System.Threading.Tasks;
using UGF.Initialize.Runtime;

namespace UGF.Application.Runtime
{
    /// <summary>
    /// Represents an application with modules.
    /// </summary>
    public interface IApplication : IInitialize
    {
        /// <summary>
        /// Initializing created modules which require additional asynchronous initialization.
        /// </summary>
        Task InitializeAsync();

        /// <summary>
        /// Adds the specified module by the register type.
        /// </summary>
        /// <param name="module">The module to register.</param>
        void AddModule<T>(IApplicationModule module) where T : class, IApplicationModule;

        /// <summary>
        /// Adds the specified module by the register type.
        /// </summary>
        /// <param name="registerType">The type to register module.</param>
        /// <param name="module">The module to register.</param>
        void AddModule(Type registerType, IApplicationModule module);

        /// <summary>
        /// Removes a module by the specified register type.
        /// </summary>
        bool RemoveModule<T>() where T : class, IApplicationModule;

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
        T GetModule<T>() where T : class, IApplicationModule;

        /// <summary>
        /// Tries to get module by the specified register type.
        /// </summary>
        /// <param name="module">The found module.</param>
        bool TryGetModule<T>(out T module) where T : class, IApplicationModule;

        /// <summary>
        /// Tries to get module by the specified register type.
        /// </summary>
        /// <param name="registerType">The type used to register module.</param>
        /// <param name="module">The found module.</param>
        bool TryGetModule(Type registerType, out IApplicationModule module);
    }
}
