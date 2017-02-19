using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopController : MonoBehaviour
{
    [SerializeField]
    private float m_walkSpeed;

    [SerializeField]
    private float m_dirChangeRate;

    [SerializeField]
    private float m_minPatrolDuration;
    [SerializeField]
    private float m_maxPatrolDuration;

    private float m_patrolTime = 0;
    private float m_patrolDir = 0;

	void Start ()
    {
		
	}
	
	void FixedUpdate ()
    {
        if(m_patrolTime > 0)
        {
            transform.position += Vector3.right * m_patrolDir * m_walkSpeed * Time.fixedDeltaTime;
            m_patrolTime -= Time.fixedDeltaTime;
        }
        else if (m_patrolTime <= 0 && Random.Range(0f, 1f) < m_dirChangeRate)
        {
            m_patrolDir = Random.Range(0f, 1f) > 0.5f ? -1 : 1;
            transform.localScale = new Vector3(m_patrolDir, 1, 1);
            m_patrolTime = Random.Range(m_minPatrolDuration, m_maxPatrolDuration);
        }
	}
}
