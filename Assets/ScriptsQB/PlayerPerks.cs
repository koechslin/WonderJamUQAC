using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPerks : MonoBehaviour
{
    [SerializeField] private Player m_player;

    [SerializeField] private float m_speedUp;
    [SerializeField] private float m_speedDown;
    [SerializeField] private float m_regenFuelChange;


    public bool m_asteroidDeviation = false;
    public bool m_inverseController = false;

    public void IncreaseMaxHP()
    {
        m_player.maxHP++;
    }
    public void DecreaseMaxHP()
    {
        if (m_player.maxHP > 1)
        {
            m_player.maxHP--;
        }
    }

    public void IncreaseSpeed()
    {
        m_player.movementSpeed *= m_speedUp;
    }
    public void DecreaseSpeed()
    {
        m_player.movementSpeed *= m_speedDown;
    }

    public void IncreaseRegenFuel()
    {
        m_player.regularFuelReplenishment += m_regenFuelChange;
    }
    public void DecreaseRegenFuel()
    {
        m_player.regularFuelReplenishment -= m_regenFuelChange;
    }
}
