using Unity.VisualScripting;
using UnityEngine;

namespace Components
{
    /// <summary>
    /// Handles the concrete movement to the targetPosition which is given through the MovementBehavior components.
    /// </summary>
    public class MovementComponent : MonoBehaviour
    {

        private Vector2 _targetPosition;
        [field: SerializeField] public int MovementSpeed { get; set; }
        
        private Vector2 TargetPosition
        {
            get => _targetPosition;
            set => _targetPosition = RoundVector(value);
        }

        private void Start()
        {
            TargetPosition = transform.position;
            MovementSpeed = 5;
        }

        /// <summary>
        /// Moves the game object towards the position of targetPosition.
        /// </summary>
        private void Update()
        {
            transform.position =
                Vector2.MoveTowards(
                    transform.position,
                    TargetPosition,
                    Time.deltaTime * MovementSpeed
                );
        }

        /// <summary>
        /// Initializes the movement to a specific ingame point / position.
        /// </summary>
        /// <param name="position"></param>
        public void MoveTo(Vector2 position) => TargetPosition = position;

        /// <summary>
        /// Rounds the x, y values of a vector3 to the next nearest integer.
        /// </summary>
        /// <param name="vector3"></param>
        /// <returns>Vector2 rounded to nearest integer</returns>
        private static Vector2 RoundVector(Vector2 vector3)
        {
            return new Vector2(Mathf.Round(vector3.x), Mathf.Round(vector3.y));
        }
    }
}