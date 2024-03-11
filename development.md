## Setup build dependencies

(per https://docs.bepinex.dev/articles/dev_guide/plugin_tutorial/1_setup.html)
```
sudo apt-get update && \
  sudo apt-get install -y dotnet-sdk-8.0

dotnet new -i BepInEx.Templates --nuget-source https://nuget.bepinex.dev/v3/index.json  
```
