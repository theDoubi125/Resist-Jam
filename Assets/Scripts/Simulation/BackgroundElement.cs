using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnReleaseDelegate(Transform prefab, Transform instance);

public class BackgroundElement : MonoBehaviour
{
    [SerializeField]
    float m_distanceBeforeUnspawn = 5;

    private Transform m_prefab;

    public virtual void Update()
    {
        if(CameraController.ProtestTailPos > transform.position.x + m_distanceBeforeUnspawn)
        {
            ReleaseDelegate(m_prefab, transform);
            gameObject.SetActive(false);
        }
    }

    public virtual void Init(Transform prefab)
    {
        m_prefab = prefab;
    }

    public OnReleaseDelegate ReleaseDelegate;
}