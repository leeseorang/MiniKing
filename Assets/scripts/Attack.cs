using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    List<GameObject> enemys;
    GameObject enemy;

    float shortDis;

    Vector3 attackPos;

    void Start()
    {
        enemys = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));

        // 여기 내용 공부할것
        if (enemys.Count > 0)
        {
            //Distance 거리 재기?
            shortDis = Vector3.Distance(gameObject.transform.position, enemys[0].transform.position);
            enemy = enemys[0];

            foreach (GameObject foundEnemy in enemys)
            {
                float Distance = Vector3.Distance(gameObject.transform.position, foundEnemy.transform.position);
                if (Distance < shortDis)
                {
                    enemy = foundEnemy;
                    shortDis = Distance;
                }
            }
        }

        // enemy의 위치 - 발사체 위치 
        // normalized 백터 정규화(균일한 이동)
        if (enemys.Count > 0)
        {
            attackPos = (enemy.transform.position - transform.position).normalized;
        }
    }
    private void Update()
    {
        transform.position += attackPos * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 발사체 삭제 조건
        Destroy(gameObject);

        if (other.transform.tag == "Enemy")
        {
            Destroy(enemy);
        }
    }
}