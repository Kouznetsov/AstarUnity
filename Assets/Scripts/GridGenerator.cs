using System;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLine
{
    public Vector2Int start;
    public Vector2Int end;
}

public class GridGenerator : MonoBehaviour
{
    public int nodesWidth;
    public int nodesHeight;
    public Canvas canvas;
    public GameObject nodePrefab;
    public Vector2Int[] obstacleLines;

    [HideInInspector] public List<Node> grid = new List<Node>();

    private void Awake()
    {
        GenerateGrid();
        //SetObstacleLines();
    }
    
    void GenerateGrid()
    {
        float heightUnit = 1f / nodesHeight;
        float widthUnit = 1f / nodesWidth;


        for (var y = 0; y < nodesHeight; y++)
        {
            for (var x = 0; x < nodesWidth; x++)
            {
                var go = Instantiate(nodePrefab, canvas.transform, false);
                var rt = go.GetComponent<RectTransform>();

                rt.offsetMin = Vector2.zero;
                rt.offsetMax = Vector2.zero;
                rt.anchorMin = new Vector2(x * (widthUnit), y * (heightUnit));
                rt.anchorMax = new Vector2((x + 1) * widthUnit, (y + 1) * heightUnit);
                var node = go.GetComponent<Node>();
                node.position = new Vector2Int(x, y);
                grid.Add(node);
            }
        }
    }

    void SetObstacleLines()
    {
        for (var i = 0; i < obstacleLines.Length; i += 2)
        {
            var current = obstacleLines[i];
            var end = obstacleLines[i + 1];

            while (!current.Equals(end))
            {
                grid.Find(n => n.position == current).status = NodeStatus.OBSTACLE;
                if (current.x != end.x)
                    current.x += current.x > end.x ? -1 : 1;
                if (current.y != end.y)
                    current.y += current.y > end.y ? -1 : +1;
            }
        }
    }
}