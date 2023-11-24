using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // 발사체 삭제 조건
        Destroy(gameObject);
    }
}
