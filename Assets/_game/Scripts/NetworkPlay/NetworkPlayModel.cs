using Paired.Scripts.Game;
using Paired.Scripts.Misc;
using UnityEngine;
using Zenject;

namespace Paired.Scripts.NetworkPlay
{
    public class NetworkPlayModel : MonoBehaviour
    {
        [Inject(Id = SceneType.Play)]
        public string PlaySceneName { get; private set; }

        [Inject]
        public GameSignals.GameOver GameOverSignal { get; private set; }
    }
}
