using System;
using Components.MovementBehavior;
using Pattern.Visitor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Components
{
    public class VisionComponent : MonoBehaviour, IElement
    {

        [SerializeField] private int visionRange = 1;

        public int VisionRange
        {
            get => visionRange;
            set => visionRange = value;
        }

        private void Update()
        {
            var enemyInVision = EnemyInVision();
            if (enemyInVision is not null) Chase(enemyInVision);
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

        private bool IsInVision(GameObject @object)
        {
            return Vector2.Distance(transform.position, @object.transform.position) < VisionRange;
        }

        private GameObject EnemyInVision()
        {
            var enemyTeam = CompareTag("EnemyTeam")
                ? GameObject.FindGameObjectsWithTag("HeroTeam")
                : GameObject.FindGameObjectsWithTag("EnemyTeam");

            foreach (var enemy in enemyTeam)
            {
                if (IsInVision(enemy)) return enemy;
            }

            return null;
        }

        public void Accept(IVisitor visitor) => visitor.Visit(this);
    }
}