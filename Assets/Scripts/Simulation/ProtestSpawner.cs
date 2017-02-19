using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtestSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform m_protesterPrefab;

    [SerializeField]
    private int m_protesterCount;

    [SerializeField]
    private float m_spawnRadius;

    private List<Transform> m_protesters = new List<Transform>();

    public float ProtestCenter = 0;
    public float ProtestHead = 0;
    public float ProtestTail = 0;

    void Start ()
    {
		for(int i=0; i<m_protesterCount; i++)
        {
            Transform protester = Transform.Instantiate<Transform>(m_protesterPrefab, Quaternion.Euler(0, Random.Range(0f, 360f), 0) * new Vector3(Random.Range(0f, m_spawnRadius), 0, 0), Quaternion.identity);
            protester.GetComponent<WalkingProtester>().SetSpawner(this);
            m_protesters.Add(protester);
        }
        UpdatePos();
    }
	
	void Update ()
    {
        UpdatePos();
	}

    void UpdatePos()
    {
        ProtestHead = m_protesters[0].position.x;
        ProtestTail = m_protesters[0].position.x;
        ProtestCenter = 0;
        foreach (Transform protester in m_protesters)
        {
            ProtestCenter += protester.position.x;
            if (ProtestHead < protester.position.x)
                ProtestHead = protester.position.x;
            if (ProtestTail > protester.position.x)
                ProtestTail = protester.position.x;
        }
        ProtestCenter /= m_protesters.Count;
    }
}
