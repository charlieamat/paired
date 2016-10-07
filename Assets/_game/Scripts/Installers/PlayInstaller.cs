using Zenject;
using Paired.Scripts.Order;
using Paired.Scripts.Game;

public class PlayInstaller : MonoInstaller
{
    public Deck Deck;

    private readonly int _cardCount = 16;

    public override void InstallBindings()
    {
        Container.Bind<OrderFactory>();

        Container.BindInstance(_cardCount).WithId("CardCount");
        Container.BindInstance(Deck);

        Container.BindSignal<GameSignals.GameOver>();
        Container.BindSignal<OrderSignals.Initialized>();

        Container.BindTrigger<GameSignals.GameOver.Trigger>();
        Container.BindTrigger<OrderSignals.Initialized.Trigger>();
    }
}