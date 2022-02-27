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

    public float speedP1Value;
    public TextMeshProUGUI speedP1;
    public float speedP2Value;
    public TextMeshProUGUI speedP2;
    public float handlingP1Value;
    public TextMeshProUGUI handlingP1;
    public float handlingP2Value;
    public TextMeshProUGUI handlingP2;

    public int shipModelP1;
    public int shipModelP2;

    private void Start()
    {
        speedP1Value = speedP2Value = 2f;
        handlingP1Value = handlingP2Value = 4f;
        shipModelP1 = shipModelP2 = 0;
    }

    public void ChangeShipOccuring(int playerNumber)
    {
        if (playerNumber == 0)
        {
            if (pictureShip1P1.activeSelf)
            {
                pictureShip1P1.SetActive(false);
                pictureShip2P1.SetActive(true);
                speedP1Value = 2.5f;
                handlingP1Value = 3f;
                speedP1.text = "Speed = " + speedP1Value;
                handlingP1.text = "Handling = " + handlingP1Value;
                shipModelP1 = 1;
            }
            else
            {
                pictureShip1P1.SetActive(true);
                pictureShip2P1.SetActive(false);
                speedP1Value = 2f;
                handlingP1Value = 4f;
                speedP1.text = "Speed = " + speedP1Value;
                handlingP1.text = "Handling = " + handlingP1Value;
                shipModelP1 = 0;
            }
        }
        if (playerNumber == 1)
        {
            if (pictureShip1P2.activeSelf)
            {
                pictureShip1P2.SetActive(false);
                pictureShip2P2.SetActive(true);
                speedP2Value = 2.5f;
                handlingP2Value = 3f;
                speedP2.text = "Speed = " + speedP2Value;
                handlingP2.text = "Handling = " + handlingP2Value;
                shipModelP2 = 1;
            }
            else
            {
                pictureShip1P2.SetActive(true);
                pictureShip2P2.SetActive(false);
                speedP2Value = 2f;
                handlingP2Value = 4f;
                speedP2.text = "Speed = " + speedP2Value;
                handlingP2.text = "Handling = " + handlingP2Value;
                shipModelP2 = 0;
            }
        }
    }
}
