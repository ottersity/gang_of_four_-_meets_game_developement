using UnityEngine;

namespace Scripts.Components
{
    public class HealthComponent : MonoBehaviour
    {

        [SerializeField] private int healthPoints;

        public int HealthPoints
        {
            get => healthPoints;
            set => healthPoints = value;
        }

        /// <summary>
        /// Reduces the hp of the game object by given amount and
        /// let the object die if the hp drops below zero.
        /// </summary>
        /// <param name="amount">How much hp the object should lose</param>
        public void LoseHealth(int amount)
        {
            HealthPoints -= amount;
            if (healthPoints <= 0) Die();
        }

        /// <summary>
        /// Let the object die and simply destroys it... no second chance
        /// for phija.
        /// </summary>
        public void Die()
        {
            Destroy(gameObject);
        }

    }
}