using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace Paired.Scripts.Game
{
    public class NetworkGameOverHandler : NetworkBehaviour
    {
        private GameSignals.GameOver.Trigger _gameOverTrigger;

        [Inject]
        public void Construct(GameSignals.GameOver.Trigger gameOverTrigger)
        {
            _gameOverTrigger = gameOverTrigger;
        }

        public void GameOver(bool isWinner)
        {
            var results = new GameObject().AddComponent<GameResults>();

            results.Construct(isWinner ? GameResult.Won : GameResult.Lost);

            DontDestroyOnLoad(results.gameObject);

            CmdGameOver();
        }

        [Command]
        public void CmdGameOver()
        {
            _gameOverTrigger.Fire();
        }
    }
}