using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace CharacterAbility
{
    [RequireComponent(typeof(Animator)),AddComponentMenu("")]
    public class Ability : MonoBehaviour
    {
        protected Animator m_animator;
        protected virtual void Awake()
        {
            m_animator = GetComponent<Animator>();
        }
    }
}
