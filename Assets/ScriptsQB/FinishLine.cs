using UnityEngine;
using System.Collections.Generic;

public class FinishLine : MonoBehaviour
{
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
            Debug.Log("P1 wins !");
            Time.timeScale = 0f;
        }
        else if (col.CompareTag("P2"))
        {
            Debug.Log("P2 wins !");
            Time.timeScale = 0f;
        }
    }

    public void Respawn(string playerTag)
    {
        if (playerTag == "P1")
        {
            m_P1finishChoice = true;
        }
        else if (playerTag == "P2")
        {
            m_P2finishChoice = true;
        }

        if (m_P1finishChoice && m_P2finishChoice)
        {
            Time.timeScale = 1f;
            m_perksMenuP1.SetActive(false);
            m_perksMenuP2.SetActive(false);
            m_player1.transform.position = m_posP1;
            m_player2.transform.position = m_posP2;
            m_player1.GetComponent<Player>().Respawn();
            m_player2.GetComponent<Player>().Respawn();
        }
    }
}
