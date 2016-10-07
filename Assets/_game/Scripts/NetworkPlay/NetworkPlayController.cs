using Paired.Scripts.Network;
using Zenject;

namespace Paired.Scripts.NetworkPlay
{
    public class NetworkPlayController : IInitializable
    {
        private NetworkPlayModel _model;
        private NetworkGameService _networkGameService;

        private bool _disconnect;

        public NetworkPlayController(NetworkPlayModel model, NetworkGameService networkGameService)
        {
            _model = model;
            _networkGameService = networkGameService;
        }

        public void Initialize()
        {
            _model.GameOverSignal.Event += OnGameOver;
        }

        private void OnGameOver()
        {
            if (_disconnect)
            {
                _networkGameService.Disconnect();
            }
            else
            {
                _disconnect = true;
            }
        }
    }
}
