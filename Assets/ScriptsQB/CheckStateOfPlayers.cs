using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckStateOfPlayers : MonoBehaviour
{

    [SerializeField] bool p1Ready;
    [SerializeField] bool p2Ready;
    private string race;
    public GameObject p1ReadyButton;
    public GameObject p2ReadyButton;
    public GameObject p1NotReadyButton;
    public GameObject p2NotReadyButton;

    // Start is called before the first frame update
    void Start()
    {
        p1Ready = false;
        p2Ready = false;
        race = "Space Race";
    }

    public void switchP1State()
    {
        p1Ready = !p1Ready;
        p1NotReadyButton.SetActive(!p1Ready);
        p1ReadyButton.SetActive(p1Ready);
    }

    public void switchP2State()
    {
        p2Ready = !p2Ready;
        p2NotReadyButton.SetActive(!p2Ready);
        p2ReadyButton.SetActive(p2Ready);
    }

    private void Update()
    {
        if (p1Ready && p2Ready)
        {
            SceneManager.LoadScene(race);
            FindObjectOfType<AudioManager>().Stop("Main Menu");
            FindObjectOfType<AudioManager>().Play("Space Race");
        }
    }
}
