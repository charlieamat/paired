using Zenject;
using Paired.Scripts.Card;

public class CardInstaller : MonoInstaller
{
    private string _defaultStateName = "Default";
    private string _revealedStateName = "Revealed";
    private string _hiddenStateName = "Hidden";
    private string _revealTransitionParameter = "Revealed";

    public int Id;

    public override void InstallBindings()
    {
        Container.BindInstance(Id);
        Container.BindInstance(_defaultStateName).WithId(CardAnimationState.Default);
        Container.BindInstance(_revealedStateName).WithId(CardAnimationState.Revealed);
        Container.BindInstance(_hiddenStateName).WithId(CardAnimationState.Hidden);
        Container.BindInstance(_revealTransitionParameter).WhenInjectedInto<RevealHandler>();
        Container.BindInstance(_revealTransitionParameter).WhenInjectedInto<HideHandler>();

        Container.Bind<Card>();
        Container.Bind<CardModel>().AsSingle();

        Container.BindAllInterfaces<CardInitializer>().To<CardInitializer>();

        Container.BindCommand<CardCommands.RevealCommand>().To<RevealHandler>(c => c.Reveal);
        Container.BindCommand<CardCommands.HideCommand>().To<HideHandler>(c => c.Hide);
    }
}

public enum CardAnimationState
{
    Default,
    Hidden,
    Revealed
}