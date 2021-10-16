using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringSystem : MonoBehaviour
{


    public GameObject scoreText;
    public GameObject heartText;
    public static int points;
    public static int lives;
    private void Start()
    {
        lives = GameManager.Instance.lives;
    }

    void Update()
    {
        scoreText.GetComponentInChildren<Text>().text = points.ToString();
        heartText.GetComponentInChildren<Text>().text = lives.ToString();
    }
}
