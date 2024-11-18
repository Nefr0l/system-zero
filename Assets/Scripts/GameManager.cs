using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Level management
    public List<GameObject> Levels; // to be continued
    public bool IsWin;
    
    // Drag and drop boundaries values
    public float topBorder;
    public float downBorder;
    public float leftBorder;
    public float rightBorder;
    public float borderOffset;

    // Values for connections between lines
    public Color lineColorConnected;
    public Color lineColorDisconnected;
    public Material lineMaterial;
    
    // Other values
    public Sprite progressbarCompleted;
    public Sprite progressbarUncompleted;

    public void CheckWin()
    {
        GameObject[] checkboxes = gameObject.transform
            .Cast<Transform>()
            .Where(t => t.gameObject.CompareTag("Checkbox"))
            .Select(t => t.gameObject)
            .ToArray();

        bool isWin = true;
        foreach (var c in checkboxes)
        {
            if (c.GetComponent<SpriteRenderer>().sprite != progressbarCompleted)
            {
                isWin = false;
            }
        }

        if (isWin)
        {
            IsWin = true;
            Win();
        }
    }

    private void Win()
    {
        Debug.Log("Win");
    }
}
