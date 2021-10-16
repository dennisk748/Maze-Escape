using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
/* SUBSCRIBING TO MY YOUTUBE CHANNEL: 'VIN CODES' WILL HELP WITH MORE VIDEOS AND CODE SHARING IN THE FUTURE :) THANK YOU */

public class MoveToNextLevel : MonoBehaviour
{
    public int nextSceneLoad;
    //public GameObject completiontext;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        Advertisement.Initialize("3830559", true);
        while (!Advertisement.IsReady())
            yield return null;
        //completiontext.SetActive(false);
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Advertisement.Show();
            if(SceneManager.GetActiveScene().buildIndex == 12) /* < Change this int value to whatever your
                                                                   last level build index is on your
                                                                   build settings */
            {
                Debug.Log("You Completed ALL Levels");
                //completiontext.SetActive(true);

                //Show Win Screen or Somethin.
            }
            else
            {
                //Move to next level
                StartCoroutine("LoadNextScene");

                //Setting Int for Index
                if (nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
                {
                    PlayerPrefs.SetInt("levelAt", nextSceneLoad);
                }
            }
        }
    }

    IEnumerator LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneLoad);
        yield return new WaitForSeconds(15);
    }
}
