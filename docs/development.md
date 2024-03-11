## Setup build dependencies

(per https://docs.bepinex.dev/articles/dev_guide/plugin_tutorial/1_setup.html)
```

# we need dotnet 6 which is no longer provided by ubuntu so use microsoft version instead
# but my ubuntu 24.04 repos don't have that old dotnet therefore ugly hack of
https://learn.microsoft.com/en-us/dotnet/core/install/linux-scripted-manual#scripted-install
export DOTNET_ROOT=$HOME/.dotnet
export PATH=$PATH:$DOTNET_ROOT:$DOTNET_ROOT/tools


sudo apt-get update && \
  sudo apt-get install -y dotnet-sdk-8.0

~/development/dyson-sphere-program/dsp-auto-alarm$ dotnet new bepinex5plugin -n AutoAlarm -T netstandard2.0 -U 2018.4.12

Welcome to .NET 6.0!
---------------------
SDK Version: 6.0.419

Telemetry
---------
The .NET tools collect usage data in order to help us improve your experience. It is collected by Microsoft and shared with the community. You can opt-out of telemetry by setting the DOTNET_CLI_TELEMETRY_OPTOUT environment variable to '1' or 'true' using your favorite shell.

Read more about .NET CLI Tools telemetry: https://aka.ms/dotnet-cli-telemetry

----------------
Installed an ASP.NET Core HTTPS development certificate.
To trust the certificate run 'dotnet dev-certs https --trust' (Windows and macOS only).
Learn about HTTPS: https://aka.ms/dotnet-https
----------------
Write your first app: https://aka.ms/dotnet-hello-world
Find out what's new: https://aka.ms/dotnet-whats-new
Explore documentation: https://aka.ms/dotnet-docs
Report issues and find source on GitHub: https://github.com/dotnet/core
Use 'dotnet --help' to see available commands or visit: https://aka.ms/dotnet-cli
--------------------------------------------------------------------------------------
The template "BepInEx 5 Plugin Template" was created successfully.

```

## developing 

per https://docs.bepinex.dev/articles/dev_guide/plugin_tutorial/4_configuration.html

https://github.com/icsharpcode/AvaloniaILSpy/releases
ILSpy /home/kevinh/.nuget/packages/dysonsphereprogram.gamelibs/0.10.29.21950-r.0/lib/netstandard2.0/Assembly-CSharp.dll