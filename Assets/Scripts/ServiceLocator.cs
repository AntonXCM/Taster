using UnityEngine;

public static class ServiceLocator
{
    private static TypeDictionary services = new();

    public static void Register<T>(T service) where T : class => services.Replace(service); 

	public static T Get<T>() where T : class
	{
		if (services.ContainsKey<T>())
			return services.Get<T>();
		else
		{
			Debug.Log("Service not found: " + typeof(T).Name);
			return null;
		}
	}
}