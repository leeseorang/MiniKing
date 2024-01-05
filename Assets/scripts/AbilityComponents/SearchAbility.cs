using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CharacterAbility
{
    public class SearchAbility : Ability
    {
        private string m_searchTag = "Player";
        //private Coroutine m_Cor_Check;
        private Coroutine m_Cor_Find;
        private GameObject m_target;
        protected  Action<GameObject> Act_Finded;
        [SerializeField] LayerMask m_SearchLayer;
        [SerializeField, Range(0, 5)] private float m_SearchRange = 1;
#if UNITY_EDITOR
        [SerializeField][ReadOnly] GameObject m_SearchedOBJ;
#endif
        public virtual void SubscribeFindEvent(Action<GameObject> action)
        {
            Act_Finded += action;
        }
        public virtual void UnSubFindEvent(Action<GameObject> action)
        {
            Act_Finded -= action;
        }
        protected override void Awake()
        {
            base.Awake();
        }
        private void OnEnable()
        {
            FindTarget();
            //m_Cor_Check ??= StartCoroutine(Checker(1, () => InvokeSearchEvent(m_searchTag)));
        }
        private void OnDisable()
        {
            StopAllCoroutines();
            //StopCoroutine(m_Cor_Check);
        }
        public void FindTarget()
        {
            m_Cor_Find ??= StartCoroutine(FindChecker(1,(x)=> Act_Finded?.Invoke(x)));
        }
        private void InvokeSearchEvent(string searchTag)
        {
            m_target = GameObject.FindGameObjectWithTag(searchTag);
            if (m_target != null)
            {
                Act_Finded?.Invoke(m_target);
            }
        }
        IEnumerator Checker(float eachTime, Action action)
        {
            var lastCheckTime = Time.time;
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
        IEnumerator FindChecker(float eachTime, Action<GameObject> action)
        {
            var lastCheckTime = Time.time;
            while (true)
            {
                if (Time.time > lastCheckTime + eachTime)
                {
                    Collider[] colliders = Physics.OverlapSphere(transform.position, m_SearchRange, m_SearchLayer);
                    if (colliders.Length>0)
                    {
                        Collider col = colliders[0];
                        lastCheckTime = Time.time;
                        action?.Invoke(col.gameObject);
#if UNITY_EDITOR
                        m_SearchedOBJ = col.gameObject;
#endif
                        Debug.Log("~~");
                        StopCoroutine(m_Cor_Find);
                        m_Cor_Find = null;
                        yield break;
                    }
                }
                yield return new WaitForEndOfFrame();
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, m_SearchRange);
        }
        private void ShowRange()
        {
            
        }
    }
}
