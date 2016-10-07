using UnityEngine;
using Zenject;

namespace Paired.Scripts.Card
{
    public class Card : MonoBehaviour
    {
        private CardModel _model;
        private CardCommands.RevealCommand _revealCommand;
        private CardCommands.HideCommand _hideCommand;

        public int Id
        {
            get
            {
                return _model.Id;
            }
        }

        [Inject]
        public void Construct(CardModel model, CardCommands.RevealCommand revealCommand, CardCommands.HideCommand hideCommand)
        {
            _model = model;
            _revealCommand = revealCommand;
            _hideCommand = hideCommand;
        }

        public void Select()
        {
            _revealCommand.Execute();
        }

        public bool CanSelect()
        {
            return !_model.IsRevealed();
        }

        public void Deselect()
        {
            _hideCommand.Execute();
        }

        public bool CanDeselect()
        {
            return !_model.IsHidden();
        }
    }
}