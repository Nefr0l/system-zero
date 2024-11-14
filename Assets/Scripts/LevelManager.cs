using System;
using System.Collections.Generic;
using System.Linq;
using Classes;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private List<ModuleConnection> ModuleConnections;
    private GameManager manager;
    private GameObject buttonGameObject;
    private Button winButton;

    private void Start()
    {
        ModuleConnections = GetComponents<ModuleConnection>().ToList();
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        buttonGameObject = GameObject.FindGameObjectWithTag("Finish");
        winButton = buttonGameObject.GetComponent<Button>();
        buttonGameObject.SetActive(false);
    }   

    public bool CheckWin()
    {
        return ModuleConnections.All(m => m.IsChecked()) && !manager.IsWin;
    }
    
    public void Win()
    {
        buttonGameObject.SetActive(true);
    }
}