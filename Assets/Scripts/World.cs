using UnityEngine;

public class World : MonoBehaviour
{
    GraphBuilder builder;
    Graph graph;
    public void Awake()
    {
        graph = new Graph();
        builder = FindAnyObjectByType<GraphBuilder>();
        builder.Initialize(graph);
        builder.fillGraph(Vector2.zero);
    }
}
