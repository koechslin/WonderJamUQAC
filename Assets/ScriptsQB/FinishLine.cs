using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishLine : MonoBehaviour
{
    [Header("Finish UI")]
    [SerializeField]
    private int maxNbOfDisplay;
    [SerializeField]
    private float delayBetweenEachDisplay;
    [SerializeField]
    private Text finishText;

    [Header("Game Manager")]
    [SerializeField]
    private GameManager gameManager;

    private GameObject m_player1;
    private GameObject m_player2;

    private void Start()
    {
        m_player1 = GameObject.FindGameObjectWithTag("P1");
        m_player2 = GameObject.FindGameObjectWithTag("P2");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("P1"))
        {
            m_player1.GetComponent<Player>().StopPlayer();
            m_player2.GetComponent<Player>().StopPlayer();

            StartCoroutine(FinishTextCoroutine(1));
        }
        else if (col.CompareTag("P2"))
        {
            m_player1.GetComponent<Player>().StopPlayer();
            m_player2.GetComponent<Player>().StopPlayer();

            StartCoroutine(FinishTextCoroutine(2));
        }
    }

    private IEnumerator FinishTextCoroutine(int winnerNum)
    {
        string winner = winnerNum == 1 ? "Player 1" : "Player 2";

        int currentNbOfDisplay = 0;
        finishText.text = winner + " wins the round !";

        while (currentNbOfDisplay < maxNbOfDisplay)
        {
            finishText.gameObject.SetActive(true);
            yield return new WaitForSeconds(delayBetweenEachDisplay);
            finishText.gameObject.SetActive(false);
            yield return new WaitForSeconds(delayBetweenEachDisplay);
            currentNbOfDisplay++;
        }

        gameManager.FinishRound(winnerNum);
    }
}
