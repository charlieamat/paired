using UnityEngine.Networking;
using Zenject;

namespace Paired.Scripts.Order
{
    public interface IOrderHolder
    {
        int[] Order { get; }
    }

    public class NetworkOrderHolder : NetworkBehaviour, IOrderHolder, IInitializable
    {
        public int[] Order { get; private set; }

        private int _count;
        private OrderFactory _orderFactory;
        private OrderSignals.Initialized.Trigger _orderInitializedTrigger;

        private SyncListInt _syncedOrder = new SyncListInt();

        private void Awake()
        {
            _syncedOrder.Callback += OnOrderSynchronized;
        }

        [Inject]
        public void Construct([Inject(Id ="CardCount")] int count, OrderFactory orderFactory, OrderSignals.Initialized.Trigger orderInitializedTrigger)
        {
            _count = count;
            _orderFactory = orderFactory;
            _orderInitializedTrigger = orderInitializedTrigger;
        }

        [ServerCallback]
        public void Initialize()
        {
            var order = _orderFactory.Create(_count);

            for (int i = 0; i < order.Length; i++)
            {
                _syncedOrder.Add(order[i]);
            }
        }

        public override void OnStartClient()
        {
            if (isServer == false)
            { 
                OrderInitialized();
            }
        }

        private void OnOrderSynchronized(SyncList<int>.Operation op, int itemIndex)
        {
            if (isServer && itemIndex == _count-1) 
            {
                OrderInitialized();
            }
        }

        private void OrderInitialized()
        {
            Order = new int[_count];

            _syncedOrder.CopyTo(Order, 0);

            _orderInitializedTrigger.Fire(Order);
        }
    }
}
