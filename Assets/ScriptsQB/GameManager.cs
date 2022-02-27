using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int nbRoundsToWin;

    [Header("Player 1")]
    [SerializeField]
    private Player player1;
    [SerializeField]
    private Image[] imagesScoreP1;
    [SerializeField]
    private Text finishTextP1;

    [Header("Player 2")]
    [SerializeField]
    private Player player2;
    [SerializeField]
    private Image[] imagesScoreP2;
    [SerializeField]
    private Text finishTextP2;

    [Header("Perks")]
    [SerializeField] private GameObject m_perksMenuP1;
    [SerializeField] private GameObject m_perksMenuP2;
    [SerializeField] private List<GeneratePerk> m_perks;

    public GameObject endGameMenu;

    private int scoreP1;
    private int scoreP2;
    private Vector3 startPosP1;
    private Vector3 startPosP2;
    private bool p1finishChoice = false;
    private bool p2finishChoice = false;

    private void Start()
    {
        if (player1 == null && player2 == null) return;

        startPosP1 = player1.transform.position;
        startPosP2 = player2.transform.position;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void FinishRound(int winnerNum)
    {
        if (winnerNum == 1)
        {
            scoreP1++;
        }
        else
        {
            scoreP2++;
        }

        for (int i = 0; i < nbRoundsToWin; ++i)
        {
            imagesScoreP1[i].color = i < scoreP1 ? player1.defaultColor : new Color(0f, 0f, 0f, 0f);
        }

        for (int i = 0; i < nbRoundsToWin; ++i)
        {
            imagesScoreP2[i].color = i < scoreP2 ? player2.defaultColor : new Color(0f, 0f, 0f, 0f);
        }

        bool raceFinished = CheckMaxScore();

        if (raceFinished)
        {
            DisplayFinishRaceText();
        }
        else
        {
            DisplayPerksMenu();
        }
    }

    private bool CheckMaxScore()
    {
        return scoreP1 == nbRoundsToWin || scoreP2 == nbRoundsToWin;
    }

    private void DisplayPerksMenu()
    {
        m_perksMenuP1.SetActive(true);
        m_perksMenuP2.SetActive(true);

        foreach (GeneratePerk gp in m_perks)
        {
            gp.Setup();
        }
    }

    private void DisplayFinishRaceText()
    {
        if (scoreP1 == nbRoundsToWin)
        {
            finishTextP1.text = "You win the race !";
            finishTextP1.gameObject.SetActive(true);

            finishTextP2.text = "You lose the race !";
            finishTextP2.gameObject.SetActive(true);
        }

        if (scoreP2 == nbRoundsToWin)
        {
            finishTextP2.text = "You win the race !";
            finishTextP2.gameObject.SetActive(true);

            finishTextP1.text = "You lose the race !";
            finishTextP1.gameObject.SetActive(true);
        }

        StartCoroutine(EndGameScreenCoroutine());
    }

    public void CheckPerksChoices(string playerTag)
    {
        switch (playerTag)
        {
            case "P1":
                p1finishChoice = true;
                break;
            case "P2":
                p2finishChoice = true;
                break;
        }

        if (!p1finishChoice || !p2finishChoice) return;

        m_perksMenuP1.SetActive(false);
        m_perksMenuP2.SetActive(false);

        player1.lastCheckpoint = startPosP1;
        player2.lastCheckpoint = startPosP2;

        player1.Respawn(startPosP1);
        player2.Respawn(startPosP2);
    }

    private IEnumerator EndGameScreenCoroutine()
    {
        yield return new WaitForSeconds(3);
        endGameMenu.SetActive(true);
    }
}
