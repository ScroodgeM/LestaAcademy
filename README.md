# Lesta Academy — Unity Lecture Demos

A collection of focused Unity 6 projects created as practical material for lectures at Lesta Academy.

The repository covers software architecture and design patterns, the 3Cs of game development, fog of war and minimaps, stencil-buffer portals, procedural geometry, runtime texture generation, shaders, and multithreading. Each topic is kept in a separate Unity project so it can be opened, explored, and demonstrated independently.

## At a glance

- **7 standalone Unity 6 projects** with focused scenes and source code.
- **19 3Cs scenes** covering controls, characters, cameras, and cinematics.
- **25 design-pattern examples** grouped into creational, structural, and behavioral families.
- Side-by-side **correct and incorrect implementations** of common design principles.
- Practical rendering examples using render textures, stencil buffers, procedural meshes, runtime texture composition, and custom shaders.
- PlantUML diagrams for component-based design and entity-component-system decomposition.

## Projects

| Project | Unity version | Main entry points | Focus |
| --- | --- | --- | --- |
| [`3CsExamples`](3CsExamples) | 6000.0.26f1 | [`Assets/Scenes`](3CsExamples/Assets/Scenes) | Input, character control, 2D/3D cameras, Cinemachine, mobile controls, cinematics |
| [`FogOfWarAndMinimap`](FogOfWarAndMinimap) | 6000.0.26f1 | [`BattleScene.unity`](FogOfWarAndMinimap/Assets/Scenes/BattleScene.unity) | Render-texture fog of war, revealers, minimap composition, world/UI coordinate mapping |
| [`Portal`](Portal) | 6000.0.26f1 | [`PortalScene.unity`](Portal/Assets/Scenes/PortalScene.unity) | Linked worlds, secondary cameras, render textures, stencil masks, teleportation |
| [`ProceduralGeometry`](ProceduralGeometry) | 6000.0.33f1 | [`000_Terrain.unity`](ProceduralGeometry/Assets/Scenes/000_Terrain.unity), [`Dungeon.unity`](ProceduralGeometry/Assets/Scenes/Dungeon.unity) | Mesh generation, Perlin terrain, chunking, deformation, water, dungeon generation |
| [`ProceduralTextures`](ProceduralTextures) | 6000.0.33f1 | [`Photobooth.unity`](ProceduralTextures/Assets/Scenes/Photobooth.unity), [`TextureWithCode.unity`](ProceduralTextures/Assets/Scenes/TextureWithCode.unity) | Runtime thumbnails, render textures, GL drawing, texture export |
| [`Shaders_BuiltInRP`](Shaders_BuiltInRP) | 6000.0.37f1 | [`050_Demo.unity`](Shaders_BuiltInRP/Assets/Scenes/050_Demo.unity), [`100_Sun.unity`](Shaders_BuiltInRP/Assets/Scenes/100_Sun.unity) | Built-in Render Pipeline shaders and image post-processing |
| [`SoftwareArchitecture`](SoftwareArchitecture) | 6000.0.37f1 | [`Assets/Scripts`](SoftwareArchitecture/Assets/Scripts), [`ThreadsDemo.unity`](SoftwareArchitecture/Assets/ThreadsDemo/ThreadsDemo.unity) | OOP, design principles, design patterns, object pools, multithreading |
| [`PlantUML`](PlantUML) | — | [diagram sources](PlantUML) | Component-based and ECS-oriented architecture diagrams |

## Getting started

Clone the repository:

```bash
git clone https://github.com/ScroodgeM/LestaAcademy.git
```

Each top-level Unity folder is an independent project:

1. Open Unity Hub.
2. Select **Add project from disk**.
3. Choose one of the project folders listed above.
4. Use the Unity version shown in the project table, or a compatible Unity 6 editor.
5. Open the recommended scene and enter Play Mode.

> [!NOTE]
> `3CsExamples` and `SoftwareArchitecture` reference [`UnityTools`](https://github.com/ScroodgeM/UnityTools) through an SSH Git URL. Configure GitHub SSH access or replace the dependency in `Packages/manifest.json` with `https://github.com/ScroodgeM/UnityTools.git` if package resolution fails.

## 3Cs: controls, character, and camera

[`3CsExamples`](3CsExamples) is a progression of small scenes demonstrating how player input, character behavior, and camera design affect one another.

### Controls and character

| Scene | Topic |
| --- | --- |
| [`010_GetInput`](3CsExamples/Assets/Scenes/010_GetInput.unity) | Basic input acquisition |
| [`020_GetInputAndAnimator`](3CsExamples/Assets/Scenes/020_GetInputAndAnimator.unity) | Connecting player input to movement and animation |
| [`030_AcceletometerAndGyro`](3CsExamples/Assets/Scenes/030_AcceletometerAndGyro.unity) | Accelerometer and gyroscope input |
| [`040_OnScreenGamePad`](3CsExamples/Assets/Scenes/040_OnScreenGamePad.unity) | On-screen controls for mobile devices |

The scripts demonstrate Unity's Input System, input-device changes, movement relative to the camera, animation parameters, multiple control modes, and separation through an input interface.

### 2D camera examples

- [`200_Camera2D_CenterLock`](3CsExamples/Assets/Scenes/200_Camera2D_CenterLock.unity)
- [`210_Camera2D_FrameLock`](3CsExamples/Assets/Scenes/210_Camera2D_FrameLock.unity)
- [`220_Camera2D_SmoothFollow`](3CsExamples/Assets/Scenes/220_Camera2D_SmoothFollow.unity)
- [`230_Camera2D_LookAhead`](3CsExamples/Assets/Scenes/230_Camera2D_LookAhead.unity)
- [`240_Camera2D_DualFocuswithZoom`](3CsExamples/Assets/Scenes/240_Camera2D_DualFocuswithZoom.unity)
- [`250_Camera2D_LocalMPGroupCenterLock`](3CsExamples/Assets/Scenes/250_Camera2D_LocalMPGroupCenterLock.unity)

These scenes compare hard locks, framing, damping, look-ahead behavior, dual-target zoom, and local multiplayer framing.

### 3D camera examples

- [`400_Camera3D_Lock`](3CsExamples/Assets/Scenes/400_Camera3D_Lock.unity)
- [`410_Camera3D_LockFPS`](3CsExamples/Assets/Scenes/410_Camera3D_LockFPS.unity)
- [`420_Camera3D_SmoothFollow`](3CsExamples/Assets/Scenes/420_Camera3D_SmoothFollow.unity)
- [`430_Camera3D_RayCastBack`](3CsExamples/Assets/Scenes/430_Camera3D_RayCastBack.unity)
- [`440_Camera3D_FreeLook`](3CsExamples/Assets/Scenes/440_Camera3D_FreeLook.unity)
- [`450_Camera3D_FreeLook_ReturnOnIdle`](3CsExamples/Assets/Scenes/450_Camera3D_FreeLook_ReturnOnIdle.unity)
- [`460_Camera3D_CharacterSeenThrough`](3CsExamples/Assets/Scenes/460_Camera3D_CharacterSeenThrough.unity)
- [`470_Camera3D_AutoPOI`](3CsExamples/Assets/Scenes/470_Camera3D_AutoPOI.unity)

The sequence covers first-person and third-person locks, smooth following, obstacle handling, free look, idle recentering, visibility preservation, and automatic points of interest.

[`600_Cinematic`](3CsExamples/Assets/Scenes/600_Cinematic.unity) provides a separate Cinemachine-based cinematic example.

Key packages: **Input System 1.11.2** and **Cinemachine 3.1.2**.

## Fog of war and minimap

[`FogOfWarAndMinimap`](FogOfWarAndMinimap) demonstrates a lightweight fog-of-war pipeline inside a small top-down battle simulation.

Core ideas:

- a dedicated camera initializes and maintains the fog render buffer;
- revealers draw visibility into a render texture;
- materials combine the fog state with the world and minimap presentation;
- a transform matrix maps a followed object's position between spaces;
- simple ally/enemy spawning and movement make the visibility result easy to inspect under load.

Start with [`BattleScene.unity`](FogOfWarAndMinimap/Assets/Scenes/BattleScene.unity).

## Portals and stencil rendering

[`Portal`](Portal) renders a second world through a portal surface and moves the player between linked spaces.

The demo combines:

- two world cameras and a shared render texture;
- stencil writes for the portal aperture;
- a full-screen post-render pass restricted by the stencil value;
- camera offsets and near-clip adjustments;
- trigger-driven world switching and character repositioning;
- separate skyboxes and scene state for the linked spaces.

Relevant sources:

- [`CameraSwitcher.cs`](Portal/Assets/Scripts/CameraSwitcher.cs)
- [`DrawAnotherWorld.cs`](Portal/Assets/Scripts/DrawAnotherWorld.cs)
- [`PortalHole.shader`](Portal/Assets/Shaders/PortalHole.shader)
- [`AnotherWorldInHole.shader`](Portal/Assets/Shaders/AnotherWorldInHole.shader)

## Procedural geometry

[`ProceduralGeometry`](ProceduralGeometry) contains several independent geometry-generation exercises built with URP 17.0.3.

### Terrain

The low-poly terrain implementation demonstrates:

- procedural vertex, triangle, color, UV, and normal generation;
- optional vertex welding;
- layered Perlin-noise height and color generation;
- splitting a generated surface into chunks;
- mesh and collider synchronization;
- localized runtime deformation with several deformation modes;
- custom inspectors and continuous edit-mode regeneration.

[`000_Terrain.unity`](ProceduralGeometry/Assets/Scenes/000_Terrain.unity) also includes a controllable vehicle and projectiles that deform the generated terrain.

### Dungeon

[`Dungeon.unity`](ProceduralGeometry/Assets/Scenes/Dungeon.unity) uses a seeded random-walk map generator and converts walkable cells into floor, wall, and roof tiles. The custom editor can regenerate the dungeon and preview its logical map as a texture.

### Water

`LowPolyWater` builds a grid mesh procedurally, while the included material and shader demonstrate animated per-vertex low-poly water rendering.

## Procedural textures

[`ProceduralTextures`](ProceduralTextures) presents two approaches to runtime image generation.

### Photobooth

[`Photobooth.unity`](ProceduralTextures/Assets/Scenes/Photobooth.unity) creates UI portraits of GameObjects by:

- instantiating a target in an isolated preview stage;
- rendering it with a dedicated camera;
- reading the result into a `Texture2D`;
- assigning the generated image to UI elements.

### Texture composition with code

[`TextureWithCode.unity`](ProceduralTextures/Assets/Scenes/TextureWithCode.unity) combines multiple texture layers through a `RenderTexture` and immediate-mode `GL` quads. Each layer has its own position, size, and rotation, and the result can be exported as PNG. Utility methods also cover PNG/JPEG encoding and render-texture readback.

## Built-in Render Pipeline shaders

[`Shaders_BuiltInRP`](Shaders_BuiltInRP) contains compact examples for the Built-in Render Pipeline:

- image post-processing through `OnRenderImage` and `Graphics.Blit`;
- a multi-pass animated star shader;
- surface and atmospheric color controls;
- view-dependent atmosphere inside and outside the model silhouette;
- animated radial noise for stellar rays.

Open [`050_Demo.unity`](Shaders_BuiltInRP/Assets/Scenes/050_Demo.unity) for the post-processing material demonstrations and [`100_Sun.unity`](Shaders_BuiltInRP/Assets/Scenes/100_Sun.unity) for the star shader.

## Software architecture

[`SoftwareArchitecture`](SoftwareArchitecture) is the largest module in the repository. It contains compact C# examples designed for discussion during architecture lectures rather than as one runnable game.

### Object-oriented programming pillars

- [Abstraction](SoftwareArchitecture/Assets/Scripts/Pillars/Abstraction)
- [Encapsulation](SoftwareArchitecture/Assets/Scripts/Pillars/Encapsulation)
- [Inheritance](SoftwareArchitecture/Assets/Scripts/Pillars/Inheritance)
- [Polymorphism](SoftwareArchitecture/Assets/Scripts/Pillars/Polymorphism)

### Design principles

Most principle folders contain deliberately contrasting `Wrong` and `Correct` implementations.

- [Single Responsibility](SoftwareArchitecture/Assets/Scripts/DesignPrinciples/SingleResponsibility)
- [Open/Closed](SoftwareArchitecture/Assets/Scripts/DesignPrinciples/OpenClosed)
- [Liskov Substitution](SoftwareArchitecture/Assets/Scripts/DesignPrinciples/LiskovSubstitutionPrinciple)
- [Interface Segregation](SoftwareArchitecture/Assets/Scripts/DesignPrinciples/InterfaceSegregationPrinciple)
- [Dependency Injection](SoftwareArchitecture/Assets/Scripts/DesignPrinciples/DependencyInjection)
- [DRY](SoftwareArchitecture/Assets/Scripts/DesignPrinciples/DontRepeatYourself)
- [Delegation](SoftwareArchitecture/Assets/Scripts/DesignPrinciples/DelegationPrinciples)
- [Encapsulate What Changes](SoftwareArchitecture/Assets/Scripts/DesignPrinciples/EncapsulateWhatChanges)
- [Favor Composition over Inheritance](SoftwareArchitecture/Assets/Scripts/DesignPrinciples/FavorCompositionOverInheritance)
- [Program to an Interface](SoftwareArchitecture/Assets/Scripts/DesignPrinciples/ProgrammingForInterfaceNotImplementation)

### Creational patterns

- [Abstract Factory](SoftwareArchitecture/Assets/Scripts/DesignPatterns/Creational/AbstractFactory)
- [Builder](SoftwareArchitecture/Assets/Scripts/DesignPatterns/Creational/Builder)
- [Factory Method](SoftwareArchitecture/Assets/Scripts/DesignPatterns/Creational/FactoryMethod)
- [Lazy Initialization](SoftwareArchitecture/Assets/Scripts/DesignPatterns/Creational/LazyInitialization)
- [Multiton](SoftwareArchitecture/Assets/Scripts/DesignPatterns/Creational/Multiton)
- [Object Pool](SoftwareArchitecture/Assets/Scripts/DesignPatterns/Creational/ObjectPool)
- [Prototype](SoftwareArchitecture/Assets/Scripts/DesignPatterns/Creational/Prototype)
- [Singleton](SoftwareArchitecture/Assets/Scripts/DesignPatterns/Creational/Singleton)

### Structural patterns

- [Adapter](SoftwareArchitecture/Assets/Scripts/DesignPatterns/Structural/Adapter)
- [Composite](SoftwareArchitecture/Assets/Scripts/DesignPatterns/Structural/Composite)
- [Decorator](SoftwareArchitecture/Assets/Scripts/DesignPatterns/Structural/Decorator)
- [Facade](SoftwareArchitecture/Assets/Scripts/DesignPatterns/Structural/Facade)
- [Flyweight](SoftwareArchitecture/Assets/Scripts/DesignPatterns/Structural/Flyweight)
- [Proxy](SoftwareArchitecture/Assets/Scripts/DesignPatterns/Structural/Proxy)

### Behavioral patterns

- [Chain of Responsibility](SoftwareArchitecture/Assets/Scripts/DesignPatterns/Behavioral/ChainOfResponsibility)
- [Command](SoftwareArchitecture/Assets/Scripts/DesignPatterns/Behavioral/Command)
- [Iterator](SoftwareArchitecture/Assets/Scripts/DesignPatterns/Behavioral/Iterator)
- [Mediator](SoftwareArchitecture/Assets/Scripts/DesignPatterns/Behavioral/Mediator)
- [Memento](SoftwareArchitecture/Assets/Scripts/DesignPatterns/Behavioral/Memento)
- [Observer](SoftwareArchitecture/Assets/Scripts/DesignPatterns/Behavioral/Observer)
- [Servant](SoftwareArchitecture/Assets/Scripts/DesignPatterns/Behavioral/Servant)
- [State](SoftwareArchitecture/Assets/Scripts/DesignPatterns/Behavioral/State)
- [Strategy](SoftwareArchitecture/Assets/Scripts/DesignPatterns/Behavioral/Strategy)
- [Template Method](SoftwareArchitecture/Assets/Scripts/DesignPatterns/Behavioral/TemplateMethod)
- [Visitor](SoftwareArchitecture/Assets/Scripts/DesignPatterns/Behavioral/Visitor)

### Threads demo

[`ThreadsDemo.unity`](SoftwareArchitecture/Assets/ThreadsDemo/ThreadsDemo.unity) demonstrates distributing force calculations across linked objects while keeping Unity-facing updates coordinated by a world controller.

## Architecture diagrams

The [`PlantUML`](PlantUML) folder contains editable diagram sources:

- [`ComponentBasedDesign.plantuml`](PlantUML/ComponentBasedDesign.plantuml) — interfaces and components for character customization, inventory, skins, equipment, shops, and billing.
- [`EntityComponentSystem1.plantuml`](PlantUML/EntityComponentSystem1.plantuml) — systems and behavior dependencies.
- [`EntityComponentSystem2.plantuml`](PlantUML/EntityComponentSystem2.plantuml) — example entity compositions for a player, monster, bullet, button, and tree.

Render them with PlantUML or a compatible IDE extension.

## Educational intent

These projects are designed as lecture demonstrations:

- each scene isolates a specific concept;
- implementation details are visible and easy to discuss;
- contrasting examples make trade-offs explicit;
- generated assets and custom inspectors keep experiments reproducible;
- the code favors instructional clarity over framework-level abstraction.

Some projects contain third-party art or UI assets used to support the demonstrations. Those assets remain subject to their respective authors' terms and should be reviewed separately before redistribution.
