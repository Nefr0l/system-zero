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
    
    public Sprite progressbarCompleted;
    public Sprite progressbarUncompleted;
    private GameObject NextLevelButton;

    private static AudioSource Source;
    public AudioClip ModuleConnectedSound;
    public AudioClip WinSound;

    private bool canPlayWinSound = true;

    private void Start()
    {
        Source = GetComponent<AudioSource>();
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
        GameObject[] checkboxes = Levels[CurrentLevel-1].transform
            .Cast<Transform>()
            .Where(t => t.gameObject.CompareTag("Checkbox"))
            .Select(t => t.gameObject)
            .ToArray();

        bool isWin = true;
        foreach (var c in checkboxes)
        {
            if (c.GetComponent<SpriteRenderer>().sprite == progressbarUncompleted) isWin = false;
        }
        
        if (isWin) Win();
    } 

    public static void PlaySound(AudioClip sound)
    {
        Source.PlayOneShot(sound);
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

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void Win()
    {
        IsWin = true;
        NextLevelButton.SetActive(true);
        if (canPlayWinSound)
        {
            PlaySound(WinSound);
        }

        canPlayWinSound = false;

    }
}
