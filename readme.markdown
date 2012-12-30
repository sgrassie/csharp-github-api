# csharp-github-api - A CSharp library to access the GitHub API 
## [http://temporalcohesion.co.uk][1]

### About
This is a C# library for accessing GitHub's developer [REST API][4]. It uses the [RestSharp][3] [REST client][2] library for most of the heavy lifting.

This library is not an official Github product, is not supported or developed by Github, and all Github trademarks are their own.

Targets .NET 4.

### Status
Barely does anything at the moment. I'm lazy. Started working on this off and on again. Send a pull request.

### Building
To build, you will need to create a file called csharp-github-api.IntegrationTests.dll.config, in the csharp-github-api.IntegrationTests
folder. Simply use the provided example as a starting point:

```
$ cp csharp-github-api.IntegrationTests/csharp-github-api.IntegrationTests.dll.config.example csharp-github-api.IntegrationTests/csharp-github-api.Integration
Tests.dll.config
```

Just add your Github username and password. The file is in .gitignore, so you can't accidently commit it.
```
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <appSettings>
    <add key="username" value="example"/>
    <add key="password" value="password"/>
    <add key="token" value="abcedfg"/>
  </appSettings>
</configuration>
```

### License
Licensed under the Apache License, Version 2.0, details included in the source.

  [1]: http://temporalcohesion.co.uk
  [2]: http://github.com/johnsheehan/RestSharp
  [3]: http://restsharp.org/
  [4]: http://developer.github.com/
