using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAbility
{
    public class SearchAbility : Ability
    {
        private string m_searchTag="Player";
        private Coroutine m_Cor_Check;
        private GameObject m_target;
        public Action<GameObject> Act_Finded;
        protected override void Awake()
        {
            base.Awake();
        }
        private void OnEnable()
        {
            m_Cor_Check??=StartCoroutine(Checker(1, ()=>StartSearch(m_searchTag)));
        }
        private void OnDisable()
        {
            StopCoroutine(m_Cor_Check);
        }
        private void StartSearch(string searchTag)
        {
            m_target = GameObject.FindGameObjectWithTag(searchTag);
            if(m_target != null)
            {
                Act_Finded?.Invoke(m_target);
            }
        }
        IEnumerator Checker(float eachTime,Action action)
        {
            var lastCheckTime=Time.time;
            while (true)
            {
                if (Time.time > lastCheckTime + eachTime)
                {
                    lastCheckTime = Time.time;
                    action?.Invoke();
                    Debug.Log("~~");
                }
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
