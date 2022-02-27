using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPerks : MonoBehaviour
{
    [SerializeField] private GeneratePerk m_gp1;
    [SerializeField] private GeneratePerk m_gp2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_gp1.m_bonusText == m_gp2.m_bonusText && m_gp1.m_malusText == m_gp2.m_malusText)
        {
            m_gp1.Setup();
            m_gp2.Setup();
        }
    }
}
