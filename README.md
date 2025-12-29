# TSEspionage - A Twilight Struggle Mod

TSEspionage is a mod for the Playdek Twilight Struggle client. This mod adds various improvements and addresses bugs.

## Installation (Windows)

First you will need to find your Twilight Struggle install folder. Open Steam and right click on "Twilight Struggle", then click on "Properties...", next click on "Installed Files", finally click "Browse...". This will open the folder that Twilight Struggle is installed into.

Next [download the latest version of the mod](https://github.com/xelrach/TSEspionage/releases) for Windows. Open the ZIP file. Extract the contents of the ZIP file directly into the Twilight Struggle install directory. You should now have a directory called "BePinEx" in the same place as the "TwlightStruggle" executable.

![Twilight Struggle Folder](https://github.com/xelrach/TSEspionage/blob/main/docs/Twilight_Struggle_Folder.png?raw=true)

## Uninstall (Windows)

Remove doorstop_config.ini, .doorstop_version, winhttp.dll, BePinEx, and dotnet from the Twilight Struggle directory

## Development

If you would like to develop TSEspionage there are a few steps to get set-up:
* Install the .NET SDK by opening a shell and running:
```
  winget install "Microsoft.DotNet.SDK.10"
```
or by following the instructions at https://dotnet.microsoft.com/en-us/download.
* TSEspionage is a C# project, so you will likely want to install an IDE that supports C#.
* Use git to clone the repository.
* Open the git directory in your IDE
* Let your IDE run NuGet (or change to the directory where you cloned the repo and run ```dotnet restore``` at the command prompt).
* Open TSEspionage/TSEspionage.csproj, find the line beginning ```<TSPath>``` and ending ```</TSPath>``` with a path between, and change the path to match your Twilight Struggle install directory.
