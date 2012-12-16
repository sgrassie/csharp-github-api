# csharp-github-api - A CSharp library to access the GitHub API 
## [http://temporalcohesion.co.uk][1]

### What
It does what it says on the tin*: A C# library for accessing [GitHub's API][4].

Targets .NET 4

Uses John Sheehan's excellent [RestSharp][3] [REST client][2] library for most of the heavy lifting.

### *Status
Barely does anything at the moment. I'm lazy. Started working on this off and on again. Send a pull request.

### Building
For now, just fire up VS and hit build.
To run the integration tests which require authentication, you'll need to create a file called csharp-github-api.IntegrationTests.dll.config. Simply use the provided example as a starting point. Just add your Github username and password. The file is in .gitignore, so you can't accidently commit it.

### License
Licensed under the Apache License, Version 2.0, details included in the source.

  [1]: http://temporalcohesion.co.uk
  [2]: http://github.com/johnsheehan/RestSharp
  [3]: http://restsharp.org/
  [4]: http://develop.github.com/
