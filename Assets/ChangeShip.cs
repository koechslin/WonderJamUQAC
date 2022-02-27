using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeShip : MonoBehaviour
{
    public GameObject pictureShip1P1;
    public GameObject pictureShip2P1;
    public GameObject pictureShip1P2;
    public GameObject pictureShip2P2;

    public TextMeshProUGUI speedP1;
    public TextMeshProUGUI speedP2;
    public TextMeshProUGUI handlingP1;
    public TextMeshProUGUI handlingP2;

    public void ChangeShipOccuring(int playerNumber)
    {
        if (playerNumber == 0)
        {
            if (pictureShip1P1.activeSelf)
            {
                pictureShip1P1.SetActive(false);
                pictureShip2P1.SetActive(true);
                speedP1.text = "Speed = 2.5";
                handlingP1.text = "Handling = 3";
            }
            else
            {
                pictureShip1P1.SetActive(true);
                pictureShip2P1.SetActive(false);
                speedP1.text = "Speed = 2";
                handlingP1.text = "Handling = 4";
            }
        }
        if (playerNumber == 1)
        {
            if (pictureShip1P2.activeSelf)
            {
                pictureShip1P2.SetActive(false);
                pictureShip2P2.SetActive(true);
                speedP2.text = "Speed = 2.5";
                handlingP2.text = "Handling = 3";
            }
            else
            {
                pictureShip1P2.SetActive(true);
                pictureShip2P2.SetActive(false);
                speedP2.text = "Speed = 2";
                handlingP2.text = "Handling = 4";
            }
        }
    }
}
