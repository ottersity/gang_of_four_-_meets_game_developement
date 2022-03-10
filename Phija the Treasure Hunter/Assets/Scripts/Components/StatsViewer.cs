using Scripts.Components;
using UnityEngine;

namespace Components
{
    /// <summary>
    /// Pretty dirty code just for debugging purpose.
    /// </summary>
    public class StatsViewer: MonoBehaviour
    {
        private void OnGUI()
        {
            GUI.Label(
                new Rect(10, 0, 200, 20),
                "Phija HP: " + GetComponent<HealthComponent>().HealthPoints
            );
            
            GUI.Label(
                new Rect(10, 20, 200, 20),
                "Phija Attack Range: " + GetComponent<AttackComponent>().AttackRange
            );
            
            GUI.Label(
                new Rect(10, 40, 200, 20),
                "Phija Attack Power: " + GetComponent<AttackComponent>().AttackPower
            );
        }

    }
}