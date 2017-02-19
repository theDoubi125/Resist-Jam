﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hooligan : WalkingProtester
{
    private Breakable m_target;

    [SerializeField]
    private float m_visionRadius;

    [SerializeField]
    private float m_attackRatio;

    [SerializeField]
    private float m_breakSpeed;

    [SerializeField]
    private float m_breakMass = 3;

    private int m_layerMask;

    [SerializeField]
    private Sprite[] m_attackSprites;

    [SerializeField]
    private float m_attackAnimSpeed = 0.3f;

    [SerializeField]
    private float m_attackSuccessRate = 0.5f;

    [SerializeField]
    private float m_attackRange = 0.5f;

    [SerializeField]
    private float m_animTime = 0;

    private SpriteRenderer m_renderer;
    private int m_lastAnimId = 0;
    private float m_posFromCenter = 0;

    private HashSet<Breakable> m_checkedBreakables = new HashSet<Breakable>();
    private bool m_getBack = false;

    public override void Start()
    {
        m_renderer = GetComponentInChildren<SpriteRenderer>();
        m_layerMask = LayerMask.GetMask("Breakable");
        base.Start();
    }

    public override void FixedUpdate ()
    {
        if (m_getBack)
        {
            Vector3 target = new Vector3(m_spawner.ProtestCenter + m_posFromCenter, 0, 0);
            Vector3 dir = (target - transform.position).normalized;
            transform.position += dir * m_breakSpeed * Time.fixedDeltaTime;
            if (Vector3.Distance(transform.position, target) < m_attackRange)
            {
                m_target = null;
                m_rigidbody.GetComponent<Collider>().enabled = true;
                m_getBack = false;
            }
        }
        else if (m_target == null)
        {
            base.FixedUpdate();
            Collider[] breakables = Physics.OverlapSphere(transform.position, m_visionRadius, m_layerMask);
            if (breakables.Length > 0)
            {
                foreach (Collider collider in breakables)
                {
                    Breakable breakable = collider.GetComponent<Breakable>();
                    if (!m_checkedBreakables.Contains(breakable))
                    {
                        if (Random.Range(0f, 1f) < m_attackRatio)
                        {
                            m_target = breakable;
                            m_rigidbody.mass = m_breakMass;
                            m_rigidbody.GetComponent<Collider>().enabled = false;
                            m_posFromCenter = transform.position.x - m_spawner.ProtestCenter;
                        }
                        m_checkedBreakables.Add(breakable);
                    }
                }
            }
        }
        else
        {
            if ((m_target.transform.position - transform.position).magnitude < m_attackRange)
            {
                m_animTime += Time.deltaTime;
                m_renderer.GetComponent<Animator>().enabled = false;
                int spriteId = Mathf.FloorToInt(m_animTime / m_attackAnimSpeed) % m_attackSprites.Length;
                m_renderer.sprite = m_attackSprites[spriteId];
                if(m_lastAnimId > spriteId)
                {
                    if(Random.Range(0f, 1f) > m_attackSuccessRate)
                    {
                        m_target.Break();
                        m_target = null;
                        m_renderer.GetComponent<Animator>().enabled = true;
                        transform.localScale = new Vector3(1, 1, 1);
                        m_rigidbody.mass = 1;
                        m_getBack = true;
                    }
                }
                m_lastAnimId = spriteId;
            }
            else
            {
                transform.position += (m_target.transform.position - transform.position).normalized * m_breakSpeed * Time.fixedDeltaTime;
                float dir = (m_target.transform.position - transform.position).x > 0 ? 1 : -1;
                transform.localScale = new Vector3(dir, 1, 1);
            }

        }

	}
}
