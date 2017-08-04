using System;

using Mono.Terminal;

namespace TerminalSample
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var le = new LineEditor ("foo") {
				HeuristicsMode = "csharp"
			};

			le.AutoCompleteEvent += (text, pos) => {
				var completions = new  [] {
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
				return new LineEditor.Completion (string.Empty, completions);
			};

			string s;

			while ((s = le.Edit ("shell> ", "")) != null) {
				Console.WriteLine ("----> [{0}]", s);
			}
		}
	}
}
