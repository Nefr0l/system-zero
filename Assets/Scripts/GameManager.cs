using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Classes;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<GameObject> Modules = new();
    
    public float topBorder;
    public float downBorder;
    public float leftBorder;
    public float rightBorder;

    void Start()
    {
        Modules = GameObject.FindGameObjectsWithTag("Module").ToList();
    }
}
