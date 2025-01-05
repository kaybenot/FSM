using System;
using UnityEngine;

public abstract class State<T>
{
    public abstract string Name { get; }
    
    public abstract void Enter(T obj);
    public abstract void Execute(T obj);
    public abstract void Exit(T obj);
}
