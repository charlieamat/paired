using Paired.Scripts.Misc;
using Paired.Scripts.Network;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Paired.Scripts.Lobby
{
    public class LobbyModel
    {
        [InjectOptional]
        public LobbyState State { get; private set; }

        [Inject]
        public NetworkSignals.MatchList MatchListSignal { get; private set; }

        [Inject]
        public NetworkSignals.MatchCreate MatchCreateSignal { get; private set; }

        [Inject]
        public NetworkSignals.MatchJoined MatchJoinedSignal { get; private set; }

        [Inject(Id = SceneType.Main)]
        public string MainSceneName;

        [Inject(Id = LobbyUI.ConnectionStatusPanel)]
        public Animator ConnectionStatusPanel { get; private set; }

        [Inject(Id = LobbyUI.ConnectionStatusLabel)]
        public Text ConnectionStatusLabel { get; private set; }

        [Inject(Id = LobbyUI.GameResultsForWinner)]
        public Animator GameResultsForWinnerPanel { get; private set; }

        [Inject(Id = LobbyUI.GameResultsForLoser)]
        public Animator GameResultsForLoserPanel { get; private set; }

        [Inject(Id = LobbyUI.CancelButton)]
        public Button CancelButton { get; private set; }

        [Inject(Id = LobbyUI.CancelButtonLabel)]
        public Text CancelButtonLabel { get; private set; }
    }
}