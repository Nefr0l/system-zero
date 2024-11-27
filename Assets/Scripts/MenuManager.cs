using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public List<GameObject> panels;
    public List<Button> levelButtons;
    private static int HighestLevel;
    
    public Slider fxSlider;
    public Slider musicSlider;
    public AudioMixer fxMixer;
    public AudioMixer musicMixer;
    
    private void Start()
    {
        HidePanels();
        InitializePanels();
        ShowPanel(panels[0]);

        HighestLevel = PlayerPrefs.HasKey("highestLevel") ? PlayerPrefs.GetInt("highestLevel") : 1;
        for (int i = 0; i < levelButtons.Count; i++)
        {
            if (i > HighestLevel) levelButtons[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "x";
        }
        
        float fxVolume = PlayerPrefs.HasKey("fxVolume") ? PlayerPrefs.GetFloat("fxVolume") : 0.75f;
        fxSlider.value = fxVolume;
        fxMixer.SetFloat("Volume", (float)Math.Log10(fxVolume) * 20);

        float musicVolume = PlayerPrefs.HasKey("musicVolume") ? PlayerPrefs.GetFloat("musicVolume") : 0.75f;
        musicSlider.value = musicVolume;
        musicMixer.SetFloat("Volume", (float)Math.Log10(musicVolume) * 20);
    }

    public void LoadLevel(int levelNumber)
    {
        if (levelNumber - 1 > HighestLevel) return;
        PlayerPrefs.SetInt("level", levelNumber);
        SceneManager.LoadScene("Game");
    }

    public void ShowPanel(GameObject panel)
    {
        HidePanels();
        panel.SetActive(true);
    }

    public void ChangeFxVolume()
    {
        PlayerPrefs.SetFloat("fxVolume", fxSlider.value);
        fxMixer.SetFloat("Volume", (float)Math.Log10(fxSlider.value) * 20);
    }
    
    public void ChangeMusicVolume()
    {
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
        musicMixer.SetFloat("Volume", (float)Math.Log10(musicSlider.value) * 20);
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
