using Actors.InputThings;
using UnityEngine;

namespace Actors
{
    public class ActorAnimations : ActorSystem
    {
        private Animator _animator;
        private static readonly int AnimPlayerMove = Animator.StringToHash("AminPlayerMove");

        protected override void Awake()
        {
            base.Awake();
            _animator = GetComponent<Animator>();
        }

        public void Update()
        {
            Animate();
        }

        private void Animate()
        {
            var direction = 0;

            if (ActorInput.Movement.x > 0)
                direction = 4;
            else if (ActorInput.Movement.x < 0)
                direction = 3;
            else if (ActorInput.Movement.y > 0)
                direction = 2;
            else if (ActorInput.Movement.y < 0)
                direction = 1;

            _animator.SetInteger(AnimPlayerMove, direction);
        }
    }
}