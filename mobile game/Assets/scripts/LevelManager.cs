using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private const float TIME_BEFORE_PLAY = 2.0f;
    private static LevelManager instance;
    public static LevelManager Instance {get {return instance;}}

    public Transform respawnPoint;
    private GameObject player;
    public GameObject congratsPopup;
    public GameObject failurePopup;
    public GameObject[] redHearts;

    public GameObject NoLivesPopUp;
    public GameObject pauseMenu;

    public Text timer;
    public Text endTimeText;
    public Text heartcounter;
    private float delayTime = 1f;

    private float startTime;
    public float silverTime;
    private float levelDuration;
    public float goldTime;

    public void Start()
    {
        failurePopup.SetActive(false);
        congratsPopup.SetActive (false);
        instance = this;
        pauseMenu.SetActive(false);
        NoLivesPopUp.SetActive(false);
        startTime = Time.time;
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = respawnPoint.position;
    }
    private void Update()
    {
        heartcounter.text = GameManager.Instance.lives.ToString();
        GameManager.Instance.save();
        if (player != null)
        {
            if (player.transform.position.y < -10.0f)
                Death();

            if (Time.time - startTime < TIME_BEFORE_PLAY)
                return;

            levelDuration = Time.time - (startTime + TIME_BEFORE_PLAY);
            string minutes = ((int)levelDuration / 60).ToString("00");
            string seconds = (levelDuration % 60).ToString("00.00");
            timer.text = minutes + ":" + seconds;
        }
    }

    public void togglePauseMenuPopup()
    {
        
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        Time.timeScale = (pauseMenu.activeSelf) ? 0 : 1;
    }
    public void ToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main menu");
    }
    public void RestartLevel()
    {
        if (GameManager.Instance.lives != 0)
        {
            GameManager.Instance.lives -= 1;
            GameManager.Instance.save();
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            OpenNoLives();
        }
    }
    public void OpenNoLives()
    {
        NoLivesPopUp.SetActive(!NoLivesPopUp.activeSelf);
        Time.timeScale = (NoLivesPopUp.activeSelf) ? 0 : 1;
        for (int i = 0; i < redHearts.Length; i++)
        {
            if(GameManager.Instance.lives == 0)
            {
                redHearts[i].SetActive(false);
            }
            else if (GameManager.Instance.lives == 1)
            {
                redHearts[2].SetActive(false);
                redHearts[1].SetActive(false);
            }
            else if (GameManager.Instance.lives == 2)
            {
                redHearts[0].SetActive(false);
            }
            else if (GameManager.Instance.lives == 3)
            {
                redHearts[i].SetActive(true);
            }
        }
    }
    public void Victory()
    {
        foreach (Transform t in congratsPopup.transform.parent)
        {
            t.gameObject.SetActive (false);
        }
        congratsPopup.SetActive (true);


        Rigidbody rigid = player.GetComponent<Rigidbody> ();
        rigid.constraints = RigidbodyConstraints.FreezePosition;

        levelDuration = Time.time - (startTime + TIME_BEFORE_PLAY);
        string minutes = ((int) levelDuration / 60 ).ToString("00");
        string seconds = (levelDuration % 60).ToString("00.00");
        endTimeText.text = minutes + ":" + seconds; 

        if (levelDuration < goldTime)
        {
            GameManager.Instance.currency += 1000;
            endTimeText.color = Color.yellow;
        }
        else if (levelDuration < silverTime)
        {
            GameManager.Instance.currency += 25;
            endTimeText.color = Color.gray;
        }
        else
        {
            GameManager.Instance.currency += 10;
            endTimeText.color = Color.gray;
        }
        GameManager.Instance.save();

        string saveString = "";
        LevelData level = new LevelData(SceneManager.GetActiveScene().name);
        saveString += (level.BestTime > levelDuration || level.BestTime == 0.0f) ? levelDuration.ToString () : level.BestTime.ToString();
        saveString += '&';
        saveString += silverTime.ToString ();
        saveString += '&';       
        saveString += goldTime.ToString ();
        PlayerPrefs.SetString (SceneManager.GetActiveScene ().name,saveString);
        
    }
    public void Death()
    {

        //player.transform.position = respawnPoint.position;
        //Rigidbody rigid = player.GetComponent<Rigidbody> ();
        //rigid.velocity = Vector3.zero;
        //rigid.angularVelocity = Vector3.zero;
        //RestartLevel();
        Invoke("showFailurePopup", delayTime);
        
        //togglePauseMenuPopup();
    }
    public void showFailurePopup()
    {
            foreach (Transform t in failurePopup.transform.parent)
            {
                t.gameObject.SetActive(false);
            }
            failurePopup.SetActive(true);
        if (player != null)
        {
            Rigidbody rigid = player.GetComponent<Rigidbody>();
            rigid.constraints = RigidbodyConstraints.FreezePosition;
        }

    }
}
