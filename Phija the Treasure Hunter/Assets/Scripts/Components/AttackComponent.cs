using System;
using System.Linq;
using Scripts.Components;
using Unity.VisualScripting;
using UnityEngine;

namespace Components
{
    public class AttackComponent : MonoBehaviour
    {

        [SerializeField] private string team;
        [SerializeField] private int attackRange;

        /// <summary>
        /// Attack range of the game objects. Only targets inside of range will lose health.
        /// </summary>
        public int AttackRange
        {
            get => attackRange;
            set => attackRange = value;
        }

        /// <summary>
        /// Team of the game object. Either 'enemy' or 'hero'
        /// </summary>
        public string Team
        {
            get => team;
            set => team = value;
        }

        private void Start()
        {
            gameObject.AddComponent<CircleCollider2D>();
        }

        private void FixedUpdate()
        {
            var objectsInRange = Physics2D.OverlapCircleAll(transform.position, AttackRange);
            if (objectsInRange.Length == 0) return;
            var enemiesInRange = Array.FindAll(objectsInRange, val => val.gameObject.GetComponent<AttackComponent>().Team != Team).ToArray();
            if (enemiesInRange.Length == 0) return;
            enemiesInRange[0].GetComponent<HealthComponent>().LoseHealth(1);
        }

    }
}