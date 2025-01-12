using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Graph
{
    public Dictionary<Vector2, Node> nodes = new Dictionary<Vector2, Node>();
    public List<(Node, Node)> edges = new List<(Node, Node)>();
    public float agentWidth = 0.5f;
    
    public static Graph Instance { get; private set; }

    public Graph()
    {
        Instance = this;
    }

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

    public List<Vector2> AStar(Vector2 start, Vector2 goal)
    {
        if (!nodes.ContainsKey(start) || !nodes.ContainsKey(goal))
        {
            Debug.LogError("Start or goal node does not exist in the graph.");
            return null;
        }

        Node startNode = nodes[start];
        Node goalNode = nodes[goal];

        var openSet = new SortedSet<Node>(Comparer<Node>.Create((a, b) => a.F.CompareTo(b.F)));
        var closedSet = new HashSet<Node>();

        startNode.G = 0;
        startNode.H = Heuristic(startNode.Position, goalNode.Position);

        openSet.Add(startNode);

        Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();

        while (openSet.Count > 0)
        {
            Node current = openSet.First();
            openSet.Remove(current);

            if (current == goalNode)
            {
                return ReconstructPath(cameFrom, current);
            }

            closedSet.Add(current);

            foreach (var neighbor in GetNeighbors(current))
            {
                if (closedSet.Contains(neighbor)) continue;

                float tentativeG = current.G + Vector2.Distance(current.Position, neighbor.Position);

                if (!openSet.Contains(neighbor))
                {
                    neighbor.G = tentativeG;
                    neighbor.H = Heuristic(neighbor.Position, goalNode.Position);
                    openSet.Add(neighbor);
                    cameFrom[neighbor] = current;
                }
                else if (tentativeG < neighbor.G)
                {
                    // Update G cost and adjust position in the sorted set
                    openSet.Remove(neighbor);
                    neighbor.G = tentativeG;
                    cameFrom[neighbor] = current;
                    openSet.Add(neighbor);
                }
            }
        }

        Debug.LogError("No path found.");
        return null;
    }

    private List<Vector2> ReconstructPath(Dictionary<Node, Node> cameFrom, Node current)
    {
        var path = new List<Vector2> { current.Position };

        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Add(current.Position);
        }

        path.Reverse();
        return path;
    }

    private IEnumerable<Node> GetNeighbors(Node node)
    {
        foreach (var edge in edges)
        {
            if (edge.Item1 == node && canReach(node.Position, edge.Item2.Position))
            {
                yield return edge.Item2;
            }
            else if (edge.Item2 == node && canReach(node.Position, edge.Item1.Position))
            {
                yield return edge.Item1;
            }
        }
    }

    private float Heuristic(Vector2 a, Vector2 b)
    {
        return Vector2.Distance(a, b);
    }
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
