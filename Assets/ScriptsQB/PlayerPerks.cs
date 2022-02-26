using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPerks : MonoBehaviour
{
    [SerializeField] private Player m_player;

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
}
