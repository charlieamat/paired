using Zenject;

namespace Paired.Scripts.Game
{
    public class GameSignals
    {
        public class GameOver : Signal 
        {
            public class Trigger : TriggerBase
            {
            }
        }
    }
}
