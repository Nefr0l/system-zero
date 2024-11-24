using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public List<GameObject> panels;
    
    private void Start()
    {
        HidePanels();
        InitializePanels();
        ShowPanel(panels[0]);
    }

    public void LoadLevel(int levelNumber)
    {
        PlayerPrefs.SetInt("level", levelNumber);
        SceneManager.LoadScene("Game");
    }

    public void ShowPanel(GameObject panel)
    {
        HidePanels();
        panel.SetActive(true);
    }

    private void InitializePanels()
    {
        Vector2 pos = GameObject.Find("Canvas").transform.position;
        
        foreach (var p in panels)
            p.transform.position = pos;
    }

    private void HidePanels()
    {
        foreach (var p in panels)
            p.SetActive(false);
    }
}
