using System;
using System.Collections.Generic;
using System.IO;

using Taster.Foods;

using UnityEngine;

public static class Database
{
	public static Ingredient[] Ingredients;
	public static string[] Poisons;
	static Database()
	{
		string dataPath = Path.Combine(Application.persistentDataPath,"Default Pack"), input = File.ReadAllText(Path.Combine(dataPath, "data", "ingredients.csv"));
		List<Ingredient> ingredients = new();
		foreach(string line in input.Split('\n'))
		{
			string[] parts = line.Split(';');
			if(parts.Length < 2)
			{
				Console.WriteLine("Некорректная строка: " + line);
				continue;
			}
			string name = parts[0].Trim();
			{
				Texture2D texture = new(95,95);
				texture.LoadImage(File.ReadAllBytes(Path.Combine(dataPath, "gfx", "ingredients", name + ".png")));

				Sprite sprite = Sprite.Create(texture, new Rect(0,0,95,95),Vector2.one/2,100, 1,SpriteMeshType.Tight,new(0.5f,0.5f,1,1));
				sprite.name = name;
				ingredients.Add(new(int.Parse(parts[1].Trim()), name, sprite));
			}
		}
		Database.Ingredients = ingredients.ToArray();
		Poisons = File.ReadAllText(Path.Combine(dataPath, "data", "poisons.txt")).Split(',');
	}
}