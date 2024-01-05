using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CharacterAbility
{
    public class AttackAbility : Ability
    {
        [SerializeField]
        private float m_attackDist = 1;
        [SerializeField]
        private float m_attackCooltime = 1f;
        [SerializeField]
        private float m_attackPretime = 0.1f;
        [SerializeField]
        private float m_damage = 1;
        [SerializeField,ReadOnly]
        private float m_lastAtktime;
        private SearchAbility m_searchAbility;
        private TargetInfo m_target;
        public TargetInfo GetTargetInfo()=>m_target;

        private Coroutine m_Cor_Atk;
        public event EventHandler<float> FuncAttackDamage;

        public class TargetInfo
        {
            public EventHandler<float> FuncAttackDamage;//여기에 health 연결하여 데미지를 받게 하면 됨. 서치한 타겟의 health정보를 가져와야 함
                                                        //. 서치 어빌리티가 가져오면 좋을것 같음.
            public Transform tf;
            public TargetInfo(EventHandler<float> funcAttackDamage)
            {
                FuncAttackDamage = funcAttackDamage;
            }
        }
        private void OnEnable()
        {
            StartAttack();
        }
        private void OnDisable()
        {
            StopAttack();
        }
        protected override void Awake()
        {
            base.Awake();
            m_lastAtktime = Time.time;
            m_target = new TargetInfo(FuncAttackDamage);
            m_searchAbility = GetComponent<SearchAbility>();
            if(m_searchAbility != null )
            m_searchAbility.SubscribeFindEvent(TargetSet);
        }
        void TargetSet(GameObject go)
        {
            m_target.tf = go.transform;
            StartAttack();
        }
        public void StartAttack()
        {
            m_Cor_Atk ??= StartCoroutine(Co_Attack());
        }
        public void StopAttack()
        {
            if (m_Cor_Atk != null)
            {
                StopCoroutine(m_Cor_Atk);
            }
        }
        IEnumerator Co_Attack()
        {
            while (true)
            {
                if (m_target.tf == null) yield break;
                if (Time.time > m_lastAtktime + m_attackCooltime&&CheckEnableAttack())
                {
                    m_lastAtktime = Time.time;
                    DoAttack();
                    var dir = m_target.tf.position - transform.position;
                    transform.forward = dir;
                }
                yield return new WaitForEndOfFrame();
            }
        }
        private bool CheckEnableAttack()
        {
            return Vector3.Distance(m_target.tf.position, transform.position) < m_attackDist;
        }
        private void DoAttack()
        {
            DoAtkAnim();
            Invoke("ApplyDamage", m_animator.GetCurrentAnimatorStateInfo(0).length/1.8f);
        }

        private void ApplyDamage()
        {
            FuncAttackDamage?.Invoke(null,m_damage);
        }

        private void DoAtkAnim()
        {
            m_animator.SetTrigger("Attack");
        }
    }
}