using Paired.Scripts.Game;
using UnityEngine.Networking;

namespace Paired.Scripts.Matching
{
    public class NetworkMatchHandler : NetworkBehaviour
    {
        private int _cardCount;
        private GameCommands.GameOver _gameOverCommand;

        private int _matchCount;

        public void Construct(int cardCount, GameCommands.GameOver gameOverCommand)
        {
            _cardCount = cardCount;
            _gameOverCommand = gameOverCommand;
        }

        [ServerCallback]
        public void Match()
        {
            _matchCount++;

            RpcMatch(_matchCount == (_cardCount / 2));
        }

        [ClientRpc]
        public void RpcMatch(bool gameOver)
        {
            if (gameOver)
            {
                _gameOverCommand.Execute(isLocalPlayer);
            }
        }
    }
}