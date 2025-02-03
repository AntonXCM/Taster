using System;
using System.Collections.Generic;

public class TypeDictionary : Dictionary<Type,object> 
{
    public void Add<T>(T objecT) => Add(typeof(T),objecT);
    public void Replace<T>(T objecT) => this[typeof(T)] = objecT;
    public T Get<T>() => (T)this[typeof(T)];
    public bool ContainsKey<T>() => ContainsKey(typeof(T));
}