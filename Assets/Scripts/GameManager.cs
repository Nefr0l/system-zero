using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> Levels;
    private int CurrentLevel;
    private int HighestLevel;
    public static bool IsWin;
    
    public float topBorder;
    public float downBorder;
    public float leftBorder;
    public float rightBorder;
    public float borderOffset;
    
    public Color lineColorConnected;
    public Color lineColorDisconnected;
    
    private GameObject NextLevelButton;
    private static AudioSource Source;
    private bool canPlayWinSound = true;
    public AudioClip ModuleConnectedSound;
    public AudioClip WinSound;
    
    private void Start()
    {
        IsWin = false;
        Source = GetComponent<AudioSource>();
        CurrentLevel = PlayerPrefs.HasKey("level") ? PlayerPrefs.GetInt("level") : 1;

        foreach (var l in Levels) l.SetActive(false);
        Levels[CurrentLevel - 1].SetActive(true);
        
        NextLevelButton = GameObject.FindGameObjectWithTag("Finish");
        NextLevelButton.SetActive(false);
    }

    public bool WinCheck()
    {
        GameObject[] checkboxes = Levels[CurrentLevel-1].transform
            .Cast<Transform>()
            .Where(t => t.gameObject.CompareTag("Checkbox"))
            .Select(t => t.gameObject)
            .ToArray();
        
        var checks = checkboxes[0].GetComponent<ConnectionManager>().Connections;
        return checks.Count(e => e.IsConnected()) == checks.Count();
    }

    public static void PlaySound(AudioClip sound)
    {
        Source.PlayOneShot(sound);
    }

    public void NextLevelButtonClick()
    {
        PlayerPrefs.SetInt("level", CurrentLevel + 1);
        PlayerPrefs.SetInt("highestLevel", CurrentLevel > HighestLevel ? CurrentLevel : HighestLevel);
        PlayerPrefs.Save();

        SceneManager.LoadScene(CurrentLevel == Levels.Count ? "Menu" : "Game");
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Win()
    {
        if (!canPlayWinSound) return;
        
        PlaySound(WinSound);
        IsWin = true;
        NextLevelButton.SetActive(true);
        canPlayWinSound = false;
    }
}
