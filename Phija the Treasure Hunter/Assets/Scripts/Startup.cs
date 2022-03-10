using System.Collections;
using System.Collections.Generic;
using Components;
using Components.MovementBehavior;
using Scripts.Components;
using UnityEngine;

public class Startup : MonoBehaviour
{
    /// <summary>
    /// Does all actions to get the game started.
    /// Especially adding static game objects like towers and treasure to the scene.
    /// </summary>
    void Start()
    {
        Time.fixedDeltaTime = 1f;

        GameObject treasure = new GameObject("Treasure");
        treasure.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("sprites/Chest");
        treasure.transform.position = new Vector2(0, 4);
        
        GameObject hero = new GameObject("Phija");
        hero.transform.position = new Vector2(-3, -3);
        hero.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("sprites/Phija");
        hero.transform.localScale = new Vector3(4, 4, 0);
        hero.AddComponent<MovementComponent>();
        hero.AddComponent<ClickMovementBehaviorComponent>();
        hero.AddComponent<AttackComponent>().AttackRange = 1;
        hero.GetComponent<AttackComponent>().Team = "HeroTeam";
        hero.AddComponent<HealthComponent>().HealthPoints = 50;

        GameObject tower1 = new GameObject("Tower");
        tower1.transform.position = new Vector2(-3, 0);
        tower1.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("sprites/Tower");
        tower1.AddComponent<VisionComponent>();
        tower1.AddComponent<AttackComponent>().AttackRange = 2;
        tower1.GetComponent<AttackComponent>().Team = "EnemyTeam";
        tower1.AddComponent<HealthComponent>().HealthPoints = 10;

        GameObject tower2 = new GameObject("Tower");
        tower2.transform.position = new Vector2(3, 0);
        tower2.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("sprites/Tower");
        tower2.AddComponent<VisionComponent>();
        tower2.AddComponent<AttackComponent>().AttackRange = 2;
        tower2.GetComponent<AttackComponent>().Team = "EnemyTeam";
        tower2.AddComponent<HealthComponent>().HealthPoints = 10;
        
        for (var i = 0; i < 10; i++)
        {
            var minion = new GameObject("Minion") ;
            minion.transform.position = new Vector2(1, 0);
            minion.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("sprites/Minion");
            minion.transform.localScale = new Vector2(0.5f, 0.5f);
            minion.AddComponent<MovementComponent>();
            minion.AddComponent<RandomMovementBehaviorComponent>();
            minion.AddComponent<VisionComponent>();
            minion.AddComponent<AttackComponent>().AttackRange = 1;
            minion.GetComponent<AttackComponent>().Team = "EnemyTeam";
            minion.AddComponent<HealthComponent>().HealthPoints = 2;
        }
    }

}