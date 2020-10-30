using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    public Text centerText;
    public Text gCostText; // distance from end node
    public Text hCostText; // distance from starting node

    [HideInInspector] public Vector2Int position;

    public int f_cost
    {
        get { return _g_cost + _h_cost; }
    }

    public int g_cost
    {
        get { return _g_cost; }
        set
        {
            _g_cost = value;
            gCostText.text = $"{_g_cost}";
            centerText.text = $"{(_g_cost + _h_cost)}";
        }
    }

    public int h_cost
    {
        get { return _h_cost; }
        set
        {
            _h_cost = value;
            hCostText.text = $"{_h_cost}";
            centerText.text = $"{(_g_cost + _h_cost)}";
        }
    }

    [HideInInspector] public Node parentNode;

    private int _g_cost;
    private int _h_cost;
    private Button _button;
    private Image _image;
    private NodeStatus _status;


    private void Awake()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SetObstacle);
        EventTrigger trigger = _button.gameObject.AddComponent<EventTrigger>();
        var pointerDown = new EventTrigger.Entry();
        pointerDown.eventID = EventTriggerType.PointerEnter;
        pointerDown.callback.AddListener((e) => status = NodeStatus.OBSTACLE);
        trigger.triggers.Add(pointerDown);
        gCostText.text = "";
        hCostText.text = "";
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
            case NodeStatus.PATH:
                _image.color = Color.magenta;
                break;
        }
    }
}