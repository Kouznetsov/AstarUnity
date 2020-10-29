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
    }

    void NextStep()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            NextStep();
    }
}
