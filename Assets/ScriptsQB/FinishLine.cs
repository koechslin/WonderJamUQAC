using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FinishLine : MonoBehaviour
{
    [SerializeField]
    private int maxNbOfDisplay;
    [SerializeField]
    private float delayBetweenEachDisplay;
    [SerializeField]
    private Text finishText;
    [SerializeField]
    private GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("P1"))
        {
            GameObject.Find("ShipP1").GetComponent<Player>().StopPlayer();
            GameObject.Find("ShipP2").GetComponent<Player>().StopPlayer();

            StartCoroutine(FinishTextCoroutine(1));
        }
        else if (col.CompareTag("P2"))
        {
            GameObject.Find("ShipP1").GetComponent<Player>().StopPlayer();
            GameObject.Find("ShipP2").GetComponent<Player>().StopPlayer();

            StartCoroutine(FinishTextCoroutine(2));
        }
    }

    private IEnumerator FinishTextCoroutine(int winnerNum)
    {
        string winner = winnerNum == 1 ? "Player 1" : "Player 2";

        int currentNbOfDisplay = 0;
        finishText.text = winner + " wins the round !";

        while(currentNbOfDisplay < maxNbOfDisplay)
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
