using System;
using System.Collections.Generic;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.Types;
using Zenject;

namespace Paired.Scripts.Network
{
    public class NetworkGameService : IInitializable, IDisposable
    {
        private CustomNetworkManager _networkManager;
        private NetworkSignals.MatchList _matchListSignal;
        private NetworkSignals.MatchCreate _matchCreateSignal;
        private NetworkSignals.DestroyMatch _destroyMatchSignal;

        private delegate void DisconnectDelegate();
        private DisconnectDelegate OnDisconnect;

        private ulong _matchId;

        public NetworkGameService(CustomNetworkManager networkManager,
            NetworkSignals.MatchList matchListSignal,
            NetworkSignals.MatchCreate matchCreateSignal,
            NetworkSignals.DestroyMatch destroyMatchSignal)
        {
            _networkManager = networkManager;
            _matchListSignal = matchListSignal;
            _matchCreateSignal = matchCreateSignal;
            _destroyMatchSignal = destroyMatchSignal;
        }

        public void Initialize()
        {
            _matchListSignal.Event += OnMatchList;
            _matchCreateSignal.Event += OnMatchCreate;
            _destroyMatchSignal.Event += OnDestroyMatch;
        }

        public void StartGame()
        {
            _networkManager.StartMatchMaker();
            _networkManager.matchMaker.ListMatches(0, 1, "", true, 0, 0, _networkManager.OnMatchList);
        }

        public void Disconnect()
        {
            OnDisconnect();
        }

        private void StartMatch()
        {
            _networkManager.matchMaker.ListMatches(0, 1, "", true, 0, 0, _networkManager.OnMatchList);
        }

        private void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> responseData)
        {
            if (responseData.Count > 0)
            {
                _networkManager.matchMaker.JoinMatch(responseData[0].networkId, "", "", "", 0, 0, _networkManager.OnMatchJoined);
                OnDisconnect = DisconnectClient;
            }
            else
            {
                _networkManager.matchMaker.CreateMatch("", 2, true, "", "", "", 0, 0, _networkManager.OnMatchCreate);
                OnDisconnect = _networkManager.StopMatchMaker;
            }
        }

        private void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
        {
            _matchId = (ulong)matchInfo.networkId;
            OnDisconnect = DisconnectHost;
        }

        private void DisconnectHost()
        {
            _networkManager.matchMaker.DestroyMatch((NetworkID)_matchId, 0, _networkManager.OnDestroyMatch);
        }

        private void DisconnectClient()
        {
            _networkManager.StopClient();
            _networkManager.StopMatchMaker();
        }

        private void OnDestroyMatch(bool arg1, string arg2)
        {
            _networkManager.StopHost();
            _networkManager.StopMatchMaker();
        }

        public void Dispose()
        {
            _matchListSignal.Event -= OnMatchList;
            _matchCreateSignal.Event -= OnMatchCreate;
            _destroyMatchSignal.Event -= OnDestroyMatch;
        }
    }
}
