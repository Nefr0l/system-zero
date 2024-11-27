using System.Collections.Generic;
using System.Linq;
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
            c.LineObject.SetActive(false);
            
            LineRenderer l = c.LineObject.AddComponent<LineRenderer>();
            
            l.SetPositions(new []{ c.ConnectionFrom.transform.position , c.ConnectionTo.transform.position});
            l.startColor = gameManager.lineColorDisconnected;
            l.endColor = l.startColor;
            l.material = new Material(Shader.Find("Sprites/Default"));
            l.startWidth = 0.1f;
            l.endWidth = l.startWidth;
        }
    }

    public void ShowLines()
    {
        foreach (var c in Connections)
        {
            c.LineObject.SetActive(true);
            c.StateChanged = true;
        }
    }
    
    public void HideLines()
    {
        foreach (var c in Connections)
        {
            c.LineObject.SetActive(false);
            c.StateChanged = false;
        }
    }
    
    // Function called every frame module is being dragged
    public void UpdateLines(GameObject moduleToCheck) 
    {
        foreach (var c in Connections.Where(e => e.ConnectionTo == moduleToCheck || e.ConnectionFrom == moduleToCheck))
        {
            LineRenderer line = c.LineObject.GetComponent<LineRenderer>();
            line.SetPositions(new [] {c.ConnectionFrom.transform.position, c.ConnectionTo.transform.position} );
            
            if (c.IsConnected() && c.StateChanged)
            {
                c.PlayConnectionSound();
                c.StateChanged = false;
            }
            
            line.startColor = (c.IsConnected()) ? gameManager.lineColorConnected : gameManager.lineColorDisconnected;
            line.endColor = line.startColor;
        }

        if (gameManager.WinCheck()) gameManager.Win();
    }
}
