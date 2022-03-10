using Components;
using Scripts.Components;
using UnityEngine;

namespace Powerups
{
    public class MeatOfMightBad : MonoBehaviour
    {

        public void Handle(HealthComponent component)
        {
            component.HealthPointsMax += 5;
            component.HealthPoints = component.HealthPointsMax;
        }

        public void Handle(AttackComponent component)
        {
            component.AttackPower += 1;
        }

        public void Handle(VisionComponent component)
        {
            component.VisionRange += 1;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            Handle(col.GetComponent<HealthComponent>());
            Handle(col.GetComponent<AttackComponent>());
            if (col.TryGetComponent<VisionComponent>(out var visionComponent))
                Handle(visionComponent);
            Destroy(gameObject);
        }

    }
}