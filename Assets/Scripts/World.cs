using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class World : MonoBehaviour
{
    public List<Agent> Agents { get; } = new();
    public List<Wall> Walls { get; private set; } = new();

    GraphBuilder builder;
    Graph graph;
    
    public static World Instance { get; private set; }
    
    public void Awake()
    {
        Instance = this;
        
        graph = new Graph();
        builder = FindAnyObjectByType<GraphBuilder>();
        builder.Initialize(graph);
        builder.fillGraph(Vector2.zero);

        Walls = FindObjectsByType<Wall>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).ToList();
    }
}
