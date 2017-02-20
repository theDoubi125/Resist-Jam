using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField]
    public float m_roadSize;
    [SerializeField]
    public int m_roadInstances;
    [SerializeField]
    private float m_distBeforeMove;

	void Update ()
    {
        if(CameraController.ProtestTailPos - transform.position.x > m_distBeforeMove)
        {
            transform.position += Vector3.right * m_roadSize * m_roadInstances;
        }
	}
}
