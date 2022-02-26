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

    [Header("Perks")]
    [SerializeField] private GeneratePerk m_perks1P1;
    [SerializeField] private GeneratePerk m_perks1P2;
    [SerializeField] private GeneratePerk m_perks2P1;
    [SerializeField] private GeneratePerk m_perks2P2;

    [SerializeField] private GameObject m_perksMenuP1;
    [SerializeField] private GameObject m_perksMenuP2;

    private List<GeneratePerk> m_perks = new List<GeneratePerk>();
    private GameObject m_player1;
    private GameObject m_player2;
    private Vector3 m_posP1;
    private Vector3 m_posP2;

    private bool m_P1finishChoice = false;
    private bool m_P2finishChoice = false;

    private void Start()
    {
        m_player1 = GameObject.FindGameObjectWithTag("P1");
        m_player2 = GameObject.FindGameObjectWithTag("P2");
        m_posP1 = m_player1.transform.position;
        m_posP2 = m_player2.transform.position;

        m_perks.Add(m_perks1P1);
        m_perks.Add(m_perks1P2);
        m_perks.Add(m_perks2P1);
        m_perks.Add(m_perks2P2);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        m_perksMenuP1.SetActive(true);
        m_perksMenuP2.SetActive(true);

        foreach (GeneratePerk gp in m_perks)
        {
            gp.Setup();
            Debug.Log("perk");
        }

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

    public void Respawn(string playerTag)
    {
        switch (playerTag)
        {
            case "P1":
                m_P1finishChoice = true;
                break;
            case "P2":
                m_P2finishChoice = true;
                break;
        }

        if (!m_P1finishChoice || !m_P2finishChoice) return;

        Time.timeScale = 1f;
        m_perksMenuP1.SetActive(false);
        m_perksMenuP2.SetActive(false);
        m_player1.transform.position = m_posP1;
        m_player2.transform.position = m_posP2;
        m_player1.GetComponent<Player>().Respawn();
        m_player2.GetComponent<Player>().Respawn();
    }
}
