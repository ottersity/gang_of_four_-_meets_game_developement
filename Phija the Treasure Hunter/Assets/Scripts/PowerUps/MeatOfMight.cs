using Components;
using Pattern.Visitor;
using Scripts.Components;
using UnityEngine;

namespace Powerups
{
    public class MeatOfMight : MonoBehaviour, IVisitor
    {
        /// <summary>
        /// Raises maximum hp by 10 and fully restores health points.
        /// </summary>
        /// <param name="element">The element on which the action should be applied.</param>
        public void Visit(HealthComponent element)
        {
            element.HealthPointsMax += 5;
            element.HealthPoints = element.HealthPointsMax;
        }

        /// <summary>
        /// Raises attack power by 1.
        /// </summary>
        /// <param name="element">The element on which the action should be applied.</param>
        public void Visit(AttackComponent element) => element.AttackPower += 1;

        /// <summary>
        /// Raises vision range by 1.
        /// </summary>
        /// <param name="element">The element on which the action should be applied.</param>
        public void Visit(VisionComponent element) => element.VisionRange += 1;

        private void OnTriggerEnter2D(Collider2D col)
        {
            foreach (var element in col.gameObject.GetComponents<IElement>())
                element.Accept(this);
            Destroy(gameObject);
        }
    }
}