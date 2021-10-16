using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneScript : MonoBehaviour
{
    public GameObject LoadingScreen;
    public GameObject menuMusic;
    public Slider slider;
    public Text percentageText;
   
  
    public void selectScene(int sceneIndex)
    {
        switch (sceneIndex)
        {
            case 1:
                StartCoroutine( LoadAsynchronously(1));                
              break;
            case 2:
                StartCoroutine( LoadAsynchronously(2));               
                break;
            case 3:
                StartCoroutine(LoadAsynchronously(3));
                break;
            case 4:
                StartCoroutine(LoadAsynchronously(4));
                break;
            case 5:
                StartCoroutine(LoadAsynchronously(5));
                break;
            case 6:
                StartCoroutine(LoadAsynchronously(6));
                break;
            case 7:
                StartCoroutine(LoadAsynchronously(7));
                break;
            case 8:
                StartCoroutine(LoadAsynchronously(8));
                break;
            case 9:
                StartCoroutine(LoadAsynchronously(9));
                break;
            case 10:
                StartCoroutine(LoadAsynchronously(10));
                break;
            case 11:
                StartCoroutine(LoadAsynchronously(11));
                break;
            case 12:
                StartCoroutine(LoadAsynchronously(12));
                break;
            case 13:
                StartCoroutine(LoadAsynchronously(13));
                break;
        }
    }
    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        LoadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
            percentageText.text = (int)progress * (int)100f + "%";

            yield return null;
        }
    }
}
