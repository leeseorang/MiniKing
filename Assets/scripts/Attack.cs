using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // 앞으로 발사!!
    private void Start()
    {
        Debug.Log("Hi");
    }

    private void OnTriggerEnter(Collider other)
    {
        // 발사체 삭제 조건
        Destroy(gameObject);
        Debug.Log("Hi1");
    }
}
