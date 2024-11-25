using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public List<GameObject> panels;
    
    public Slider fxSlider;
    public Slider musicSlider;
    public AudioMixer fxMixer;
    public AudioMixer musicMixer;
    
    private void Start()
    {
        HidePanels();
        InitializePanels();
        ShowPanel(panels[0]);
        
        float fxVolume = (PlayerPrefs.HasKey("fxVolume")) ? PlayerPrefs.GetFloat("fxVolume") : 0.75f;
        fxSlider.value = fxVolume;
        fxMixer.SetFloat("Volume", (float)Math.Log10(fxVolume) * 20);

        float musicVolume = (PlayerPrefs.HasKey("musicVolume")) ? PlayerPrefs.GetFloat("musicVolume") : 0.75f;
        musicSlider.value = musicVolume;
        musicMixer.SetFloat("Volume", (float)Math.Log10(musicVolume) * 20);
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
