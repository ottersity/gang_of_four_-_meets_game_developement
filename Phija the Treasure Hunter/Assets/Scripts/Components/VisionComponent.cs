using System;
using Components.MovementBehavior;
using Pattern.Visitor;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace Components
{
    public class VisionComponent : MonoBehaviour, IElement
    {

        /// <summary>
        /// Informs subscribers if an enemy is in sight.
        /// </summary>
        public event EventHandler<GameObject> EnemySpottedEvent;

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
                EnemySpottedEvent?.Invoke(this, enemyInVision);
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
        
        /// <summary>
        /// Callback for `EnemySpottedEvent` will start chasing the spotted enemy.
        /// </summary>
        /// <param name="sender">Who spotted the enemy</param>
        /// <param name="obj">The spotted enemy</param>
        public void OnEnemySpottedEvent(object sender, GameObject obj) => Chase(obj);

    }
}