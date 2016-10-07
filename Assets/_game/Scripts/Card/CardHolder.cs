using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Paired.Scripts.Card
{
    public class CardHolder : MonoBehaviour
    {
        public List<Card> Cards { get; private set; }

        [Inject]
        public void Construct(List<Card> cards)
        {
            Cards = cards;
        }

        public void Select(int id)
        {
            GetCard(id).Select();
        }

        public bool CanSelect(int id)
        {
            return GetCard(id).CanSelect();
        }

        public void Deselect(int id)
        {
            GetCard(id).Deselect();
        }

        public bool CanDeselect(int id)
        {
            return GetCard(id).CanDeselect();
        }

        private Card GetCard(int id)
        {
            return Cards.Find(c => c.Id == id);
        }
    }
}