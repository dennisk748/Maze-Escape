using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider Masterslider;
    public Slider Musicslider;
    public Slider Sfxslider;

   

    private void Start()
    {
        Masterslider.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
        Musicslider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        Sfxslider.value = PlayerPrefs.GetFloat("SfxVolume", 0.75f);
    }
    public void setMasterLevel(float MastersliderValue)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(MastersliderValue) * 20);
        PlayerPrefs.SetFloat("MasterVolume", MastersliderValue);
    }
    public void setMusicLevel(float MusicsliderValue)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(MusicsliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", MusicsliderValue);
    }
    public void setSfxLevel(float SfxsliderValue)
    {
        mixer.SetFloat("SfxVolume", Mathf.Log10(SfxsliderValue) * 20);
        PlayerPrefs.SetFloat("SfxVolume", SfxsliderValue);
    }
}
