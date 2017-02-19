using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField]
    Transform[] m_brokenElements = null;
    [SerializeField]
    Transform[] m_intactElements = null;

    private SpriteRenderer m_renderer;

	void Start ()
    {
        m_renderer = GetComponent<SpriteRenderer>();
        foreach (Transform element in m_brokenElements)
            element.gameObject.SetActive(false);
        foreach (Transform element in m_intactElements)
            element.gameObject.SetActive(true);
    }
	
	void Update ()
    {
		
	}

    public void Break()
    {
        foreach (Transform element in m_brokenElements)
            element.gameObject.SetActive(true);
        foreach (Transform element in m_intactElements)
            element.gameObject.SetActive(false);
    }
}
