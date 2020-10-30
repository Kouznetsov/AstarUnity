using System;
using System.Collections;
using System.Collections.Generic;
using Packages.Rider.Editor.UnitTesting;
using UnityEngine;

public class Astar : MonoBehaviour
{
    private List<Node> _grid;
    private int diagMove = Mathf.FloorToInt(Mathf.Sqrt(2) * 10);
    private bool _found = false;
    public Vector2Int start;
    public Vector2Int target;

    private void Start()
    {
        var gr = GetComponent<GridGenerator>();
        _grid = gr.grid;
        SetStartAndTarget();
        Debug.Log("diagMove = " + diagMove);
    }

    void SetStartAndTarget()
    {
        var startNode = _grid.Find(n => n.position.Equals(start));
        var targetNode = _grid.Find(n => n.position.Equals(target));

        startNode.status = NodeStatus.OPEN;
        targetNode.status = NodeStatus.TARGET;
        startNode.h_cost = EstimateHCost(start);
        startNode.g_cost = 0;
        Debug.Log("Estimated H cost = " + EstimateHCost(start));
    }

    int EstimateHCost(Vector2Int a)
    {
        return GetGridDistance(a, target);
    }

    int GetGridDistance(Vector2Int a, Vector2Int b)
    {
        var xDif = Mathf.Abs(a.x - b.x);
        var yDif = Mathf.Abs(a.y - b.y);
        var max = Mathf.Max(xDif, yDif);
        var min = Mathf.Min(xDif, yDif);
        int diag = 0;

        diag = min * diagMove;
        return diag + (max - min) * 10;
    }

    List<Node> GetNeighboursOf(Node node)
    {
        List<Node> neighbours = new List<Node>();
        Vector2Int[] neighboursVectors =
        {
            new Vector2Int(node.position.x - 1, node.position.y - 1),
            new Vector2Int(node.position.x, node.position.y - 1),
            new Vector2Int(node.position.x + 1, node.position.y - 1),
            new Vector2Int(node.position.x - 1, node.position.y),
            new Vector2Int(node.position.x + 1, node.position.y),
            new Vector2Int(node.position.x - 1, node.position.y + 1),
            new Vector2Int(node.position.x, node.position.y + 1),
            new Vector2Int(node.position.x + 1, node.position.y + 1),
        };

        foreach (var neighboursVector in neighboursVectors)
        {
            Node neighbour = _grid.Find(cn => cn.position.Equals(neighboursVector));

            if (neighbour != null && neighbour.status != NodeStatus.CLOSED && neighbour.status != NodeStatus.OBSTACLE)
            {
                var dist = node.g_cost + GetGridDistance(node.position, neighbour.position);
                //neighbour.g_cost = node.g_cost + neighboursVector.g_cost;
                if (neighbour.status != NodeStatus.OPEN || neighbour.g_cost > dist)
                {
                    neighbour.parentNode = node;
                    neighbour.g_cost = dist;
                }

                neighbour.h_cost = EstimateHCost(neighbour.position);
                neighbours.Add(neighbour);
            }
        }

        return neighbours;
    }

    void NextStep()
    {
        if (_found)
            return;
        var lowestOpenFcost = _grid.Count * _grid.Count;
        Node currentNode = null;

        for (var i = 0; i < _grid.Count; i++)
        {
            if (_grid[i].status == NodeStatus.OPEN && (_grid[i].f_cost < lowestOpenFcost || currentNode != null &&
                _grid[i].f_cost == lowestOpenFcost &&
                _grid[i].h_cost < currentNode.h_cost))
            {
                lowestOpenFcost = _grid[i].f_cost;
                currentNode = _grid[i];
            }
        }

        // Find current Node (open & lowest f cost & lowest h cost


        if (currentNode is null)
            return;
        currentNode.h_cost = EstimateHCost(currentNode.position);
        currentNode.status = NodeStatus.CLOSED;


        if (currentNode.position == target)
        {
            Debug.Log("Found the target");
            var targetNode = _grid.Find(n => n.position == target);

            targetNode.status = NodeStatus.PATH;
            for (Node current = targetNode.parentNode; current != null; current = current.parentNode)
            {
                current.status = NodeStatus.PATH;
            }

            _found = true;
            return;
        }

        List<Node> neighbours = GetNeighboursOf(currentNode);

        foreach (var neighbour in neighbours)
        {
            if (neighbour.status != NodeStatus.CLOSED && neighbour.status != NodeStatus.OBSTACLE)
            {
                if (neighbour.status != NodeStatus.OPEN || neighbour.f_cost < currentNode.f_cost)
                {
                    neighbour.status = NodeStatus.OPEN;
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            FindPath();
    }

    void FindPath()
    {
        var startTime = Time.time;
        while (!_found)
        {
            NextStep();
        }
        Debug.Log("Took " + (Time.time - startTime));
    }
}