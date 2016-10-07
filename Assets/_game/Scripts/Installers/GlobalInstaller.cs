using Paired.Scripts.Misc;
using Paired.Scripts.Network;
using Paired.Scripts.UI;
using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller<GlobalInstaller>
{
    readonly string _mainSceneName = "Main";
    readonly string _lobbySceneName = "Lobby";
    readonly string _playSceneName = "Play";

    public GameObject NetworkManagerPrefab;
    public NetworkAutoSolveType NetworkAutoSolveType;

    public override void InstallBindings()
    {
        Container.Bind<ScreenManager>().FromGameObject().AsSingle();
        Container.Bind<CustomNetworkManager>().FromPrefab(NetworkManagerPrefab).AsSingle().NonLazy();
        Container.Bind<AsyncProcessor>().FromGameObject().AsSingle().NonLazy();

        Container.BindAllInterfacesAndSelf<NetworkGameService>().To<NetworkGameService>().AsSingle();

        Container.BindInstance(NetworkAutoSolveType);

        Container.BindInstance(_mainSceneName).WithId(SceneType.Main);
        Container.BindInstance(_lobbySceneName).WithId(SceneType.Lobby);
        Container.BindInstance(_playSceneName).WithId(SceneType.Play);
        
        Container.BindSignal<NetworkSignals.MatchList>();
        Container.BindSignal<NetworkSignals.MatchCreate>();
        Container.BindSignal<NetworkSignals.MatchJoined>();
        Container.BindSignal<NetworkSignals.DestroyMatch>();
        
        Container.BindTrigger<NetworkSignals.MatchList.Trigger>();
        Container.BindTrigger<NetworkSignals.MatchCreate.Trigger>();
        Container.BindTrigger<NetworkSignals.MatchJoined.Trigger>();
        Container.BindTrigger<NetworkSignals.DestroyMatch.Trigger>();
    }
}

public enum NetworkAutoSolveType
{
    Disabled,
    Server,
    Client
}