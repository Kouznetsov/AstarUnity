using System;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    public Text centerText;
    public Text gCost; // distance from end node
    public Text hCost; // distance from starting node

    private Button _button;
    private Image _image;
    private NodeStatus _status;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SetObstacle);
        gCost.text = "";
        hCost.text = "";
        centerText.text = "";
    }

    public NodeStatus status
    {
        get { return _status; }
        set
        {
            _status = value; 
            UpdateWithStatus();
        }
    }

    private void Start()
    {
        _status = NodeStatus.BLANK;
    }

    void SetObstacle()
    {
        _status = NodeStatus.OBSTACLE;
        UpdateWithStatus();
    }
    
    void UpdateWithStatus()
    {
        switch (_status)
        {
            case NodeStatus.OPEN:
                _image.color = Color.green;
                break;
            case NodeStatus.BLANK:
                _image.color = Color.white;
                break;
            case NodeStatus.OBSTACLE:
                _image.color = Color.black;
                break;
            case NodeStatus.CLOSED:
                _image.color = Color.red;
                break;
            case NodeStatus.START:
                _image.color = Color.cyan;
                centerText.text = "A";
                break;
            case NodeStatus.TARGET:
                _image.color = Color.yellow;
                centerText.text = "B";
                break;
        }
    }
}