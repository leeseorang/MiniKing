using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float playerSpeed = 3f;
    Rigidbody playerRb;

    public float playerHp; // 생명력
    public float playerMaxHp; // 최대 체력

    public Slider HpBarSlider;

    Animator playerDie;

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

    // 기초 hp 체력 셋팅
    public void SetHp(float MaxHp)
    {
        playerMaxHp = MaxHp;
        playerHp = playerMaxHp;
    }
    public void CheckHp()
    {
        if (HpBarSlider != null)//hp 업데이트
        {
            HpBarSlider.value = playerHp / playerMaxHp;
        }
    }
    public void Damage(float damage)
    {
        if (playerMaxHp == 0 || playerHp <= 0)
            return;

        playerHp -= damage;
        CheckHp();//hp 업데이트

        if (playerHp <= 0)//player 체력이 0이면
        {
            playerDie.SetBool("isDie", true);
        }
    }
}
