using Paired.Scripts.Matching;
using UnityEngine.Networking;

namespace Paired.Scripts.Card
{
    public class NetworkSelectHandler : NetworkBehaviour
    {
        private Matcher _matcher;
        private CardHolder _p1CardHolder;
        private CardHolder _p2CardHolder;

        public void Construct(Matcher matcher, CardHolder p1CardHolder, CardHolder p2CardHolder)
        {
            _matcher = matcher;
            _p1CardHolder = p1CardHolder;
            _p2CardHolder = p2CardHolder;
        }

        public void Select(int id)
        {
            if (_p1CardHolder.CanSelect(id))
            {
                CmdSelectCard(id);
            } 
        }

        [Command]
        private void CmdSelectCard(int id)
        {
            _matcher.SelectCard(id);

            RpcSelectCard(id);
        }

        [ClientRpc]
        private void RpcSelectCard(int id)
        {
            var cardHolder = isLocalPlayer ? _p1CardHolder : _p2CardHolder;

            cardHolder.Select(id);
        }
    }
}
