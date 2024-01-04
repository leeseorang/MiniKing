using CharacterAbility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AttackAbility),typeof(SearchAbility))]
public class AttackPlayerModule : MonoBehaviour
{
    AttackAbility m_attackability;
    SearchAbility m_searchAbility;
    Player m_player;
    private void Awake()
    {
        m_attackability = GetComponent<AttackAbility>();
        m_searchAbility = GetComponent<SearchAbility>();
        m_attackability.FuncAttackDamage += PlayerSend;
    }
    private void OnEnable()
    {
        m_searchAbility.SubscribeFindEvent(FindPlayer());
    }
    private void OnDisable()
    {
        m_searchAbility.UnSubFindEvent(FindPlayer());
    }
    private Action<GameObject> FindPlayer()
    {
        return (x)=> { m_player ??= x.GetComponent<Player>(); };
    }
    private void PlayerSend(object sender, float e)
    {
        if(m_player!= null)
        {
            m_player.GetDamage((int)e);
        }
    }
}
