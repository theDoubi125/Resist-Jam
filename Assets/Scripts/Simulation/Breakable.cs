using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField]
    private Sprite m_brokenSprite;

    private SpriteRenderer m_renderer;

	void Start ()
    {
        m_renderer = GetComponent<SpriteRenderer>();
	}
	
	void Update ()
    {
		
	}

    public void Break()
    {
        m_renderer.sprite = m_brokenSprite;
    }
}
