using Paired.Scripts.Card;
using Paired.Scripts.Game;
using Paired.Scripts.Matching;
using Paired.Scripts.NetworkPlay;
using Paired.Scripts.Order;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

public class NetworkPlayInstaller : DecoratorInstaller
{
    public GameObject NetworkMatcherPrefab;

    [Inject(Id = "Player 1")]
    private CardHolder _playerCardHolder;

    [InjectOptional]
    private NetworkAutoSolveType _autoSolveType;

    public override void PostInstallBindings()
    {
        Container.BindInstance(NetworkMatcherPrefab);

        Container.BindAllInterfaces<NetworkPlayController>().To<NetworkPlayController>();

        Container.BindCommand<CardCommands.SelectCommand, int>().ToNothing();
        Container.BindCommand<GameCommands.GameOver, bool>().ToNothing();

        if ((_autoSolveType == NetworkAutoSolveType.Server && NetworkServer.active) ||
            (_autoSolveType == NetworkAutoSolveType.Client && !NetworkServer.active))
        {
            Container.BindInstance(_playerCardHolder).WhenInjectedInto<AutoSolver>();
            Container.BindAllInterfaces<AutoSolver>().To<AutoSolver>();
        }
    }
}