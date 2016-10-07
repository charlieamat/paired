using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Paired.Scripts.Card
{
    public class CardClickHandler : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    {
        private CardModel _model;
        private CardCommands.SelectCommand _selectCommand;

        [Inject]
        public void Construct(CardModel model, CardCommands.SelectCommand selectCommand)
        {
            _model = model;
            _selectCommand = selectCommand;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _selectCommand.Execute(_model.Id);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
        }

        public void OnPointerUp(PointerEventData eventData)
        {
        }
    }
}
