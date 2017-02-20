using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    private Transform[] m_firstFloorPrefab = null;
    [SerializeField]
    private Transform[] m_secondFloorPrefab = null;
    [SerializeField]
    private float m_cellWidth = 80;
    [SerializeField]
    private float m_cellHeight = 60;
    [SerializeField]
    private float m_spawnDistance = 10;
    private int m_spawnedPos = 0;

    private Dictionary<Transform, Stack<Transform>> m_instances = new Dictionary<Transform, Stack<Transform>>();

	void Start ()
    {
		for(int i=-5; i<10; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                if (j == 0)
                {
                    SpawnElement(i, j, m_firstFloorPrefab);
                }
                if (j == 1)
                {
                    SpawnElement(i, j, m_secondFloorPrefab);
                }
            }
        }
	}

    public void SpawnElement(int pos, int floor, Transform[] prefabs)
    {
        Transform prefab = prefabs[Random.Range(0, prefabs.Length)];
        Transform instance = Transform.Instantiate<Transform>(prefab, transform.position + new Vector3(m_cellWidth * pos, m_cellHeight * floor, 0), Quaternion.identity);
        BackgroundElement element = instance.GetComponent<BackgroundElement>();
        element.Init(prefab);
        element.ReleaseDelegate = OnElementRelease;
        if(m_spawnedPos < pos)
            m_spawnedPos = pos;
    }

    public void OnElementRelease(Transform prefab, Transform instance)
    {
        if (!m_instances.ContainsKey(prefab))
            m_instances[prefab] = new Stack<Transform>();
        m_instances[prefab].Push(instance);
    }
	
	void Update ()
    {
        int lastToSpawn = (int)((CameraController.CameraCenter + m_spawnDistance) / m_cellWidth);

        if (m_spawnedPos < lastToSpawn)
        {
            for(int i=m_spawnedPos+1; i< lastToSpawn; i++)
            {
                for(int j=0; j<2; j++)
                {
                    if (j == 0)
                    {
                        SpawnElement(i, j, m_firstFloorPrefab);
                    }
                    if (j == 1)
                    {
                        SpawnElement(i, j, m_secondFloorPrefab);
                    }
                }
            }
        }
	}
}
