using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneratePerk : MonoBehaviour
{
    [SerializeField] private Animator m_animator;
    
    private GameObject m_player;
    private PlayerPerks m_playerPerks;

    private bool m_increaseHP = false;
    [SerializeField] private string m_increaseHPDescription = "HP up";
    private bool m_asteroidsDeviation = false;
    [SerializeField] private string m_asteroidsDeviationDescription = "Asteroids Deviation";
    private bool m_increaseSpeed = false;
    [SerializeField] private string m_increaseSpeedDescription = "Speed up";
    private bool m_increaseRegenFuel = false;
    [SerializeField] private string m_increaseRegenFuelDescription = "Fuel Regen up";

    private bool m_decreaseHP = false;
    [SerializeField] private string m_decreaseHPDescription = "HP down";
    private bool m_inverseController = false;
    [SerializeField] private string m_inverseControllerDescription = "Inverse controller";
    private bool m_decreaseSpeed = false;
    [SerializeField] private string m_decreaseSpeedDescription = "Speed down";
    private bool m_decreaseRegenFuel = false;
    [SerializeField] private string m_decreaseRegenFuelDescription = "Fuel Regen down";

    private List<bool> m_bonus = new List<bool>();
    private List<bool> m_malus = new List<bool>();

    private List<string> m_bonusDescritpion = new List<string>();
    private List<string> m_malusDescritpion = new List<string>();

    private int m_bonusChoice = 0;
    private int m_malusChoice = 0;


    [SerializeField] private Text m_bonusText;
    [SerializeField] private Text m_malusText;


    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_playerPerks = m_player.GetComponent<PlayerPerks>();

        Generate();
        SetBonusText();
        SetMalusText();
        m_animator.SetBool("IsChoosingPerk", true);
    }

    public void Setup()
    {
        Generate();
        SetBonusText();
        SetMalusText();
        m_animator.SetBool("IsChoosingPerk", true);
    }

    public void Generate()
    {
        m_bonus.Add(m_increaseHP);
        m_bonusDescritpion.Add(m_increaseHPDescription);
        m_bonus.Add(m_asteroidsDeviation);
        m_bonusDescritpion.Add(m_asteroidsDeviationDescription);
        m_bonus.Add(m_increaseSpeed);
        m_bonusDescritpion.Add(m_increaseSpeedDescription);
        m_bonus.Add(m_increaseRegenFuel);
        m_bonusDescritpion.Add(m_increaseRegenFuelDescription);

        m_malus.Add(m_decreaseHP);
        m_malusDescritpion.Add(m_decreaseHPDescription);
        m_malus.Add(m_inverseController);
        m_malusDescritpion.Add(m_inverseControllerDescription);
        m_malus.Add(m_decreaseSpeed);
        m_malusDescritpion.Add(m_decreaseSpeedDescription);
        m_malus.Add(m_decreaseRegenFuel);
        m_malusDescritpion.Add(m_decreaseRegenFuelDescription);

        m_bonusChoice = Random.Range(0, m_bonus.Count);
        m_malusChoice = Random.Range(0, m_malus.Count);
        m_bonus[m_bonusChoice] = true;
        m_malus[m_malusChoice] = true;
    }

    public void SetBonusText()
    {
        m_bonusText.text = m_bonusDescritpion[m_bonusChoice];
    }

    public void SetMalusText()
    {
        m_malusText.text = m_malusDescritpion[m_malusChoice];
    }

    public void Choose()
    {
        // Bonus
        if (m_bonusText.text == m_increaseHPDescription)
        {
            m_playerPerks.IncreaseMaxHP();
        }
        else if (m_bonusText.text == m_asteroidsDeviationDescription)
        {
            m_playerPerks.m_asteroidDeviation = true;
        }
        else if (m_bonusText.text == m_increaseSpeedDescription)
        {
            m_playerPerks.IncreaseSpeed();
        }
        else if (m_bonusText.text == m_increaseRegenFuelDescription)
        {
            m_playerPerks.IncreaseRegenFuel();
        }

        // Malus
        if (m_malusText.text == m_decreaseHPDescription)
        {
            m_playerPerks.DecreaseMaxHP();
        }
        else if (m_malusText.text == m_inverseControllerDescription)
        {
            m_playerPerks.m_inverseController = true;
        }
        else if (m_malusText.text == m_decreaseSpeedDescription)
        {
            m_playerPerks.DecreaseSpeed();
        }
        else if (m_malusText.text == m_decreaseRegenFuelDescription)
        {
            m_playerPerks.DecreaseRegenFuel();
        }


        m_animator.SetBool("IsChoosingPerk", false);
    }
}
