using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using Zenject;

namespace Paired.Scripts.Network
{
    public class CustomNetworkManager : NetworkLobbyManager
    {
        private NetworkSignals.MatchList.Trigger _matchListTrigger;
        private NetworkSignals.MatchCreate.Trigger _matchCreateTrigger;
        private NetworkSignals.MatchJoined.Trigger _matchJoinedTrigger;
        private NetworkSignals.DestroyMatch.Trigger _destroyMatchTrigger;

        [Inject]
        public void Construct(NetworkSignals.MatchList.Trigger matchListTrigger,
            NetworkSignals.MatchCreate.Trigger matchCreateTrigger,
            NetworkSignals.MatchJoined.Trigger matchJoinedTrigger,
            NetworkSignals.DestroyMatch.Trigger destroyMatchTrigger)
        {
            _matchListTrigger = matchListTrigger;
            _matchCreateTrigger = matchCreateTrigger;
            _matchJoinedTrigger = matchJoinedTrigger;
            _destroyMatchTrigger = destroyMatchTrigger;
        }

        public override void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
        {
            base.OnMatchList(success, extendedInfo, matchList);
            _matchListTrigger.Fire(success, extendedInfo, matchList);
            if (!success) Debug.Log(extendedInfo, this);
        }

        public override void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
        {
            base.OnMatchCreate(success, extendedInfo, matchInfo);
            _matchCreateTrigger.Fire(success, extendedInfo, matchInfo);
            if (!success) Debug.Log(extendedInfo, this);
        }

        public override void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo)
        {
            base.OnMatchJoined(success, extendedInfo, matchInfo);
            _matchJoinedTrigger.Fire(success, extendedInfo, matchInfo);
            if (!success) Debug.Log(extendedInfo, this);
        }

        public override void OnDestroyMatch(bool success, string extendedInfo)
        {
            base.OnDestroyMatch(success, extendedInfo);
            _destroyMatchTrigger.Fire(success, extendedInfo);
            if (!success) Debug.Log(extendedInfo, this);
        }
    }
}