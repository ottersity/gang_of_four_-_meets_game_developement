using System;
using System.Collections.Generic;
using Components.MovementBehavior;
using Pattern.Visitor;
using UnityEngine;

namespace Components
{
    public class VisionComponent : MonoBehaviour, IElement, IObservable<GameObject>, IObserver<GameObject>
    {

        private readonly List<IObserver<GameObject>> _observers = new();

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
                foreach (var observer in _observers)
                    observer.OnNext(enemyInVision);

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
        /// Allows observer to subscribe to specific events.
        /// </summary>
        /// <param name="observer">The observer which subscribes</param>
        /// <returns>
        /// IDisposable.Dispose implementation so that the observer can remove itself from the subscribers
        /// collection.
        /// </returns>
        public IDisposable Subscribe(IObserver<GameObject> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);

            return new Unsubscriber(_observers, observer);
        }

        public void OnNext(GameObject value) => Chase(value);
        public void OnCompleted() => throw new NotImplementedException();
        public void OnError(Exception error) => throw new NotImplementedException();

        /// <summary>
        /// Defines an IDisposable implementation that the provider can return to subscribers so that they can stop
        /// receiving notifications at any time.
        /// </summary>
        private class Unsubscriber : IDisposable
        {

            private List<IObserver<GameObject>> _observers;
            private IObserver<GameObject> _observer;

            public Unsubscriber(List<IObserver<GameObject>> observers, IObserver<GameObject> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null) _observers.Remove(_observer);
            }

        }
    }
}