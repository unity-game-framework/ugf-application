# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## Unreleased - 2019-01-01
- [Commits](https://github.com/unity-game-framework/ugf-application/compare/0.0.0...0.0.0)
- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/0?closed=1)

### Added
- Nothing.

### Changed
- Nothing.

### Deprecated
- Nothing.

### Removed
- Nothing.

### Fixed
- Nothing.

### Security
- Nothing.

## 1.1.0 - 2019-09-04
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

## 1.0.0 - 2019-08-29
- [Commits](https://github.com/unity-game-framework/ugf-application/compare/1.0.0-preview.1...1.0.0)
- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/3?closed=1)

### Added
- Package short description.
- `ApplicationLauncher`: a `MonoBehaviour` that create and initialize application.

### Changed
- Update to Unity 2019.2.
- Package dependencies:
    - `com.ugf.initialize`: from `1.0.0-preview` to `1.0.0`.

## 1.0.0-preview.1 - 2019-07-21
- [Commits](https://github.com/unity-game-framework/ugf-application/compare/1.0.0-preview...1.0.0-preview.1)
- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/2?closed=1)

### Changed
- `ApplicationInstance`: throws exception if an application not specified.

## 1.0.0-preview - 2019-07-18
- [Commits](https://github.com/unity-game-framework/ugf-application/compare/9022819...1.0.0-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-application/milestone/1?closed=1)

### Added
- This is a initial release.

---
> Unity Game Framework | Copyright 2019
