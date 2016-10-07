using Zenject;

namespace Paired.Scripts.Order
{
    public class OrderSignals
    {
        public class Initialized : Signal<int[]>
        {
            public class Trigger : TriggerBase
            {
            }
        }
    }
}
