using UnityEngine;
using Zenject;

namespace Paired.Scripts.Card
{
    public class CardModel
    {
        [Inject]
        public int Id { get; private set; }

        [Inject]
        public MeshRenderer Renderer { get; private set; }

        [Inject]
        public Animator Animator;

        [Inject(Id = CardAnimationState.Default)]
        private string _defaultStateName;

        [Inject (Id = CardAnimationState.Revealed)]
        private string _revealedStateName;

        [Inject(Id = CardAnimationState.Hidden)]
        private string _hiddenStateName;

        public bool IsRevealed()
        {
            var state = Animator.GetCurrentAnimatorStateInfo(0);
            return state.IsName(_revealedStateName) || (state.IsName(_hiddenStateName) && state.normalizedTime < 1);
        }

        public bool IsHidden()
        {
            var state = Animator.GetCurrentAnimatorStateInfo(0);
            return state.IsName(_hiddenStateName) || state.IsName(_defaultStateName) ||
                (state.IsName(_revealedStateName) && state.normalizedTime < 1);
        }
    }
}
