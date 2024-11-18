using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> Levels;
    private int CurrentLevel;
    public bool IsWin;
    
    public float topBorder;
    public float downBorder;
    public float leftBorder;
    public float rightBorder;
    public float borderOffset;
    
    public Color lineColorConnected;
    public Color lineColorDisconnected;
    public Material lineMaterial;
    
    public Sprite progressbarCompleted;
    public Sprite progressbarUncompleted;

    private GameObject NextLevelButton;

    private void Start()
    {
        CurrentLevel = (PlayerPrefs.HasKey("Level")) ? PlayerPrefs.GetInt("Level") : 1;

        foreach (var l in Levels)
        {
            l.SetActive(false);
        }
        Levels[CurrentLevel - 1].SetActive(true);
        
        NextLevelButton = GameObject.FindGameObjectWithTag("Finish");
        NextLevelButton.SetActive(false);
    }

    public void CheckWin()
    {
        GameObject[] checkboxes = gameObject.transform
            .Cast<Transform>()
            .Where(t => t.gameObject.CompareTag("Checkbox"))
            .Select(t => t.gameObject)
            .ToArray();

        foreach (var c in checkboxes)
        {
            if (c.GetComponent<SpriteRenderer>().sprite != progressbarCompleted)
            {
                break;
            }
        }
        
        Win();
    }

    public void NextLevelButtonClick()
    {
        if (CurrentLevel+1 == Levels.Count)
        {
            SceneManager.LoadScene("Menu");
        }
        
        PlayerPrefs.SetInt("Level", CurrentLevel + 1);
        PlayerPrefs.Save();
        
        SceneManager.LoadScene("Game");
        
        // Add handling for out of array of levels exception
    }

    private void Win()
    {
        Debug.Log("Win");
        IsWin = true;
        NextLevelButton.SetActive(true);
    }
}
