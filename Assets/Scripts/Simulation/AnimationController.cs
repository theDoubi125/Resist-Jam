using System;
using System.Collections.Generic;
using UnityEngine;

public enum AnimEvent
{
    FRAME, LOOP
}

public class AnimationController : MonoBehaviour
{
    public Sprite Current { get { return m_animations[m_selectedAnim].GetSprite(m_selectedSprite); } }
    public delegate void AnimationDelegate(AnimationController animation, AnimEvent animEvent);
    private AnimationDelegate m_animListener;

    [SerializeField]
    private Animation[] m_animations;

    [SerializeField]
    private float m_animSpeed = 0;

    private float m_time = 0;
    private int m_selectedAnim = 0;
    private int m_selectedSprite = 0;

    private SpriteRenderer m_spriteRenderer = null;

    public void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        m_time += Time.deltaTime;
        if (m_time > m_animSpeed)
        {
            m_selectedSprite++;
            if (m_selectedSprite >= m_animations[m_selectedAnim].SpriteCount)
            {
                m_selectedSprite = 0;

                if (m_animListener != null)
                    m_animListener(this, AnimEvent.LOOP);
            }
            m_time -= m_animSpeed;
            if (m_animListener != null)
                m_animListener(this, AnimEvent.FRAME);
            m_spriteRenderer.sprite = Current;
        }
    }

    public void RegisterListener(AnimationDelegate listener)
    {
        m_animListener += listener;
    }

    public void RemoveListener(AnimationDelegate listener)
    {
        m_animListener -= listener;
    }

    public void SelectAnim(int anim)
    {
        m_selectedAnim = anim;
        m_selectedSprite = 0;
        m_time = 0;
        m_spriteRenderer.sprite = Current;
    }
}