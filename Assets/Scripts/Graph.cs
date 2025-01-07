using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class Graph
{
    public Dictionary<Vector2, Node> nodes = new Dictionary<Vector2, Node>();
    public List<(Node, Node)> edges = new List<(Node, Node)>();
    public float agentWidth = 0.5f;

    public void AddNode(Vector2 position)
    {
        if (!nodes.ContainsKey(position))
        {
            nodes[position] = new Node(position);
        }
    }
    public void AddEdge(Vector2 pos1, Vector2 pos2)
    {
        edges.Add((nodes[pos1], nodes[pos2]));
    }

    public bool canReach(Vector2 from, Vector2 to)
    {
        Vector2 direction = (to - from).normalized;
        float distance = Vector2.Distance(from, to);
        var hit = Physics2D.BoxCast(from, new Vector2(agentWidth, agentWidth), 0, direction, distance);
        return hit.collider == null;
    }

    public Color nodeColor = Color.green;
    public Color edgeColor = Color.blue;
    public float nodeSize = 0.1f;
}

public class Node
{
    public Vector2 Position { get; }
    public float G { get; set; } = float.MaxValue; // Cost from start node
    public float H { get; set; } = 0;             // Heuristic cost to goal
    public float F => G + H;                      // Total cost
    public bool Visited { get; set; } = false;

    public Node(Vector2 position)
    {
        Position = position;
    }
}
