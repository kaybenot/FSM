using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    public Node Root;
}

public class Node
{
    public Vector2 Position;
    public List<Node> Neighbours;
}
