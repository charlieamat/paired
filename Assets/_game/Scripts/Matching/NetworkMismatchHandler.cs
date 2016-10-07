using Paired.Scripts.Card;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Paired.Scripts.Network
{
    public class NetworkMismatchHandler : NetworkBehaviour
    {
        private CardHolder _p1CardHolder;
        private CardHolder _p2CardHolder;

        public void Construct(CardHolder p1CardHolder, CardHolder p2CardHolder)
        {
            _p1CardHolder = p1CardHolder;
            _p2CardHolder = p2CardHolder;
        }

        public void Mismatch(int id1, int id2)
        {
            RpcHandleMismatch(id1, id2);
        }

        [ClientRpc]
        private void RpcHandleMismatch(int id1, int id2)
        {
            StartCoroutine(DoHandleMismatch(id1, id2));
        }

        public IEnumerator DoHandleMismatch(int id1, int id2)
        {
            var cardHolder = isLocalPlayer ? _p1CardHolder : _p2CardHolder;

            while (!(cardHolder.CanDeselect(id1) && cardHolder.CanDeselect(id2)))
            {
                yield return new WaitForEndOfFrame();
            }
            cardHolder.Deselect(id1);
            cardHolder.Deselect(id2);
        }
    }
}
