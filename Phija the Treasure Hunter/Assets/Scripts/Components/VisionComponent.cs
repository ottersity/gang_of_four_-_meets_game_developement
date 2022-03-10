using System;
using Components.MovementBehavior;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Components
{
    public class VisionComponent : MonoBehaviour
    {

        [SerializeField] private int visionRange = 1;

        public int VisionRange => visionRange;
        private GameObject _hero;

        private void Start()
        {
            _hero = GameObject.Find("Phija");
        }

        private void Update()
        {
            if (!IsInVision(_hero)) return;
            Chase(_hero);
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

    }
}