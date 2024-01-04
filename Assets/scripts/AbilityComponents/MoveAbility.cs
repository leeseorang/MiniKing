using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CharacterAbility
{
    [RequireComponent(typeof(SearchAbility))]
    public class MoveAbility : Ability
    {
        [SerializeField]
        private float m_speed=1;
        [SerializeField]
        private float m_mesh = 1;//덩치
        SearchAbility m_search;
        Transform m_target;
        public enum State
        {
            None,Approach,StepBack
        }
        public State state;
        protected override void Awake()
        {
            base.Awake();
            m_search = GetComponent<SearchAbility>();
            state=State.Approach;
        }
        private void OnEnable()
        {
            if (m_search != null)
            {
                m_search.SubscribeFindEvent(Finded);
            }
        }
        private void OnDisable()
        {
            if (m_search != null)
            {
                m_search.UnSubFindEvent(Finded);
            }
        }
        private void Update()
        {
            switch (state)
            {
                case State.None:
                    break;
                case State.Approach:
                    Approach();
                    break;
                case State.StepBack:
                    StepBack();
                    break;
                    
            }
            LookAt();
        }
        private void Approach()
        {
            if (m_target == null) return;
            var distancev = m_target.position - transform.position;
            var distance= Vector3.Magnitude(distancev);
            if (distance < m_mesh) return;
            var dir=Vector3.Normalize(distancev);
            var velocity=dir*m_speed*Time.deltaTime;
            transform.position = transform.position + velocity;
        }
        private void StepBack()
        {
            if (m_target == null) return;
            var distancev = m_target.position - transform.position;
            var distance = Vector3.Magnitude(distancev);
            if (distance > 3) return;
            var dir = Vector3.Normalize(distancev);
            var velocity = -dir * m_speed * Time.deltaTime;
            transform.position = transform.position + velocity;
        }
        private void LookAt()
        {
            if (m_target == null) return;
            var distancev = m_target.position - transform.position;
            var dir = Vector3.Normalize(distancev);
            transform.forward = Vector3.Lerp(transform.forward, dir, 0.8f);
        }
        private void Finded(GameObject go)
        {
            m_target = go.transform;
        }
    }
}
