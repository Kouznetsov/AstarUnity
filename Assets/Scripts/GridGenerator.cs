using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public int nodesWidth;
    public int nodesHeight;
    public Canvas canvas;
    public GameObject nodePrefab;

    public Vector2 start;
    public Vector2 target;

    [HideInInspector] public List<Node> grid = new List<Node>();

    private void Start()
    {
        GenerateGrid();
        SetStartAndTarget();
    }

    void SetStartAndTarget()
    {
        grid[(int) (nodesWidth * start.y + start.x)].status = NodeStatus.START;
        grid[(int) (nodesWidth * target.y + target.x)].status = NodeStatus.TARGET;
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
                grid.Add(go.GetComponent<Node>());
            }
        }
    }
}