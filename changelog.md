# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

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


