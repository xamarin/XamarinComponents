
**Mono.Options** is a beautiful command line parsing library. It is small, succinct, a 
joy to use, easy and powerful, all in one.

It is one of those libraries that does more with less. Something that every 
programmer aspires to write, but that we seldom achieve.

It has also struck a good balance for Unix and Windows developers as options can 
be used on both systems, and map well to practices on both systems. It took a long 
time to get the right "blend" of parsing, but I think we have achieved it.

There are a few steps to get everything in place. First, we need to set up the 
options expected:

    // these variables will be set when the command line is parsed
    var verbosity = 0;
    var shouldShowHelp = false;
    var names = new List<string> ();
    var repeat = 1;

    // thses are the available options, not that they set the variables
    var options = new OptionSet { 
        { "n|name=", "the name of someone to greet.", n => names.Add (n) }, 
        { "r|repeat=", "the number of times to repeat the greeting.", (int r) => repeat = r }, 
        { "v", "increase debug message verbosity", v => { if (v != null) ++verbosity; } }, 
        { "h|help", "show this message and exit", h => shouldShowHelp = h != null },
    };

Then, in the `static void Main (string[] args)` method, we parse the incoming 
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

Finally, to show a "help" screen / output the available arguments to
console, there is a nifty `WriteOptionDescriptions` method:

    // show some app description message
    Console.WriteLine ("Usage: OptionsSample.exe [OPTIONS]+ message");
    Console.WriteLine ("Greet a list of individuals with an optional message.");
    Console.WriteLine ("If no message is specified, a generic greeting is used.");
    Console.WriteLine ();
    
    // output the options
    Console.WriteLine ("Options:");
    options.WriteOptionDescriptions (Console.Out);
