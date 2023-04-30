using Actors.InputThings.StateMachineThings;
using UnityEngine;

namespace Actors.InputThings.AI
{
    public class AIActorInput : MonoBehaviour, IActorInput
    {
        public Vector2 Movement { get; private set; }
        public Vector3 Look { get; private set; }
        public bool Fire { get; private set; }

        protected readonly StateMachine StateMachine = new();

        protected void SetMovement(Vector2 movement)
        {
            Movement = movement;
        }

        protected void SetLook(Vector3 look)
        {
            Look = look;
        }

        protected void SetFire(bool fire)
        {
            Fire = fire;
        }
    }
}