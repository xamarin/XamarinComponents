using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Android.Content;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

using FloatingSearchViews.Suggestions.Models;

namespace FloatingSearchViewSample
{
	public static class DataHelper
	{
		private static List<Colour> colors = new List<Colour> ();

		public static async Task<List<ISearchSuggestion>> GetHistoryAsync (Context context, int count)
		{
			await InitColorWrapperListAsync (context);

			return await Task.Run (() => {
				var suggestionList = new List<ISearchSuggestion> ();
				for (int i = 0; i < count; i++) {
					suggestionList.Add (new ColorSuggestion (colors [i]) {
						IsHistory = true
					});
				}
				return suggestionList;
			});
		}

		public static async Task<List<ISearchSuggestion>> FindAsync (Context context, string query)
		{
			await InitColorWrapperListAsync (context);

			return await Task.Run (() => {
				return colors
					.Where (c => c.Name.IndexOf (query, StringComparison.OrdinalIgnoreCase) != -1)
					.Select (c => (ISearchSuggestion)new ColorSuggestion (c))
					.ToList ();
			});
		}

		private static Task InitColorWrapperListAsync (Context context)
		{
			return Task.Run (() => {
				if (colors.Count == 0) {
					lock (colors) {
						if (colors.Count == 0) {
							using (var stream = context.Assets.Open ("colors.json"))
							using (var reader = new StreamReader (stream))
							using (var json = new JsonTextReader (reader))
								colors = JArray.Load (json)
									.Select (c => new Colour (c ["name"], c ["rgb"], c ["hex"]))
									.ToList ();
						}
					}
				}
			});
		}
	}
}
