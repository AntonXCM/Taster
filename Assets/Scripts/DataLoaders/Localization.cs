using System;
using System.Collections.Generic;
using UnityEngine;

namespace Taster.DataLoaders
{
    [CreateAssetMenu(fileName = "Localization", menuName = "Scriptable Objects/Localization")]
    public class Localization : ScriptableObject
    {
        [SerializeField] Sprite[] Flags;
		[SerializeField] TextAsset[] Assets;

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

        [RuntimeInitializeOnLoadMethod]
        private static void OnGameStart()
        {
            Localization loadBase = Resources.Load<Localization>("Localization");

            for (int i = 0; i < loadBase.Assets.Length; i++)
            {
                TextAsset file = loadBase.Assets[i];

                Sprite sprite = loadBase.Flags[i];

                locales[file.name] = new Locale { Flag = sprite, Translations = new Dictionary<string, string>(GetTranslations(file)) };

                IEnumerable<KeyValuePair<string, string>> GetTranslations(TextAsset file)
                {
                    string[] rows = file.text.Split('\n');
                    foreach (string row in rows)
                    {
                        string[] strings = row.Split(';');
                        yield return new(strings[0], strings[1].Trim());
                    }
                }
            }

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
					default:
						currentLanguage = "en";
						break;
                }
                PlayerPrefs.SetString("Language", currentLanguage);
            }

            currentLocale = locales[currentLanguage];
		}
		[Serializable]
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