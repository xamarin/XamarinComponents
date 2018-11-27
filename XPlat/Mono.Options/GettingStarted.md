
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
        { "v", "increase debug message verbosity", v => { 
            if (v != null) 
                ++verbosity; 
        } }, 
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

## Using OptionSet and Option
 
 Use of `OptionSet` is split up into two parts:
 
 1.  Initialization.
 2.  Parsing.
 
During the _initialization_ phase, new `Option` instances are created and associated with an action 
to perform when the `Option` requirements are met (e.g. when a required value has been 
encountered). This phase is not thread safe. All options added during this phase are considered 
to have been _registered_.
 
    OptionSet p = new OptionSet () {
      { "option-a", v => { /* action to perform */ } },
    };

There are three ways to add `Option`s to the `OptionSet`:

 1. With C# collection initializers, as used above.
 2. Explicitly by calling `OptionSet.Add(string, Action<string>)` and the other `Add` 
    overloads.
 3. By creating a new subclass of `Option` and adding it via `OptionSet.Add(Option)`. 
    This is not recommended, but is available if you require more direct option handling than the 
    default `NDesk.Options.Option`, implementation provides.

During the _parsing_ phase, an `IEnumerable<string>` is enumerated, looking for arguments which match 
a registered option, and invoking the corresponding action when an option and associated (optional) 
value is encountered. During this phase, the `OptionSet` instance itself is thread safe, but full 
thread safety depends upon thread-safety of the registered actions. Any option-like strings for names 
that haven't been registered, e.g. `--this-was-never-registered=false`, and all arguments that are 
not used as option values are returned from `OptionSet.Parse(IEnumerable<string>)` or processed by 
the default handler `<>`, if registered.

## Note to Inheritors
 
Subclasses can override the following `virtual` methods to customize option parsing behavior:
 
 * `OptionSet.Parse(string, OptionContext)`
 * `OptionSet.CreateOptionContext`
 
## Simple Example

The following example demonstrates some simple usage of `OptionSet`:

    // these variables will be set when the command line is parsed
    var verbosity = 0;
    var shouldShowHelp = false;
    var names = new List<string> ();
    var repeat = 1;

    // thses are the available options, not that they set the variables
    var options = new OptionSet { 
        { "n|name=", "the name of someone to greet.", n => names.Add (n) }, 
        { "r|repeat=", "the number of times to repeat the greeting.", (int r) => repeat = r }, 
        { "v", "increase debug message verbosity", v => { 
            if (v != null) 
                ++verbosity; 
        } }, 
        { "h|help", "show this message and exit", h => shouldShowHelp = h != null },
    };

The output (under the influence of different command-line arguments) is:

    $ mono greet.exe --help
    Usage: greet [OPTIONS]+ message
    Greet a list of individuals with an optional message.
    If no message is specified, a generic greeting is used.
    
    Options:
      -n, --name=NAME            the NAME of someone to greet.
      -r, --repeat=TIMES         the number of TIMES to repeat the greeting.
                                   this must be an integer.
      -v                         increase debug message verbosity
      -h, --help                 show this message and exit

Different forms of arguments can be parsed:

    $ mono greet.exe -v- -n A -name=B --name=C /name D -nE
    Hello A!
    Hello B!
    Hello C!
    Hello D!
    Hello E!

Extras are parsed:

    $ mono greet.exe -v -n E custom greeting for: {0}
    # Using new message: custom greeting for: {0}
    custom greeting for: E
    
Errors are handled:

    $ mono greet.exe -r not-an-int
    greet: Could not convert string `not-an-int' to type Int32 for option `-r'.
    Try `greet --help' for more information.

Notice how the output produced by `--help` uses the descriptions provided during `OptionSet` 
initialization. Notice that the `Option` requiring a value (`n|name=`) can use multiple 
different forms of invocation, including: 

 - `-n value`
 - `-n=value`
 - `-name value`
 - `-name=value`
 - `--name value`
 - `--name=value`
 - `/name value`
 - `/name=value`

Notice also that the boolean `v` option can take three separate forms: `-v` and `-v+`, 
which both enable the option, and `-v-`, which disables the option. (The second `greet` 
invocation uses `-v-`, which is why no debug messages are shown.)

Finally, note that the action can specify a type to use. If no type is provided, the action 
parameter will be a `string`. If a type is provided, then `System.ComponentModel.TypeConverter`
will be used to convert a string to the specified type.

    var show_help = false;
    var macros = new Dictionary<string, string>();
    bool create = false, extract = false, list = false;
    string output = null, input = null;
    string color  = null;

    var p = new OptionSet {
        // gcc-like options
        { "D:", "Predefine a macro with an (optional) value.", (m, v) => {
            if (m == null)
                throw new OptionException ("Missing macro name for option -D.", "-D");
            macros.Add (m, v);
        } },
        { "d={-->}{=>}", "Alternate macro syntax.", (m, v) => macros.Add (m, v) },
        { "o=", "Specify the output file", v => output = v },

        // tar-like options
        { "f=", "The input file", v => input = v },
        { "x", "Extract the file", v => extract = v != null },
        { "c", "Create the file", v => create = v != null },
        { "t", "List the file", v => list = v != null },

        // ls-like optional values
        { "color:", "control whether and when color is used", 
            v => color = v },

        // other...
        { "h|help", "show this message and exit", v => show_help = v != null },
        
        // default
        { "<>", v => Console.WriteLine ("def handler: color={0}; arg={1}", color, v)},
    };

The output (under the influence of different command-line arguments) is:

    $ mono bundling.exe --help
    Usage: bundling [OPTIONS]+
    Demo program to show the effects of bundling options and their values
    
    Options:
      -D[=VALUE1:VALUE2]         Predefine a macro with an (optional) value.
      -d=VALUE1-->VALUE2         Alternate macro syntax.
      -o=VALUE                   Specify the output file
      -f=VALUE                   The input file
      -x                         Extract the file
      -c                         Create the file
      -t                         List the file
          --color[=VALUE]        control whether and when color is used
      -h, --help                 show this message and exit

The output (under the influence of different command-line arguments) is:

    $ mono bundling.exe -D
    bundling: Missing macro name for option -D.
    Try `greet --help' for more information.

Macros can also be parsed now:

    $ mono bundling.exe -DA -DB=C "-dD-->E" "-dF=>G" -d "H=>I" -cf input --color -ooutput
    Macros:
      A=<null>
      B=C
      D=E
      F=G
      H=I
    Options:
      Input File: input
      Ouptut File: output
      Create: True
      Extract: False
      List: False
      Color: <null>
