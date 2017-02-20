using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private ProtestSpawner m_protest;

    public static float CameraCenter { get; private set; }
    public static float ProtestTailPos { get; private set; }
    public static float ProtestHeadPos { get; private set; }

    [SerializeField]
    private float m_speed;

    private float m_pos;

    void Start ()
    {
        transform.position = new Vector3(m_protest.ProtestCenter, transform.position.y, transform.position.z);
    }

	void FixedUpdate ()
    {
        m_pos += Input.GetAxis("Horizontal") * m_speed;
        if (m_pos < m_protest.ProtestTail - m_protest.ProtestCenter)
            m_pos = m_protest.ProtestTail - m_protest.ProtestCenter;
        if (m_pos > m_protest.ProtestHead - m_protest.ProtestCenter)
            m_pos = m_protest.ProtestHead - m_protest.ProtestCenter;
        transform.position = new Vector3(m_protest.ProtestCenter + m_pos, transform.position.y, transform.position.z);
        CameraCenter = m_protest.ProtestCenter + m_pos;
        ProtestTailPos = m_protest.ProtestTail;
        ProtestHeadPos = m_protest.ProtestHead;
    }
}
