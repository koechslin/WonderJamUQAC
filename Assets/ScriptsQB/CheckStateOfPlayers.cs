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

    public Animator shipModel1;
    public Animator shipModel2;

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
            SendDataToPlayersSpaceships();
            SceneManager.LoadScene(race);
            FindObjectOfType<AudioManager>().Stop("Main Menu");
            FindObjectOfType<AudioManager>().Play("Space Race");
        }
    }

    public void resetButtons()
    {
        p1Ready = false;
        p2Ready = false;
        p1NotReadyButton.SetActive(!p1Ready);
        p1ReadyButton.SetActive(p1Ready);
        p2NotReadyButton.SetActive(!p2Ready);
        p2ReadyButton.SetActive(p2Ready);
    }

    private void SendDataToPlayersSpaceships()
    {
        ChangeShip changeShipScript = GetComponent<ChangeShip>();
        if (changeShipScript.shipModelP1 == 0)
        {
            PlayersSpaceships.animatorP1 = shipModel1;
        }
        else if (changeShipScript.shipModelP1 == 1)
        {
            PlayersSpaceships.animatorP1 = shipModel2;
            
        }
        PlayersSpaceships.healthP1 = 3;
        PlayersSpaceships.fuelP1 = 1000;
        PlayersSpaceships.speedP1 = changeShipScript.speedP1Value;
        PlayersSpaceships.handlingP1 = changeShipScript.handlingP1Value;

        if (changeShipScript.shipModelP2 == 0)
        {
            PlayersSpaceships.animatorP2 = shipModel1;
        }
        else if (changeShipScript.shipModelP2 == 1)
        {
            PlayersSpaceships.animatorP2 = shipModel2;

        }
        PlayersSpaceships.healthP2 = 3;
        PlayersSpaceships.fuelP2 = 1000;
        PlayersSpaceships.speedP2 = changeShipScript.speedP2Value;
        PlayersSpaceships.handlingP2 = changeShipScript.handlingP2Value;
    }
}
