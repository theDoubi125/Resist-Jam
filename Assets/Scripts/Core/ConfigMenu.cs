using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ConfigMenu : MonoBehaviour
{
    private StatPanel[] m_statPanels;

    private BenevolentPanel m_benevolents = null;

    [SerializeField]
    private Text m_moneyText = null;

    [SerializeField]
    private int m_money;

    [SerializeField]
    private int m_animCount = 2;


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

    public int[] ComputeStats()
    {
        int[] result = new int[Enum.GetNames(typeof(StatType)).Length];
        for (int i = 0; i < result.Length; i++)
            result[i] = 0;
        foreach(StatPanel statPanel in m_statPanels)
        {
            result[(int)statPanel.Type] = statPanel.Value;
        }
        return result;
    }

    public void TestStats()
    {
        int[] stats = ComputeStats();
        for (int i=0; i< stats.Length; i++)
        {
            Debug.Log((StatType)i + " : " + stats[i]);
        }
    }
}
