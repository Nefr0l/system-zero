using System.Collections.Generic;
using Classes;
using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    public List<Connection> Connections;

    private GameManager gameManager;
    
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        
        foreach (var c in Connections)
        {
            c.LineObject = new GameObject("Line_" + 1);
            c.LineObject.transform.SetParent(transform); 
    
            c.LineObject.AddComponent<LineRenderer>();

            LineRenderer line = c.LineObject.GetComponent<LineRenderer>();

            line.material = new Material(Shader.Find("Sprites/Default"));
            line.startColor = gameManager.lineColorDisconnected;
            line.endColor = line.endColor;
            line.startWidth = 0.1f;
            line.endWidth = line.startWidth;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, c.ConnectionTo.transform.position);
        }
        HideLines();
    }

    public void ShowLines()
    {
        foreach (var c in Connections)
        {
            LineRenderer line = c.LineObject.GetComponent<LineRenderer>();
            line.enabled = true;
        }
    }
    
    public void HideLines()
    {
        foreach (var c in Connections)
        {
            LineRenderer line = c.LineObject.GetComponent<LineRenderer>();
            line.enabled = false;
        }
    }
    
    public void UpdateLines()
    {
        int validConnections = 0;
        
        foreach (var c in Connections)
        {
            LineRenderer line = c.LineObject.GetComponent<LineRenderer>();
            line.SetPositions(new []{c.ConnectionFrom.transform.position, c.ConnectionTo.transform.position});

            if (c.IsConnected())
            {
                line.startColor = gameManager.lineColorConnected;
                c.ObjectToCheck.GetComponent<SpriteRenderer>().sprite = gameManager.progressbarCompleted;
                validConnections++;
                c.PlayConnectionSound();
            }
            else
            {
                line.startColor = gameManager.lineColorDisconnected;
                c.ObjectToCheck.GetComponent<SpriteRenderer>().sprite = gameManager.progressbarUncompleted;
            }

            line.endColor = line.startColor;
        }

        if (validConnections == Connections.Count)
        {
            gameManager.CheckWin();
        }
    }
}
