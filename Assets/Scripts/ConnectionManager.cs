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
            c.LineObject.transform.SetParent(transform); 
            c.LineObject.AddComponent<LineRenderer>();

            LineRenderer line = c.LineObject.GetComponent<LineRenderer>();
            line.SetPosition(0, transform.position);
            line.SetPosition(1, c.ConnectionTo.transform.position);
            line.startColor = gameManager.lineColorDisconnected;
            line.endColor = line.endColor;
            line.material = new Material(Shader.Find("Sprites/Default"));
            line.startWidth = 0.1f;
            line.endWidth = line.startWidth;
            
            c.LineObject.SetActive(true);
        }
        
        HideLines();
    }

    public void ShowLines()
    {
        foreach (var c in Connections)
        {
            LineRenderer line = c.LineObject.GetComponent<LineRenderer>();
            line.enabled = true;
            c.IsEncounter = true;
        }
    }
    
    public void HideLines()
    {
        foreach (var c in Connections)
        {
            LineRenderer line = c.LineObject.GetComponent<LineRenderer>();
            line.enabled = false;
            c.IsEncounter = false;
        }
    }
    
    public void UpdateLines()
    {
        int validConnections = 0;
        
        foreach (var c in Connections)
        {
            LineRenderer line = c.LineObject.GetComponent<LineRenderer>();
            line.SetPositions(new [] {c.ConnectionFrom.transform.position, c.ConnectionTo.transform.position} );

            if (c.IsEncounter && line.startColor == gameManager.lineColorConnected)
            {
                c.PlayConnectionSound();
                c.IsEncounter = false;
            }
            
            if (c.IsConnected())
            {
                validConnections++;
                line.startColor = gameManager.lineColorConnected;

                var cons = Connections.Where(e => e.ObjectToCheck == c.ObjectToCheck);

                bool check = true;
                foreach (var con in cons)
                    if (con.IsConnected() == false) check = false;
                
                c.ObjectToCheck.GetComponent<SpriteRenderer>().sprite = check ? 
                    gameManager.progressbarCompleted : gameManager.progressbarUncompleted;
            }
            else
            {
                line.startColor = gameManager.lineColorDisconnected;
                c.ObjectToCheck.GetComponent<SpriteRenderer>().sprite = gameManager.progressbarUncompleted;
            }

            line.endColor = line.startColor;
        }

        if (validConnections == Connections.Count)
            gameManager.CheckWin();
    }
}
