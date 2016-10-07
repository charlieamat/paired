using Paired.Scripts.Card;
using Paired.Scripts.Game;
using Paired.Scripts.Matching;
using Paired.Scripts.Network;
using Paired.Scripts.Order;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace Paired.Scripts.NetworkPlay
{
    public class NetworkDependencyInjector : MonoBehaviour
    {
        [Inject(Id = "CardCount")]
        private int _cardCount;

        [Inject(Id = "Player 1")]
        private CardHolder _p1CardHolder;

        [Inject(Id = "Player 2")]
        private CardHolder _p2CardHolder;

        [Inject]
        private GameObject _matcherPrefab;

        [Inject]
        private IOrderHolder _orderHolder;

        [Inject]
        private CardCommands.SelectCommand _cardSelectCommand;

        [Inject]
        private GameCommands.GameOver _gameOverCommand;

        [Inject]
        private GameSignals.GameOver.Trigger _gameOverTrigger;


        public void InjectGamePlayer(GameObject gamePlayer)
        {
            Matcher matcher = null;

            if (NetworkServer.active)
            {
                matcher = Instantiate(_matcherPrefab).GetComponent<Matcher>();

                var matchCommand = new MatchCommand();
                matchCommand.Construct(gamePlayer.GetComponent<NetworkMatchHandler>().Match);

                var mismatchCommand = new MismatchCommand();
                mismatchCommand.Construct(gamePlayer.GetComponent<NetworkMismatchHandler>().Mismatch);

                matcher.Construct(_orderHolder, matchCommand, mismatchCommand);

                NetworkServer.Spawn(matcher.gameObject);
            }

            gamePlayer.GetComponent<NetworkSelectHandler>().Construct(matcher, _p1CardHolder, _p2CardHolder);
            gamePlayer.GetComponent<NetworkMatchHandler>().Construct(_cardCount, _gameOverCommand);
            gamePlayer.GetComponent<NetworkMismatchHandler>().Construct(_p1CardHolder, _p2CardHolder);
            gamePlayer.GetComponent<NetworkGameOverHandler>().Construct(_gameOverTrigger);
        }

        public void InjectLocalPlayer(GameObject gamePlayer)
        {
            _cardSelectCommand.Construct(gamePlayer.GetComponent<NetworkSelectHandler>().Select);
            _gameOverCommand.Construct(gamePlayer.GetComponent<NetworkGameOverHandler>().GameOver);
        }
    }
}
