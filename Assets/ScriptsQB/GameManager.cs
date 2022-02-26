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

    private int scoreP1;
    private int scoreP2;

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

        CheckMaxScore();
    }

    private void CheckMaxScore()
    {
        if (scoreP1 == nbRoundsToWin)
        {
            finishTextP1.text = "You win the race !";
            finishTextP1.gameObject.SetActive(true);

            finishTextP2.text = "You lose the race !";
            finishTextP2.gameObject.SetActive(true);
        }

        else if (scoreP2 == nbRoundsToWin)
        {
            finishTextP2.text = "You win the race !";
            finishTextP2.gameObject.SetActive(true);

            finishTextP1.text = "You lose the race !";
            finishTextP1.gameObject.SetActive(true);
        }
    }
}
