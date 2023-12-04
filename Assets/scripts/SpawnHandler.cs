using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_Spawnables;
    [SerializeField]
    private BoxCollider m_Range;
    [SerializeField]
    private Transform m_parent;
    private Dictionary<int, Queue<GameObject>> m_objectPool=new();
    [SerializeField]
    private bool m_Debug=false;

    
    public GameObject SpawnAt(GameObject obj, Vector3 wPosition, Quaternion rotation)
    {
        return Instantiate(obj, wPosition, rotation, m_parent);
    }
    public GameObject SpawnAt(GameObject obj, Vector2 wPos)
    {
        return SpawnAt(obj, new Vector3(wPos.x, wPos.y, 0), Quaternion.identity);
    }
    public GameObject SpawnAt(GameObject obj, Vector3 wPos)
    {
        return SpawnAt(obj, wPos, Quaternion.identity);
    }
    public GameObject SpawnAt(int idx, Vector3 wPosition, Quaternion rotation)
    {
        if (m_objectPool.ContainsKey(idx))
        {
            if (m_objectPool[idx].Count > 0)
            {
                GameObject obj = m_objectPool[idx].Dequeue();
                SpawnInit(obj);
                return obj;
            }
        }
        else
        {
            m_objectPool.Add(idx, new());
        }
        return Instantiate(m_Spawnables[idx], wPosition, rotation, m_parent);
    }
    public void Collect(int poolNum,GameObject obj)
    {
        if (m_objectPool.ContainsKey(poolNum))
        {
            m_objectPool.Add(poolNum, new());

        }
        m_objectPool[poolNum].Enqueue(obj);
        obj.SetActive(false);
    }
    public void SpawnInit(GameObject obj)
    {
        obj.SetActive(true);
    }
    public GameObject SpawnAt(int idx, Vector2 wPos)
    {
        return SpawnAt(idx, new Vector3(wPos.x, wPos.y, 0), Quaternion.identity);
    }
    public GameObject SpawnAt(int idx, Vector3 wPos)
    {
        return SpawnAt(idx, wPos, Quaternion.identity);
    }
    void Start()
    {
        SpawnAt(0,GetRandomPosition());
    }
    Vector3 GetRandomPosition()
    {
        Vector3 max = m_Range.bounds.max;
        Vector3 min = m_Range.bounds.min;
        
        return new Vector3(Random.Range(min.x,max.x),Random.Range(min.y,max.y),Random.Range(min.z,max.z));
    }
#if UNITY_EDITOR
    private void Update()
    {
        if(m_Debug)
        if (Input.GetMouseButtonDown(0))
        {
            SpawnAt(m_Spawnables[0], GetRandomPosition());
        }
    }
#endif
}
