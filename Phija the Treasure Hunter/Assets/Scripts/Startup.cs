using Components;
using Components.MovementBehavior;
using Scripts.Components;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Startup : MonoBehaviour
{

    /// <summary>
    /// Does all actions to get the game started.
    /// Especially adding static game objects like towers and treasure to the scene.
    /// </summary>
    private void Start()
    {
        Time.fixedDeltaTime = 1f;

        var heroPrefab = PrefabUtility.LoadPrefabContents("Assets/Prefabs/Hero.prefab");
        var hero = Instantiate(heroPrefab);

        var towerPrefab = PrefabUtility.LoadPrefabContents("Assets/Prefabs/Tower.prefab");
        var tower1 = Instantiate(towerPrefab, new Vector2(-3, 0), Quaternion.identity);
        var tower2 = Instantiate(towerPrefab, new Vector2(3, 0), Quaternion.identity);

        var treasurePrefab = PrefabUtility.LoadPrefabContents("Assets/Prefabs/Treasure.prefab");
        var treasure = Instantiate(treasurePrefab);

        var scoutPrefab = PrefabUtility.LoadPrefabContents("Assets/Prefabs/MinionScout.prefab");
        var scouts = new[]{ Instantiate(scoutPrefab), Instantiate(scoutPrefab) };

        var minionPrefab = PrefabUtility.LoadPrefabContents("Assets/Prefabs/Minion.prefab");
        for (var i = 0; i < 10; i++)
        {
            var minion = Instantiate(minionPrefab);
            var scoutVisionComponent = scouts[i % 2].GetComponent<VisionComponent>();
            var minionVisionComponent = minion.GetComponent<VisionComponent>(); 
            scoutVisionComponent.EnemySpottedEvent += minionVisionComponent.OnEnemySpottedEvent;
        } 

    }

}