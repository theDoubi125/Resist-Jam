﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigMenu : MonoBehaviour
{
    private StatPanel[] m_statPanels;

    private BenevolentPanel m_benevolents = null;

    [SerializeField]
    private Text m_moneyText = null;

    [SerializeField]
    private int m_money;

	void Start ()
    {
        m_statPanels = GetComponentsInChildren<StatPanel>();
        m_benevolents = GetComponentInChildren<BenevolentPanel>();
        m_moneyText.text = m_money + " €";
        foreach (StatPanel statPanel in m_statPanels)
            statPanel.SetMainMenu(this);
	}

    public bool ChangeMoney(int diff)
    {
        if (m_money + diff < 0)
            return false;
        m_money += diff;
        m_moneyText.text = m_money + " €";
        return true;
    }

    public bool isMoneyChangeValid(int diff)
    {
        return m_money + diff >= 0;
    }

    public bool isBenevolentChangeValid(int diff)
    {
        return m_benevolents.IsChangeValid(diff); ;
    }

    public bool ChangeBenevolents(int diff)
    {
        if (diff < 0)
            return m_benevolents.RemoveBenevolent(-diff);
        m_benevolents.AddBenevolent(diff);
        return true;
    }
}
