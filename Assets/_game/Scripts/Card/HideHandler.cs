using UnityEngine;

namespace Paired.Scripts.Card
{
    public class HideHandler
    {
        CardModel _model;
        private int _revealParameterId;

        public HideHandler(CardModel model, string revealParameterName)
        {
            _model = model;
            _revealParameterId = Animator.StringToHash(revealParameterName);
        }

        public void Hide()
        {
            _model.Animator.SetBool(_revealParameterId, false);
        }
    }
}
