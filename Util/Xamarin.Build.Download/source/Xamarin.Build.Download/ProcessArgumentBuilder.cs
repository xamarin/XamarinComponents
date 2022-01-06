﻿//
// ProcessArgumentBuilder.cs
//
// Copyright (c) 2010 Novell, Inc.
// Copyright (c) 2011-2016 Xamarin Inc.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Text;
using System.Collections.Generic;

namespace Xamarin.MacDev
{
	/// <summary>
	/// Builds a process argument string.
	/// </summary>
	class ProcessArgumentBuilder
	{
		readonly HashSet<string> hash = new HashSet<string> ();
		readonly StringBuilder builder = new StringBuilder ();

		public string ProcessPath {
			get; private set;
		}

		public ProcessArgumentBuilder ()
		{
		}

		public ProcessArgumentBuilder (string processPath)
		{
			ProcessPath = processPath;
		}

		public int Length {
			get { return builder.Length; }
		}

		/// <summary>
		/// Adds an argument without escaping or quoting.
		/// </summary>
		public void Add (string argument)
		{
			if (builder.Length > 0)
				builder.Append (' ');

			builder.Append (argument);
			hash.Add (argument);
		}

		/// <summary>
		/// Adds multiple arguments without escaping or quoting.
		/// </summary>
		public void Add (params string[] args)
		{
			foreach (var a in args)
				Add (a);
		}

		/// <summary>
		/// Adds a formatted argument, quoting and escaping as necessary.
		/// </summary>
		public void AddQuotedFormat (string argumentFormat, params object[] values)
		{
			AddQuoted (string.Format (argumentFormat, values));
		}

		public void AddQuotedFormat (string argumentFormat, object val0)
		{
			AddQuoted (string.Format (argumentFormat, val0));
		}

		static void AppendQuoted (StringBuilder quoted, string text)
		{
			quoted.Append ("\"");
			quoted.Append (text);
			quoted.Append ("\"");
		}

		/// <summary>Adds an argument, quoting and escaping as necessary.</summary>
		/// <remarks>The .NET process class does not support escaped
		/// arguments, only quoted arguments with escaped quotes.</remarks>
		public void AddQuoted (string argument)
		{
			if (argument == null)
				return;

			if (builder.Length > 0)
				builder.Append (' ');

			AppendQuoted (builder, argument);
			hash.Add (argument);
		}

		/// <summary>
		/// Adds multiple arguments, quoting and escaping each as necessary.
		/// </summary>
		public void AddQuoted (params string[] args)
		{
			foreach (var a in args)
				AddQuoted (a);
		}

		/// <summary>
		/// Contains the specified argument.
		/// </summary>
		/// <param name="argument">Argument.</param>
		public bool Contains (string argument)
		{
			return hash.Contains (argument);
		}

		/// <summary>Quotes a string, escaping if necessary.</summary>
		/// <remarks>The .NET process class does not support escaped
		/// arguments, only quoted arguments with escaped quotes.</remarks>
		public static string Quote (string text)
		{
			var quoted = new StringBuilder ();

			AppendQuoted (quoted, text);

			return quoted.ToString ();
		}

		public override string ToString ()
		{
			return builder.ToString ();
		}

		static string GetArgument (StringBuilder builder, string buf, int startIndex, out int endIndex, out Exception ex)
		{
			bool escaped = false;
			char qchar, c = '\0';
			int i = startIndex;

			builder.Clear ();
			switch (buf[startIndex]) {
				case '\'': qchar = '\''; i++; break;
				case '"': qchar = '"'; i++; break;
				default: qchar = '\0'; break;
			}

			while (i < buf.Length) {
				c = buf[i];

				if (c == qchar && !escaped) {
					// unescaped qchar means we've reached the end of the argument
					i++;
					break;
				}

				if (c == '\\') {
					escaped = true;
				} else if (escaped) {
					builder.Append (c);
					escaped = false;
				} else if (qchar == '\0' && (c == ' ' || c == '\t')) {
					break;
				} else if (qchar == '\0' && (c == '\'' || c == '"')) {
					string sofar = builder.ToString ();
					string embedded;

					if ((embedded = GetArgument (builder, buf, i, out endIndex, out ex)) == null)
						return null;

					i = endIndex;
					builder.Clear ();
					builder.Append (sofar);
					builder.Append (embedded);
					continue;
				} else {
					builder.Append (c);
				}

				i++;
			}

			if (escaped || (qchar != '\0' && c != qchar)) {
				ex = new FormatException (escaped ? "Incomplete escape sequence." : "No matching quote found.");
				endIndex = -1;
				return null;
			}

			endIndex = i;
			ex = null;

			return builder.ToString ();
		}

		static bool TryParse (string commandline, out string[] argv, out Exception ex)
		{
			var builder = new StringBuilder ();
			var args = new List<string> ();
			string argument;
			int i = 0, j;
			char c;

			while (i < commandline.Length) {
				c = commandline[i];
				if (c != ' ' && c != '\t') {
					if ((argument = GetArgument (builder, commandline, i, out j, out ex)) == null) {
						argv =  null;
						return false;
					}

					args.Add (argument);
					i = j;
				}

				i++;
			}

			argv = args.ToArray ();
			ex = null;

			return true;
		}

		public static bool TryParse (string commandline, out string[] argv)
		{
			Exception ex;

			return TryParse (commandline, out argv, out ex);
		}

		public static string[] Parse (string commandline)
		{
			string[] argv;
			Exception ex;

			if (!TryParse (commandline, out argv, out ex))
				throw ex;

			return argv;
		}
	}
}