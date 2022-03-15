using System;
using System.Collections.Generic;
using Components.MovementBehavior;
using Pattern.Visitor;
using UnityEngine;
using UnityEngine.Events;

namespace Components
{
    public class VisionComponent : MonoBehaviour, IElement
    {

        public UnityEvent<object, GameObject> enemySpottedEvent = new();

        [SerializeField] private int visionRange = 1;

        public int VisionRange
        {
            get => visionRange;
            set => visionRange = value;
        }

        private void Update()
        {
            var enemyInVision = EnemyInVision();
            if (enemyInVision is not null)
            {
                enemySpottedEvent.Invoke(this, enemyInVision);
                Chase(enemyInVision);
            }
        }

        /// <summary>
        /// Swaps the movement behavior to chase the given game object.
        /// </summary>
        /// <param name="object">GameObject which should be followed</param>
        private void Chase(GameObject @object)
        {
            Destroy(GetComponent<MovementBehaviorComponent>());
            gameObject.AddComponent<FollowMovementBehaviorComponent>().FollowTarget = @object;
        }

        /// <summary>
        /// Check if given game object is in vision of the game object to which this component is attached to.
        /// </summary>
        /// <param name="object"></param>
        /// <returns>True if object is in vision otherwise false</returns>
        private bool IsInVision(GameObject @object)
        {
            return Vector2.Distance(transform.position, @object.transform.position) < VisionRange;
        }

        /// <summary>
        /// Searches for enemies in sight and returns the first found enemy.
        /// There is no sorting about distance to the enemy or something.
        /// </summary>
        /// <returns>Enemy which is in sight</returns>
        private GameObject EnemyInVision()
        {
            var enemyTeam = CompareTag("EnemyTeam")
                ? GameObject.FindGameObjectsWithTag("HeroTeam")
                : GameObject.FindGameObjectsWithTag("EnemyTeam");

            foreach (var enemy in enemyTeam)
                if (IsInVision(enemy))
                    return enemy;

            return null;
        }

        public void Accept(IVisitor visitor) => visitor.Visit(this);

        public void OnEnemySpotted(object sender, GameObject enemy) => Chase(enemy);
    }
}