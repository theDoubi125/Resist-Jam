using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum StatType
{
    SECURITY, ANIMATORS, MONEY_COLLECTORS, CAMERAMEN
}

public class StatPanel : MonoBehaviour
{
    [SerializeField]
    private Text m_valueText;

    [SerializeField]
    private Text m_moneyCostText;

    [SerializeField]
    private Text m_benevolentCostText;

    [SerializeField]
    private int m_moneyCost;

    [SerializeField]
    private int m_benevolentCost;

    [SerializeField]
    private int m_value;

    private ConfigMenu m_mainMenu;

    [SerializeField]
    private StatType m_type;

	void Start ()
    {
        m_valueText.text = "" + m_value;
        m_moneyCostText.text = "" + m_moneyCost + " €";
        m_benevolentCostText.text = "" + m_benevolentCost;
    }

    public void SetMainMenu(ConfigMenu menu)
    {
        m_mainMenu = menu;
    }

    public void incrementValue()
    {
        if(m_mainMenu.isBenevolentChangeValid(-m_benevolentCost) && m_mainMenu.isMoneyChangeValid(-m_moneyCost))
        {
            m_mainMenu.ChangeMoney(-m_moneyCost);
            m_mainMenu.ChangeBenevolents(-m_benevolentCost);
            m_value++;
            m_valueText.text = "" + m_value;
        }
    }

    public void decrementValue()
    {
        if (m_value <= 0)
            return;
        m_mainMenu.ChangeMoney(m_moneyCost);
        m_mainMenu.ChangeBenevolents(m_benevolentCost);
        m_value--;
        m_valueText.text = "" + m_value;
    }

    public StatType Type { get { return m_type; } }
    public int Value { get { return m_value; } }
}
