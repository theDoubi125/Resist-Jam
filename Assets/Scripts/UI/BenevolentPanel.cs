using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BenevolentPanel : MonoBehaviour
{
    private Image[] m_images;

    [SerializeField]
    private int m_value;

	void Start ()
    {
        m_images = GetComponentsInChildren<Image>();
        UpdateRender();
	}

    public void AddBenevolent(int count)
    {
        m_value += count;
        UpdateRender();
    }

    public bool RemoveBenevolent(int count)
    {
        if (m_value < count)
            return false;
        m_value -= count;
        UpdateRender();
        return true;
    }

    private void UpdateRender()
    {
        for (int i = 0; i < m_images.Length; i++)
        {
            if (i < m_value)
                m_images[i].gameObject.SetActive(true);
            else m_images[i].gameObject.SetActive(false);
        }
    }

    public bool IsChangeValid(int diff)
    {
        return m_value + diff >= 0;
    }
}
