# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [8.4.0](https://github.com/unity-game-framework/ugf-application/releases/tag/8.4.0) - 2022-11-19  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/37?closed=1)  
    

### Added

- Add project config setup build step ([#101](https://github.com/unity-game-framework/ugf-application/issues/101))  
    - Update dependencies: `com.ugf.runtimetools` to `2.17.0` and `com.ugf.editortools` to `2.13.0` versions, add `com.ugf.build` of `1.1.0` version.
    - Add `ApplicationConfigStep` class as build step to setup project application config.

## [8.3.1](https://github.com/unity-game-framework/ugf-application/releases/tag/8.3.1) - 2022-07-26  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/36?closed=1)  
    

### Fixed

- Fix TryGetApplication from gameobject throws ([#99](https://github.com/unity-game-framework/ugf-application/issues/99))  
    - Fix `Scene.TryGetApplication()` and `GameObject.TryGetApplication()` extension methods to does not throw error when no provider found.

## [8.3.0](https://github.com/unity-game-framework/ugf-application/releases/tag/8.3.0) - 2022-07-12  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/35?closed=1)  
    

### Added

- Add application module initialize async ([#97](https://github.com/unity-game-framework/ugf-application/issues/97))  
    - Update dependencies: `com.ugf.initialize` to `2.9.0`, `com.ugf.runtimetools` to `2.9.2` and `com.ugf.editortools` to `2.8.0` versions.
    - Add `ApplicationModuleAsync` class as implementation for `IApplicationModuleAsync` interface.
    - Change `IApplicationModuleAsync` interface to inherit `IInitializeAsync` one.
    - Change `ApplicationModule` class to inherit `Initializable` class what provides children for initialization.

## [8.2.0](https://github.com/unity-game-framework/ugf-application/releases/tag/8.2.0) - 2022-06-06  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/34?closed=1)  
    

### Added

- Add application resources loader create override ([#95](https://github.com/unity-game-framework/ugf-application/issues/95))  
    - Update dependencies: `com.ugf.runtimetools` to `2.8.0` version.
    - Add `ApplicationLauncherResources.OnLoadAsset()` virtual method to override loading behaviour.
    - Change `ApplicationLauncherResources.CreateAsync()` method to return `IApplication` as result.

## [8.1.0](https://github.com/unity-game-framework/ugf-application/releases/tag/8.1.0) - 2022-05-07  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/33?closed=1)  
    

### Added

- Add application launcher from resources ([#93](https://github.com/unity-game-framework/ugf-application/issues/93))  
    - Update dependencies: `com.ugf.initialize` to `2.8.0`, `com.ugf.runtimetools` to `2.7.0`, `com.ugf.logs` to `5.3.0`, `com.ugf.editortools` to `2.5.0` and `com.ugf.builder` to `2.0.2`.
    - Update package _Unity_ version to `2021.3`.
    - Add `ApplicationLauncherResources` class used to create launcher from prefab at resources folder.

## [8.0.0](https://github.com/unity-game-framework/ugf-application/releases/tag/8.0.0) - 2021-11-23  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/32?closed=1)  
    

### Added

- Add GameObject.GetApplication extension method ([#92](https://github.com/unity-game-framework/ugf-application/pull/92))  
    - Update package _Unity_ version to `2021.2`.
    - Update dependencies: `com.ugf.initialize` to `2.7.0`, `com.ugf.runtimetools` to `2.4.0`, `com.ugf.logs` to `5.2.0`, `com.ugf.editortools` to `2.1.0` and `com.ugf.builder` to `2.0.1`. 
    - Add `GameObject.TryGetApplication()` extension method to get access to application connected to scene of the specified _GameObject_.

## [8.0.0-preview.9](https://github.com/unity-game-framework/ugf-application/releases/tag/8.0.0-preview.9) - 2021-08-02  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/31?closed=1)  
    

### Fixed

- Fix application singleton to inherit configured builders ([#90](https://github.com/unity-game-framework/ugf-application/pull/90))  
    - Update dependencies: `com.ugf.runtimetools` to `2.2.0` version.
    - Fix `ApplicationSingletonBuilder` class to inherit from `ApplicationConfiguredBuilder` class.
    - Fix `ApplicationSingletonBuilderComponent ` class to inherit from `ApplicationConfiguredBuilderComponent` class.

## [8.0.0-preview.8](https://github.com/unity-game-framework/ugf-application/releases/tag/8.0.0-preview.8) - 2021-07-06  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/30?closed=1)  
    

### Fixed

- Fix ApplicationConfigProjectAsset does not open project settings ([#88](https://github.com/unity-game-framework/ugf-application/pull/88))  
    - Fix `ApplicationConfigProjectAsset` to open _Application_ section in _ProjectSettings_ with `Open Application Project Settings` button in inspector of the asset.

## [8.0.0-preview.7](https://github.com/unity-game-framework/ugf-application/releases/tag/8.0.0-preview.7) - 2021-05-25  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/29?closed=1)  
    

### Changed

- Change project settings root name ([#86](https://github.com/unity-game-framework/ugf-application/pull/86))  
    - Update dependencies: `com.ugf.logs` to `5.1.4` version.
    - Change project settings root name to `Unity Game Framework`.

## [8.0.0-preview.6](https://github.com/unity-game-framework/ugf-application/releases/tag/8.0.0-preview.6) - 2021-04-09  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/28?closed=1)  
    

### Added

- Add higher execute priority for ApplicationSceneProviderInstanceComponent ([#84](https://github.com/unity-game-framework/ugf-application/pull/84))  
    - Add `DefaultExecutionOrder` attribute for `ApplicationSceneProviderInstanceComponent` component with highest priority.

## [8.0.0-preview.5](https://github.com/unity-game-framework/ugf-application/releases/tag/8.0.0-preview.5) - 2021-03-16  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/27?closed=1)  
    

### Added

- Add IApplicationLauncherResourceLoader default implementation ([#81](https://github.com/unity-game-framework/ugf-application/pull/81))  
    - Add `ApplicationLauncherResourceLoader` and `ApplicationLauncherResourceLoaderBase` classes as default implementation of `IApplicationLauncherResourceLoader` interface.
    - Change name of `ApplicationLauncherResources` class to `ApplicationLauncherResourcesComponent`.
    - Change name of `ApplicationLauncherResourceLoader` class to `ApplicationLauncherResourceLoaderComponent`.

### Changed

- Rename IApplicationLauncher.Launch to LaunchAsync ([#82](https://github.com/unity-game-framework/ugf-application/pull/82))  
    - Change name of `IApplicationLauncher.Launch` method to `LaunchAsync`.

## [8.0.0-preview.4](https://github.com/unity-game-framework/ugf-application/releases/tag/8.0.0-preview.4) - 2021-02-09  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/26?closed=1)  
    

### Changed

- Update project registry ([#77](https://github.com/unity-game-framework/ugf-application/pull/77))  
    - Update package publish registry.
- Update to Unity 2021.1 ([#76](https://github.com/unity-game-framework/ugf-application/pull/76))

## [8.0.0-preview.3](https://github.com/unity-game-framework/ugf-application/releases/tag/8.0.0-preview.3) - 2021-01-24  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/25?closed=1)  
    

### Changed

- Update logs package ([#73](https://github.com/unity-game-framework/ugf-application/pull/73))  
    - Update dependencies: `com.ugf.logs` to `5.1.3` version.

## [8.0.0-preview.2](https://github.com/unity-game-framework/ugf-application/releases/tag/8.0.0-preview.2) - 2021-01-24  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/24?closed=1)  
    

### Changed

- Update logs and defines packages ([#71](https://github.com/unity-game-framework/ugf-application/pull/71))  
    - Update dependencies: `com.ugf.logs` to `5.1.2` version and `com.ugf.defines` to version `2.1.1`.

## [8.0.0-preview.1](https://github.com/unity-game-framework/ugf-application/releases/tag/8.0.0-preview.1) - 2021-01-23  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/23?closed=1)  
    

### Fixed

- Fix dependencies ([#69](https://github.com/unity-game-framework/ugf-application/pull/69))  
    - Fix dependencies: `com.ugf.runtimetools` to `2.0.0` version.

## [8.0.0-preview](https://github.com/unity-game-framework/ugf-application/releases/tag/8.0.0-preview) - 2021-01-23  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/22?closed=1)  
    

### Changed

- Rework scene providers ([#65](https://github.com/unity-game-framework/ugf-application/pull/65))  
    - Add dependencies: `com.ugf.runtimetools` of `2.0.0` version.
    - Add `ApplicationSceneProviderInstanceComponent` component to create and register provider to store application by scenes.
    - Change `ApplicationLauncherComponent` and `ApplicationSceneAccessComponent` to work with provider system.
    - Remove `ApplicationSceneProvider`, `ApplicationSceneProviderInstance` classes and replace with provider system.

### Removed

- Remove ApplicationModuleDescription constructor ([#67](https://github.com/unity-game-framework/ugf-application/pull/67))  
    - Remove `ApplicationModuleDescription` constructor with `registerType` argument.

## [7.1.0](https://github.com/unity-game-framework/ugf-application/releases/tag/7.1.0) - 2021-01-15  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/21?closed=1)  
    

### Added

- Add application logging ([#61](https://github.com/unity-game-framework/ugf-application/pull/61))  
    - Add dependency: `com.ugf.logs` of `5.1.0` version.
    - Add logging for `ApplicationConfigured`, `ApplicationLauncher`, `ApplicationSingleton` and `ApplicationLauncherComponent` classes.
    - Add overridable `OnInitializeApplicationAsync` method for `ApplicationLauncher` class.
    - Change `ApplicationConfigured` modules creation workflow, now modules created at `OnInitialize` and removed on `OnUninitialize` method.
- Add initialize methods for ApplicationLauncherComponent ([#60](https://github.com/unity-game-framework/ugf-application/pull/60))  
    - Add implementation of `IInitialize` interface for `ApplicationLauncherComponent` class.

### Deprecated

- Add obsolete attribute for ApplicationModuleDescription constructor with register type ([#58](https://github.com/unity-game-framework/ugf-application/pull/58))  
    - Deprecate `ApplicationModuleDescription` constructor with `registerType` argument, use default constructor and property initialization to specify register type.

## [7.0.0](https://github.com/unity-game-framework/ugf-application/releases/tag/7.0.0) - 2021-01-10  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/20?closed=1)  
    

### Changed

- Add application and module builders ([#54](https://github.com/unity-game-framework/ugf-application/pull/54))  
    - Add `ApplicationBuilder`, `ApplicationBuilderComponent` and `ApplicationModuleBuilder` builder classes.
    - Add `ApplicationConfiguredBuilder` and `ApplicationConfiguredBuilderComponent` builder classes for `ApplicationConfigured` application.
    - Add `ApplicationOrderedBuilder` and `ApplicationOrderedBuilderComponent` builder classes for `ApplicationOrdered` application.
    - Add `ApplicationSingletonBuilder` and `ApplicationSingletonBuilderComponent` builder classes for `ApplicationSingleton` application.
    - Add `ApplicationLauncherComponent` as replacement of old `ApplicationLauncher` component, used to create and launch `IApplicationLauncher` instance.
    - Add `IApplicationLauncherResourceLoader` interface and implementation for `ApplicationLauncherResourceLoader` component.
    - Add `Launched` and `Quitting` events handling for `ApplicationOrdered` application.
    - Rework `ApplicationLauncher` to be initializable instance instead of component, used with `ApplicationBuilderComponent` builder and `ApplicationLauncherResourceLoader` resource loader components.
    - Change `ApplicationLauncherEvents` to  work with `ApplicationLauncherComponent` instead of old `ApplicationLauncher` component.
    - Remove `Quitting` event from `ApplicationLauncherEvents` component and `IApplicationLauncherEventHandler` interface.
    - Remove quitting event from application and launcher.
    - Remove `ApplicationConfiguredLauncher`, `ApplicationOrderedLauncher` and `ApplicationSingletonLauncher` launchers.

## [6.1.1](https://github.com/unity-game-framework/ugf-application/releases/tag/6.1.1) - 2021-01-09  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/19?closed=1)  
    

### Changed

- Change ApplicationModuleDescription constructor ([#51](https://github.com/unity-game-framework/ugf-application/pull/51))  
    - Add default constructor for `ApplicationModuleDescription` class, and constructor with `registerType` argument not recommended to use.
    - Change `RegisterType` property to be read and write.

## [6.1.0](https://github.com/unity-game-framework/ugf-application/releases/tag/6.1.0) - 2020-12-18  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/18?closed=1)  
    

### Added

- Add application component access ([#48](https://github.com/unity-game-framework/ugf-application/pull/48))  
    - Add `ApplicationAccessComponent` an abstract class to implement `IApplication` access component.
    - Add `ApplicationLauncher.SceneAccess` property to determine whether to register created application to `Launcher` scene.
    - Add `ApplicationLauncher.OnRegisterAtScene` and `OnUnregisterAtScene` methods to override application to scene register behaviour.
    - Add `ApplicationSceneAccessComponent` as default implementation of `ApplicationAccessComponent` to provide access using `ApplicationSceneProviderInstance` static class.
    - Add `GetApplication` and `TryGetApplication` extension methods for `Scene`, to access registered application.
    - Add `ApplicationSceneProvider` and `ApplicationSceneProviderInstance` to manage applications for specific scenes.

## [6.0.0](https://github.com/unity-game-framework/ugf-application/releases/tag/6.0.0) - 2020-12-05  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/17?closed=1)  
    

### Changed

- Change descriptions and assets to use things from new packages ([#43](https://github.com/unity-game-framework/ugf-application/pull/43))  
    - Rework all assets to work using `UGF.Builder` package, and descriptions and modules to use `UGF.Description` package.
    - Add dependencies: `com.ugf.description` and `com.ugf.builder`.
    - Add register all components under the `Unity Game Framework` add component menu.
    - Change dependencies of `com.ugf.customsettings` and `com.ugf.initialize`.
    - Change `ApplicationConfig` to be description, and replace `IApplicationModuleAsset` by `IApplicationModuleBuilder` builder interface.
    - Change `ApplicationResourceAsset` and other assets to be async builder assets.
    - Change `IApplicationModule` to be described object with module description and application.
    - Change `IApplicationModuleDescription` to contains module register type.
    - Change `ApplicationModuleAsset` to be builder asset of described object and description.
    - Change `ApplicationModuleAsset<TModule>` to be builder with default module description.
    - Change name of the root of create asset menu, from `UGF` to `Unity Game Framework`.
    - Remove `ApplicationModuleBase` and `ApplicationModuleDescribed<TDescription>`, use `ApplicationModule<TDescription>` instead, now module always described.
    - Remove `ApplicationModuleDescriptionBase`, use `ApplicationModuleDescription` instead.
    - Remove `ApplicationModuleDescribedAsset`, use `ApplicationModuleAsset` instead.
    - Remove `RegisterType` property from `ApplicationModuleAsset`, now `ApplicationModuleDescription` contains this information.
    - Remove `ApplicationResourceAsyncAsset`, now `ApplicationResourceAsset` always asynchronously loading resources.
    - Remove `ApplicationUtility`, all logic has been moved to `ApplicationResourceAsset`.

## [5.3.0](https://github.com/unity-game-framework/ugf-application/releases/tag/5.3.0) - 2020-11-10  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/16?closed=1)  
    

### Changed

- Update dependencies ([#40](https://github.com/unity-game-framework/ugf-application/pull/40))  
    - Update `com.ugf.initialize` to `2.5.0` version.
    - Update `com.ugf.customsettings` to `3.1.0` version.
    - Update `com.ugf.editortools` to `1.6.0` version.

## [5.2.1](https://github.com/unity-game-framework/ugf-application/releases/tag/5.2.1) - 2020-11-10  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/15?closed=1)  
    

### Changed

- Remove internals visible to attribute ([#37](https://github.com/unity-game-framework/ugf-application/pull/37))  
    - Remove `InternalsVisibleTo` attribute from runtime assembly.
    - Change `ApplicationSettings.Settings` to be public.

## [5.2.0](https://github.com/unity-game-framework/ugf-application/releases/tag/5.2.0) - 2020-11-07  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/14?closed=1)  
    

### Added

- Add generic IApplicationModuleDescribed interface ([#34](https://github.com/unity-game-framework/ugf-application/pull/34))  
    - Add `IApplicationModuleDescribed<TDescription>` interface.
    - Change `ApplicationModuleDescribed<TDescription>` class to implement `IApplicationModuleDescribed<TDescription>` interface.

## [5.1.0](https://github.com/unity-game-framework/ugf-application/releases/tag/5.1.0) - 2020-10-22  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/13?closed=1)  
    

### Added

- Add module with description builder ([#31](https://github.com/unity-game-framework/ugf-application/pull/31))  
    - Add `ApplicationModuleDescribed<T>` an application module default implementation with typed description.
    - Add `ApplicationModuleDescribedAsset` scriptableobject asset to define module creation with description.
    - Add `IApplicationModule.Application` property to access to application.
    - Add `IApplicationModuleDescribed`, `IApplicationModuleDescription`, `IApplicationModuleDescriptionAsset` to implement custom described application module.
    - Change `ApplicationModuleBase` to always have `Application` from creation.
    - Update package dependencies: `com.ugf.editortools` to `1.5.1`.

## [5.0.0](https://github.com/unity-game-framework/ugf-application/releases/tag/5.0.0) - 2020-10-21  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/12?closed=1)  
    

### Changed

- Rework application and module info assets ([#28](https://github.com/unity-game-framework/ugf-application/pull/28))  
    - Update dependencies to use latest packages from `public` bintray repository.
    - Add `ApplicationOrdered` as default implementation of `Application` with ordered modules.
    - Add `ApplicationSingleton` used to support global application instance definition with `ApplicationInstance` access.
    - Add `ApplicationOrderedLauncher` component to create and initialize `ApplicationOrdered` application.
    - Add `ApplicationConfiguredLauncher` component to create and initialize `ApplicationConfigured` application.
    - Add `ApplicationSingletonLauncher` component to create and initialize `ApplicationSingleton` application.
    - Add `ApplicationConfigProjectAsset` which used to load project config during application initialization.
    - Add `ApplicationLauncherResources` async resources support.
    - Add `ApplicationResourceAsyncAsset` used to implement async resource loading with `ApplicationLauncherResources` component.
    - Add `ApplicationModuleAsset` and `ApplicationModuleAsset<T>` scriptableobject asset as default abstract implementation of `IApplicationModuleAsset` interface.
    - Add `ApplicationSettings` static class to access to project settings config at runtime.
    - Rework `ApplicationBase` to exclude modules storage implementation and provides abstract methods to implement.
    - Rework `ApplicationConfigured` to inherit from `ApplicationOrdered` and initialize modules from config.
    - Rework `ApplicationConfigAsset` to support `IApplicationModuleAsset`.
    - Replace `IApplicationModuleInfo` by `IApplicationModuleAsset`, and remove related classes.
    - Remove `ApplicationConfigAssetBase` class, use `ApplicationResourceAsset` instead.
- Update to Unity 2020.2 ([#27](https://github.com/unity-game-framework/ugf-application/pull/27))

## [4.1.0-preview](https://github.com/unity-game-framework/ugf-application/releases/tag/4.1.0-preview) - 2020-02-15  

- [Commits](https://github.com/unity-game-framework/ugf-application/compare/4.0.0-preview...4.1.0-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/11?closed=1)

### Changed
- Package dependencies:
    - `com.ugf.initialize`: from `2.0.0-preview` to `2.1.0-preview`.

## [4.0.0-preview](https://github.com/unity-game-framework/ugf-application/releases/tag/4.0.0-preview) - 2020-01-26  

- [Commits](https://github.com/unity-game-framework/ugf-application/compare/3.2.0-preview...4.0.0-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/10?closed=1)

### Added
- `IApplicationResources` replace config loader.
- `IApplicationModuleAsync` to implement async loading for each module.
- `IApplication.InitializeAsync` method to implement async initialization.
- `ApplicationResourceAsset` to use in `ApplicationLauncherResources` component.
- `IApplicationLauncherEventHandler` to handler launcher events in application and modules.

### Changed
- `ApplicationLauncher`: rework all events.

### Removed
- Resource loading from `ApplicationLauncher` use `IApplicationResources` instead.
- Application config loaders.
- `ApplicationModuleBaseAsync` use `IApplicationModuleAsync` instead.
- `IApplicationModuleBuilder` and `ApplicationModuleBuilder` use `ApplicationModuleBuildHandler` instead.

### Fixed
- `ApplicationModuleInfoAssetEditor` register type display in inspector.

## [3.2.0-preview](https://github.com/unity-game-framework/ugf-application/releases/tag/3.2.0-preview) - 2020-01-13  

- [Commits](https://github.com/unity-game-framework/ugf-application/compare/3.1.0-preview...3.2.0-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/9?closed=1)

### Added
- `ApplicationLauncher`: `Launched`, `Stopped` and `Quttings` events.
- `ApplicationLauncher.UninitializeApplication` method wich invoked after launcher is stopped.
- `ApplicationLauncherEvents` component with `Launched`, `Stopped` and `Quttings` Unity events.

## [3.1.0-preview](https://github.com/unity-game-framework/ugf-application/releases/tag/3.1.0-preview) - 2020-01-11  

- [Commits](https://github.com/unity-game-framework/ugf-application/compare/3.0.0-preview...3.1.0-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/8?closed=1)

### Changed
- Package dependencies:
    - `com.ugf.customsettings`: from `1.0.0` to `2.0.0`.

## [3.0.0-preview](https://github.com/unity-game-framework/ugf-application/releases/tag/3.0.0-preview) - 2019-12-09  

- [Commits](https://github.com/unity-game-framework/ugf-application/compare/2.0.0-preview...3.0.0-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/7?closed=1)

### Added
- Package dependencies:
    - `com.ugf.customsettings`: `1.0.0`.
- `ApplicationConfigLauncher` to create configurable application with specified modules.

## [2.0.0-preview](https://github.com/unity-game-framework/ugf-application/releases/tag/2.0.0-preview) - 2019-11-18  

- [Commits](https://github.com/unity-game-framework/ugf-application/compare/1.2.0...2.0.0-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/6?closed=1)

### Changed
- Update to Unity 2019.3.
- Package dependencies:
    - `com.ugf.initialize`: from `1.2.0` to `2.0.0-preview`.
- Change async initialization to use C# async/awaits.

### Removed
- `ApplicationUnity` deprecated code.

## [1.2.0](https://github.com/unity-game-framework/ugf-application/releases/tag/1.2.0) - 2019-09-08  

- [Commits](https://github.com/unity-game-framework/ugf-application/compare/1.1.0...1.2.0)
- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/5?closed=1)

### Added
- `ApplicationBase`: `OnInitializeModules` and `OnInitializeModules` as possibility to override module initialization behaviour.

### Changed
- Package dependencies:
    - `com.ugf.initialize`: from `1.1.0` to `1.2.0`.
- `ApplicationLauncher.Stop`: application uninitialize behaviour.

## [1.1.0](https://github.com/unity-game-framework/ugf-application/releases/tag/1.1.0) - 2019-09-03  

- [Commits](https://github.com/unity-game-framework/ugf-application/compare/1.0.0...1.1.0)
- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/4?closed=1)

### Added
- `ApplicationLauncher`: `IsLaunched` and `HasApplication` properties to determine whether launcher create and complete application initialization.
- `ApplicationLauncher.Application` property used to get created `IApplication` after launch.
- `ApplicationLauncher.Stop` method to stop and uninitialize created application.
- `ApplicationLauncher`: `LaunchOnStart` and `StopOnQuit` properties to control behaviour on start and on application quit events.
- `ApplicationLauncher`: `OnStop`, `OnStopped` and `OnQuitting` overridable method events.

### Changed
- Package dependencies:
    - `com.ugf.initialize`: from `1.0.0` to `1.1.0`.

### Deprecated
- `ApplicationUnity.UninitializeOnUnityQuitting` functionality and replaced by `ApplicationLauncher.StopOnQuit` behaviour.

## [1.0.0](https://github.com/unity-game-framework/ugf-application/releases/tag/1.0.0) - 2019-08-29  

- [Commits](https://github.com/unity-game-framework/ugf-application/compare/1.0.0-preview.1...1.0.0)
- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/3?closed=1)

### Added
- Package short description.
- `ApplicationLauncher`: a `MonoBehaviour` that create and initialize application.

### Changed
- Update to Unity 2019.2.
- Package dependencies:
    - `com.ugf.initialize`: from `1.0.0-preview` to `1.0.0`.

## [1.0.0-preview.1](https://github.com/unity-game-framework/ugf-application/releases/tag/1.0.0-preview.1) - 2019-07-21  

- [Commits](https://github.com/unity-game-framework/ugf-application/compare/1.0.0-preview...1.0.0-preview.1)
- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/2?closed=1)

### Changed
- `ApplicationInstance`: throws exception if an application not specified.

## [1.0.0-preview](https://github.com/unity-game-framework/ugf-application/releases/tag/1.0.0-preview) - 2019-07-18  

- [Commits](https://github.com/unity-game-framework/ugf-application/compare/9022819...1.0.0-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/1?closed=1)

### Added
- This is a initial release.


