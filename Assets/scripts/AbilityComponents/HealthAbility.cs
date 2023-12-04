using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAbility
{
    public class HealthAbility : Ability
    {
        public int Health { get=> m_health; set { m_health = value; if (m_health <= 0) DoDie(); } }
        public bool IsAlive => m_isAlive;
        bool m_isAlive=true;
        int m_health=2;
        public event Action<GameObject> Act_Die;

        void DoDie()
        {
            m_health = 0;
            m_isAlive = false;
            DieAnim();
            Act_Die?.Invoke(gameObject);           
        }

        private void DieAnim()
        {
            m_animator.SetBool("IsDie", true);
        }
    }
}
