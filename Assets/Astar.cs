using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar : MonoBehaviour
{
    private List<Node> _grid;
    
    private void Start()
    {
        _grid = GetComponent<GridGenerator>().grid;
        _grid.Find(n => n.status == NodeStatus.START).status = NodeStatus.OPEN;
    }

    void NextStep()
    {
        //var lowestFcost = _grid.Count;
        // TODO
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            NextStep();
    }
}
