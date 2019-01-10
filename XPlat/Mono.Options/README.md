# Mono.Options

**Mono.Options** is a small, but powerful, command line parsing library.  It has 
also struck a good balance for Unix and Windows developers as options can 
be used on both systems, and map well to practices on both systems.

This library can be downloaded from NuGet ([Mono.Options](https://www.nuget.org/packages/Mono.Options)):

```
PM> Install-Package Mono.Options
```

This package supports almost all .NET platforms:

 - Full .NET Framework 4.0+ (Client Profile)
 - .NET Core 1.0
 - .NET Standard 1.6+
 - Portable Class Libraries (Profile 259)

## Getting Started

Lots more information is available in the [Getting Started Guide](GettingStarted.md), 
but some of the quick start information can be found here or in 
the [Details Guide](Details.md).

There are a few steps to get everything in place. First, we need to set up the 
options expected:

    // these variables will be set when the command line is parsed
    var verbosity = 0;
    var shouldShowHelp = false;
    var names = new List<string> ();
    var repeat = 1;

    // these are the available options, not that they set the variables
    var options = new OptionSet { 
        { "n|name=", "the name of someone to greet.", n => names.Add (n) }, 
        { "r|repeat=", "the number of times to repeat the greeting.", (int r) => repeat = r }, 
        { "v", "increase debug message verbosity", v => { if (v != null) ++verbosity; } }, 
        { "h|help", "show this message and exit", h => shouldShowHelp = h != null },
    };

Then, in the `static void Main (string[] args)` method, we can parse the incoming 
arguments and get a list of any extras:

    List<string> extra;
    try {
        // parse the command line
        extra = options.Parse (args);
    } catch (OptionException e) {
        // output some error message
        Console.Write ("greet: ");
        Console.WriteLine (e.Message);
        Console.WriteLine ("Try `greet --help' for more information.");
        return;
    }

This will read in the argements from the command line and set the variables. For
example, if the command line is:

    > greet.exe -n Matthew Welcome to Xamarin {0}!

Then, the variables will be processed and result in:

    verbosity      == 0
    shouldShowHelp == false
    names          == ["Matthew"]
    repeat         == 1
    extras         == ["Welcome to Xamarin {0}!"]

# License

**The MIT License (MIT)**

Copyright (c) .NET Foundation Contributors

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

20181107
