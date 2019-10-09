using System;
using System.IO;
using Newtonsoft.Json;
namespace MaterialSample {
	public class SamplesManager {
		#region Singleton

		static Lazy<SamplesManager> lazy = new Lazy<SamplesManager> (() => new SamplesManager ());
		public static SamplesManager SharedInstance { get => lazy.Value; }

		SamplesManager () { }

		#endregion

		#region Class Variables

		Component [] components;

		#endregion

		#region Public Functionality

		public Component [] Components {
			get {
				if (components != null)
					return components;

				using (TextReader file = File.OpenText ("Samples.js")) {
					var serializaer = new JsonSerializer ();
					components = serializaer.Deserialize<Component []> (new JsonTextReader (file));
				}

				return components;
			}
		}

		#endregion
	}
}
