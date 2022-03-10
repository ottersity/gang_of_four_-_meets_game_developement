using System;
using System.Linq;
using Pattern.Visitor;
using Scripts.Components;
using Unity.VisualScripting;
using UnityEngine;

namespace Components
{
    public class AttackComponent : MonoBehaviour, IElement
    {

        [SerializeField] private int attackRange;

        /// <summary>
        /// Attack range of the game objects. Only targets inside of range will lose health.
        /// </summary>
        public int AttackRange
        {
            get => attackRange;
            set => attackRange = value;
        }

        [SerializeField] private int attackPower;

        /// <summary>
        /// This value decides how much hp an enemy will lose after being hit.
        /// </summary>
        public int AttackPower
        {
            get => attackPower;
            set => attackPower = value;
        }

        private void FixedUpdate()
        {
            var objectsInRange = Physics2D.OverlapCircleAll(transform.position, AttackRange);
            if (objectsInRange.Length == 0) return;

            var enemiesInRange = Array.FindAll(
                objectsInRange,
                val => !val.gameObject.CompareTag(tag) && !val.gameObject.CompareTag("NoTeam")
            ).ToArray();
            if (enemiesInRange.Length == 0) return;

            enemiesInRange[0].GetComponent<HealthComponent>().LoseHealth(attackPower);
        }

        public void Accept(IVisitor visitor) => visitor.Visit(this);

    }
}