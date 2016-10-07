using System.Collections.Generic;
using UnityEngine.Networking.Match;
using Zenject;

namespace Paired.Scripts.Network
{
    public class NetworkSignals
    {
        public class MatchList : Signal<bool, string, List<MatchInfoSnapshot>>
        {
            public class Trigger : TriggerBase { }
        }

        public class MatchCreate : Signal<bool, string, MatchInfo>
        {
            public class Trigger : TriggerBase { }
        }

        public class MatchJoined : Signal<bool, string, MatchInfo>
        {
            public class Trigger : TriggerBase { }
        }

        public class DestroyMatch : Signal<bool, string>
        {
            public class Trigger : TriggerBase { }
        }
    }
}
