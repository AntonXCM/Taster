using System.Collections.Generic;
using System.IO;
using System.Linq;

using UnityEngine;
namespace Taster.DataLoaders
{
	public static class Localization
	{
		private static Locale currentLocale;
		private static Dictionary<string,Locale> locales = new();
		public static string Get(string s) => currentLocale.Translations[s];

		static Localization()
		{
			LoaderUtils.IterateInPacks(dataPath =>
			{
				foreach(var file in Directory.GetFiles(Path.Combine(dataPath, "locales")))
				{
					if(file.EndsWith(".meta")) continue;
					string localeName = Path.GetFileNameWithoutExtension(file);
					if(locales.ContainsKey(localeName))
					{
						foreach(var translation in GetTranslations())
							locales[localeName].Translations.TryAdd(translation.Key, translation.Value);
					} else
					{
						Sprite sprite = LoaderUtils.LoadSprite(Path.Combine(dataPath,LoaderUtils.GRAPHICS_FOLDER_NAME,"locales",localeName+".png"));
						locales[localeName] = new Locale { Flag = sprite, Translations = new Dictionary<string, string>(GetTranslations()) };
					}

					IEnumerable<KeyValuePair<string,string>> GetTranslations()
					{
						foreach(string row in File.ReadLines(file))
						{
							string[] strings = row.Split(';');
							yield return new(strings[0], strings[1].Trim());
						}
					}
				}
			});
			currentLocale = locales.First().Value;
		}
		public struct Locale
		{
			public Sprite Flag;
			public Dictionary<string, string> Translations;
		}
	}
}