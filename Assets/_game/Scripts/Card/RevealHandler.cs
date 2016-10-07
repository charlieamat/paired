using UnityEngine;

namespace Paired.Scripts.Card
{
    public class RevealHandler
    {
        private CardModel _model;
        private int _revealParameterId;

        public RevealHandler(CardModel model, string revealParameterName)
        {
            _model = model;
            _revealParameterId = Animator.StringToHash(revealParameterName);
        }

        public void Reveal()
        {
            _model.Animator.SetBool(_revealParameterId, true);
        }
    }
}
