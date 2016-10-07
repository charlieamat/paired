using Zenject;

public class DeckInitializer : IInitializable
{
    public Deck Deck { get; private set; }

    public DeckInitializer(Deck deck)
    {
        Deck = deck;
    }

    public void Initialize()
    {
    }
}