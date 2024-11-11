using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Level management
    public List<GameObject> Levels; // to be continued
    
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
}
