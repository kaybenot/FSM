using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class GraphBuilder : MonoBehaviour
{
    public Graph graph = new();
    private float dist = 1.5f;
    private Vector2[] directions;

    public void Initialize(Graph graph)
    {
        this.graph = graph;
    }

    void Awake()
    {
        directions = new Vector2[]
        {
            new Vector2(dist, 0), new Vector2(dist, dist), new Vector2(0, dist), new Vector2(-dist, dist),
            new Vector2(-dist, 0), new Vector2(-dist, -dist), new Vector2(0, -dist), new Vector2(dist, -dist)
        };
    }

    public void fillGraph(Vector2 position)
    {
        Queue<Vector2> toVisit = new Queue<Vector2>();
        toVisit.Enqueue(position);
        graph.AddNode(position);

        while (toVisit.Count > 0)
        {
            var current = toVisit.Dequeue();

            foreach (var dir in directions)
            {
                var neighbor = current + dir;

                if (graph.canReach(current, neighbor))
                {
                    if (!graph.nodes.ContainsKey(neighbor)) 
                    {
                        graph.AddNode(neighbor);
                        toVisit.Enqueue(neighbor);
                    }
                    graph.AddEdge(current, neighbor);
                }
            }
        }
        Debug.Log(graph.nodes.Count);
        Debug.Log(graph.edges.Count);
    }

    private void OnDrawGizmos()
    {
        Color nodeColor = Color.red;
        Color edgeColor = Color.green;
        float nodeSize = 0.1f;
        // Draw nodes
        Gizmos.color = nodeColor;
        foreach (var node in graph.nodes.Values)
        {
            Gizmos.DrawSphere(node.Position, nodeSize);
        }

        // Draw edges
        Gizmos.color = edgeColor;
        foreach (var edge in graph.edges)
        {
            Gizmos.DrawLine(edge.Item1.Position, edge.Item2.Position);
        }
    }
}
