# csharp-github-api - A CSharp library to access the GitHub API 
## [http://temporalcohesion.co.uk][1]

### What
It does what it says on the tin*: A C# library for accessing [GitHub's API][4].

Currently targets .NET 3.5

Uses John Sheehan's excellent [RestSharp][3] [REST client][2] library for most of the heavy lifting.

Uses the [Kayak C# web server][5] for integration testing of the API.

### *Status
Very early stages of development. Only authentication is completed at this time.

### License
Licensed under the Apache License, Version 2.0, details included in the source.

### Building
Is currently in a state of flux (i.e. I need to workout how to get UppercuT to do my bidding properly, but, it should be fairly straightforward, like so:
	git clone git://github.com/sgrassie/csharp-github-api.git
	cd csharp-github-api
	build
Then wait for BUILD SUCCEEDED, then grab the assembly from the code_drop folder.

Solutions are provided for both VS2008 and VS2010.
	

  [1]: http://temporalcohesion.co.uk
  [2]: http://github.com/johnsheehan/RestSharp
  [3]: http://restsharp.org/
  [4]: http://develop.github.com/
  [5]: http://kayakhttp.com/
