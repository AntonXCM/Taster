using System;
using System.Collections.Generic;
using System.IO;

using UnityEngine;
namespace Taster.DataLoaders
{
	public static class Localization
	{
		private static Locale currentLocale;
		private static Dictionary<string,Locale> locales = new();

        public static string currentLanguage;
        public static Sprite CurrentFlag => currentLocale.Flag;

        public static Action OnChangeLanguage;

        public static string Get(string s)
		{
			if (currentLocale.Translations.ContainsKey(s))
				return currentLocale.Translations[s];
			else
			{
				if (locales["en"].Translations.ContainsKey(s))
					return locales["en"].Translations[s];
				else
					return s;
			}
        }

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

            // Загрузка языка
            if (PlayerPrefs.HasKey("Language"))
                currentLanguage = PlayerPrefs.GetString("Language");
            else
            {
                switch (Application.systemLanguage)
                {
                    case SystemLanguage.Russian:
                        currentLanguage = "ru";
                        break;
                    case SystemLanguage.Ukrainian:
                        currentLanguage = "ua";
                        break;
                }
                PlayerPrefs.SetString("Language", currentLanguage);
            }

            currentLocale = locales[currentLanguage];
		}
		public struct Locale
		{
			public Sprite Flag;
			public Dictionary<string, string> Translations;
		}

		public static void SetLanguage(string lg)
		{
			if (locales.ContainsKey(lg))
			{
				currentLanguage = lg;
                currentLocale = locales[currentLanguage];
                PlayerPrefs.SetString("Language", currentLanguage);

				OnChangeLanguage?.Invoke();
            }
		}
    }
}