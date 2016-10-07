using Paired.Scripts.Lobby;
using Zenject;

namespace Paired.Scripts.Main
{
    public class MainController : IInitializable
    {
        private MainModel _model;
        private ZenjectSceneLoader _sceneLoader;

        public MainController(MainModel model, ZenjectSceneLoader sceneLoader)
        {
            _model = model;
            _sceneLoader = sceneLoader;
        }

        public void Initialize()
        {
            _model.PlayButton.onClick.AddListener(EnterLobby);
        }

        private void EnterLobby()
        {
            _sceneLoader.LoadScene(_model.LobbySceneName, InstallLobbyBindings);
        }

        private void InstallLobbyBindings(DiContainer container)
        {
            container.BindInstance(LobbyState.Connecting);
        }
    }
}