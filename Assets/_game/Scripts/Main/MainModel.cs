using Paired.Scripts.Misc;
using UnityEngine.UI;
using Zenject;

namespace Paired.Scripts.Main
{
    public class MainModel
    {
        [Inject(Id = SceneType.Lobby)]
        public string LobbySceneName { get; private set; }

        [Inject(Id = MainUI.PlayButton)]
        public Button PlayButton { get; private set; }
    }
}