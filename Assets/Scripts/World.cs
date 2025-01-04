using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class World : MonoBehaviour
{
    [SerializeField] private float spacingX;
    [SerializeField] private float spacingY;
    
    private Graph graph;
    private Bounds bounds;

    private void Awake()
    {
        bounds = GetComponent<BoxCollider2D>().bounds;
    }

    public void GenerateGraph()
    {
        graph = new Graph();

        var startPoint = bounds.center - bounds.extents;

        var allNodes = new List<Node>();

        graph.Root = new Node
        {
            Neighbours = new List<Node>(),
            Position = startPoint
        };
        allNodes.Add(graph.Root);

        var current = graph.Root;
        var generated = GenerateNodes(current);
    }

    public List<Node> GenerateNodes(Node node)
    {
        return null;
    }
}
