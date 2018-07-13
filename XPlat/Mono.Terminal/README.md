
**Mono.Terminal**, or **getline**, is a tiny command line editor for shell applications. 
It included Emacs key bindings, history, customizable completion and incremental search. 
It is equivalent to GNU's readline library, except it is implemented in .NET.

In an age where the shell or command line is more relevant every passing minute, we need 
to have proper command line editing tools everywhere.

This command line editor was put together in a class for .NET shell applications. 
The `Mono.Terminal.LineEdit` class can be used by shell applications to get 
readline-like capabilities without depending on any external libraries.

To use it, just do:

	using Mono.Terminal;

	LineEditor le = new LineEditor ("MyApp");
	while ((s = le.Edit ("prompt> ", "")) != null) {
		Console.WriteLine ("You typed: " + s);
    }
	
It supports the regular cursor editing, Emacs-like editing commands, history, incremental 
search in the history as well as history loading and saving.

The library is self-contained, and can be easily reused in any project. To use it, you 
just need to include the Mono.Terminal (getline) NuGet in your project. This is built 
on top of `System.Console`, so it does not have external library dependencies and 
will work on both Mono and .NET (finally bringing joy to people using command-line 
applications that use `Console.ReadLine`).

It was recently updated to add a popup-based completion and C# heuristics for when to 
automatically trigger code completion:

    // enable code completion
    le.HeuristicsMode = "csharp";
    
    // hook into the event
    le.AutoCompleteEvent += (text, pos) => {
        string[] completions = GetCompletions (text); 
        return new LineEditor.Completion (string.Empty, completions);
    };
    
    // get completion based on `text` and `pos`
    string[] GetCompletions (string text, int pos)
    {
        return new string [] {
            "One",
            "Two",
            "Three",
            "Four",
            "Five",
            "Six",
            "Seven",
            "Eight",
            "Nine",
            "Ten"
        };
    }
