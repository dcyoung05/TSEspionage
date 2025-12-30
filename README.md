# TSEspionage - A Twilight Struggle Mod

TSEspionage is a mod for the Playdek Twilight Struggle client. This mod adds various improvements and addresses bugs.

## Installation (Windows)

First you will need to find your Twilight Struggle install folder. Open Steam and right click on "Twilight Struggle", then click on "Properties...", next click on "Installed Files", finally click "Browse...". This will open the folder that Twilight Struggle is installed into.

Next [download the latest version of the mod](https://github.com/xelrach/TSEspionage/releases) for Windows. Open the ZIP file. Extract the contents of the ZIP file directly into the Twilight Struggle install directory. You should now have a directory called "BePinEx" in the same place as the "TwlightStruggle" executable.

![Twilight Struggle Folder](https://github.com/dcyoung05/TSEspionage/blob/main/docs/Twilight_Struggle_Folder.png?raw=true)

Then start Twilight Struggle. The first time you start the game with this mod installed, it will take a few minutes for the main window to appear.

## Uninstall (Windows)

Remove doorstop_config.ini, .doorstop_version, winhttp.dll, BePinEx, and dotnet from the Twilight Struggle directory

## Development

If you would like to develop TSEspionage there are a few steps to get set-up:
* Either:
  * Install the mod as described above.
  * Or [install BePinEx](https://docs.bepinex.dev/master/articles/user_guide/installation/index.html), using the "bleeding edge" build for your platform and IL2CPP. Then copy 
  `BePinEx\core\IL2CppInterop.Runtime.dll` from the mod ZIP file (this is needed to fix an incompatibility between BePinEx and the Unity version used by TS), or [build the fixed DLL](https://github.com/dcyoung05/Il2CppInterop) and copy it from the build directory.
* Start Twilight Struggle and get to the main menu (needed to generate the interop assemblies).
* Install the .NET SDK by opening a shell and running:
```
  winget install "Microsoft.DotNet.SDK.10"
```
* TSEspionage is a C# project, so you will likely want to install an IDE that supports C#.
* Use git to clone the repository.
* Open the git directory in your IDE.
* Let your IDE run NuGet.
* Open TSEspionage/TSEspionage.csproj, find the line beginning ```<TSPath>``` and ending ```</TSPath>``` with a path between, and change the path to match your Twilight
Struggle install directory.
* Non-standard build targets: `Test` builds the mod, copies it to the defined Twilight Struggle path, and attempts to launch the game; `Release` makes a ZIP file for
distribution (using the BePinEx files currently installed in the Twilight Struggle folder and the newly-buidl mod).
