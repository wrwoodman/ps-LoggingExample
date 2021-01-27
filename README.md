# PostSharp Logging Example using Nlog.
This is a sample project used to demonstrate how to inject properties into PostSharp logging using NLog. In this example, a property, called the UniqueId, is injected into the PostSharp activity cycle.  This allows the UID to be injected into all the log files without having to do any additional work.

This is a working example that uses PostSharp developer mode, an NLog custom renderer, and provides a sample UI using Swagger.  This is written in .NET Core 3.1.

* To run this program, run from Visual Studio (F5 or Ctrl-F5) or from the commandline with dotnet run Argo.MyProject.csproj.
* The log files that are generated will be found under the Logs directory in the Argo.MyProject directory.
* The directory for the log files can be changed in the nlogcommon.config file.

Change this value to the desired location.  
```
<variable name="logDirectory" value=".\Logs\"/>
```
