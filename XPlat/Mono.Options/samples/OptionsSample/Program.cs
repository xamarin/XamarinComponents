using System;
using System.Collections.Generic;

using Mono.Options;

namespace OptionsSample
{
	class Program
	{
		private static int verbosity;

		public static void Main (string[] args)
		{
			var shouldShowHelp = false;
			var names = new List<string> ();
			var repeat = 1;

			var p = new OptionSet { 
				{ "n|name=", 	"the name of someone to greet.", 				n => names.Add (n) }, 
				{ "r|repeat=", 	"the number of times to repeat the greeting.", 	(int r) => repeat = r }, 
				{ "v", 			"increase debug message verbosity", 			v => { if (v != null) ++verbosity; } }, 
				{ "h|help",  	"show this message and exit", 					h => shouldShowHelp = h != null },
			};

			List<string> extra;
			try {
				extra = p.Parse (args);
			} catch (OptionException e) {
				Console.Write ("OptionsSample.exe: ");
				Console.WriteLine (e.Message);
				Console.WriteLine ("Try `OptionsSample.exe --help' for more information.");
				return;
			}

			if (shouldShowHelp) {
				ShowHelp (p);
				return;
			}

			string message;
			if (extra.Count > 0) {
				message = string.Join (" ", extra.ToArray ());
				Debug ("Using new message: {0}", message);
			} else {
				message = "Hello {0}!";
				Debug ("Using default message: {0}", message);
			}

			foreach (string name in names) {
				for (int i = 0; i < repeat; ++i)
					Console.WriteLine (message, name);
			}
		}

		private static void ShowHelp (OptionSet p)
		{
			Console.WriteLine ("Usage: OptionsSample.exe [OPTIONS]+ message");
			Console.WriteLine ("Greet a list of individuals with an optional message.");
			Console.WriteLine ("If no message is specified, a generic greeting is used.");
			Console.WriteLine ();
			Console.WriteLine ("Options:");
			p.WriteOptionDescriptions (Console.Out);
		}

		private static void Debug (string format, params object[] args)
		{
			if (verbosity > 0) {
				Console.Write ("# ");
				Console.WriteLine (format, args);
			}
		}
	}
}
