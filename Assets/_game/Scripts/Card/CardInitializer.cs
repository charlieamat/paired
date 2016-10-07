using UnityEngine;
using Zenject;
using Paired.Scripts.Order;

namespace Paired.Scripts.Card
{
    public class CardInitializer : IInitializable
    {
        private int _id;
        private Deck _deck;
        private MeshRenderer _renderer;
        private OrderSignals.Initialized _orderInitializedSignal;

        public CardInitializer(int id, Deck deck, MeshRenderer renderer, OrderSignals.Initialized orderInitializedSignal)
        {
            _id = id;
            _deck = deck;
            _renderer = renderer;
            _orderInitializedSignal = orderInitializedSignal;
        }

        public void Initialize()
        {
            _orderInitializedSignal.Event += OnOrderInitialized;
        }

        private void OnOrderInitialized(int[] order)
        {
            var orderId = order[_id];
            var texture = _deck.Textures[orderId];

            _renderer.material.mainTexture = texture;
        }
    }
}