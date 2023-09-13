using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject BrickPrefab;
    public Vector2 Spacing;
    public Vector2 Offset;

    public TMP_Text ScoreText;
    public TMP_Text EndGameText;
    public int Score;

    public PlayerController PlayerControllerInstance;
    public BallController BallControllerInstance;
    
    public void Start()
    {
        SetupTheBricks();
        EndGameText.gameObject.SetActive(false);
    }
    public void SetupTheBricks()
    {
        for (int j = 0; j < 4; j++)
        {
            for (int i = 0; i < 10; i++)
            {
                Vector2 BrickPos = new Vector2();
                BrickPos.x = (i * Spacing.x) - Offset.x;
                BrickPos.y = (j * Spacing.y) + Offset.y;
                Instantiate(BrickPrefab, BrickPos, Quaternion.identity);
            }
        }
    }

    public void UpdateScore()
    {
        Score += 100;
        ScoreText.text = "Score: " + Score;
        if (Score >= 4000)
        {
            EndGameText.text = "You Win";
            EndGameText.gameObject.SetActive(true);
            BallControllerInstance.gameObject.SetActive(false);
        }
    }

    public void LoseGame()
    {
        EndGameText.text = "You Lose";
        EndGameText.gameObject.SetActive(true);
        BallControllerInstance.gameObject.SetActive(false);
        PlayerControllerInstance.CanReceiveGameInputs = false;
    }
}
