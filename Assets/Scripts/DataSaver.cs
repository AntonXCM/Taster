using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;
/// <summary>
/// Статический класс для сохранения и чтения файлов JSON
/// </summary>
public static class DataSaver
{
    /// <summary>
    /// Сохранение объекта в JSON
    /// </summary>
    /// <param name="file">Объект</param>
    /// <param name="name">
    /// Название файла 
    /// <para>
    /// Указывать без формата!
    /// </para>
    /// </param>
    /// <param name="directory">
    /// Путь к файлу после <see cref="Application.persistentDataPath"/>
    /// <para>
    /// Должен начинатся и заканчиватся с <c>'/'</c>
    /// </para>
    /// </param>
    /// 
    public static void Save(object file,string name, string directory = "/")
    {
        directory = Application.persistentDataPath + directory;
        if(!Directory.Exists(directory)) Directory.CreateDirectory(directory);
        
        string json = JsonConvert.SerializeObject(file);

        name = string.Concat( directory, name, ".json");
        if(!File.Exists(name))
        {
            StreamWriter writer = File.CreateText(name);
            writer.WriteLine(json);
            writer.Close();
        }
        else
            File.WriteAllText(name,json);
    }
    public static void Open<T>(string name,out T ouT)
    {
        StringBuilder builder = new();
        builder.AppendJoin(string.Empty,Application.persistentDataPath,'/',name,".json");
        string path = builder.ToString();
        string json;
        if(File.Exists(path))
        {
            json = File.ReadAllText(path);
            ouT = JsonConvert.DeserializeObject<T>(json);
        }
        else OpenPresaved(name,out ouT);
    }
    public static void OpenPresaved<T>(string name,out T ouT)
    {
        StringBuilder builder = new();
        builder.AppendJoin(string.Empty, "Presaved Data/", name);
        string path = builder.ToString();
        string json = Resources.Load<TextAsset>(path).text;
        ouT = JsonConvert.DeserializeObject<T>(json);
    }

	public static void SaveDict<T,T2>(Dictionary<T2,T> dictionary, string name)
    {
		string path = Path.Combine( Application.persistentDataPath,name);
		if(Directory.Exists(path)) 
            Directory.Delete(path, true);
        Directory.CreateDirectory(path);

        foreach(var pair in dictionary)
            Save(pair.Value, pair.Key.ToString(), string.Concat(Path.DirectorySeparatorChar, name, Path.DirectorySeparatorChar));
    }
	public static void OpenDict<T>(string name, out Dictionary<int, T> result)
	{
		StringBuilder builder = new();
        builder.AppendJoin(string.Empty, Application.persistentDataPath, '/', name);
		string path = builder.ToString();
		result = new();
        if(Directory.Exists(path))
            foreach(string file in Directory.GetFiles(path, "*.json"))
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                string filePath = Path.Combine(name,fileName);

                Open(filePath, out T t);
                result[int.Parse(fileName)] = t;
            }
    }

    public static bool IsSaveExists(string name, string directory = "/")
    {
        directory = Application.persistentDataPath + directory;
        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

        name = string.Concat(directory, name, ".json");
        return File.Exists(name);
    }
}
