using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float playerSpeed = 3f;
    Rigidbody playerRb;

    public int playerHp; // 현재 생명력
    public int playerMaxHp = 10; // 최대 체력
    public int damage = 0; // 데미지
    
    public Slider HpBarSlider;

    Animator playerDie;

    public GameObject joystick;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerDie = gameObject.GetComponent<Animator>();
    }

    public void Move(Vector2 dir)
    {
        playerRb.velocity = new Vector3(-dir.x * playerSpeed, 0, -dir.y * playerSpeed);

        transform.forward = playerRb.velocity;
    }

    private void Update()
    {
        CheckHp();
    }


    //생명 , 공격

    // playerMaxHp(최대hp)에서 damage를 빼면 playerHp(현재hp)
    public void CheckHp()
    {
        
        if (HpBarSlider != null)
        {
            //hp 업데이트
            playerHp = playerMaxHp - damage;
            HpBarSlider.value = playerHp;

            if (playerHp <= 0)//player 체력이 0이면
            {
                playerDie.SetBool("isDie", true);
                Destroy(HpBarSlider, 1);
                joystick.SetActive(false);
            }
        }
    }

    // void Try()
    //{
    //    int result = 0;
    //    Action<int> act1= (x) => result = x + 1 ;
    //}
}
