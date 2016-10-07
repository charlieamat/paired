using NUnit.Framework;
using NSubstitute;
using Zenject.Tests;

[TestFixture]
public class CardTests : ZenjectUnitTestFixture
{
    /*
    public override void InstallBindings()
    {
        Container.BindInstance(Substitute.For<ISelectHandler>());
        Container.BindInstance(Substitute.For <IDeselectHandler>());
    }

    [Test]
    public void ShouldDelegateSelect()
    {
        var card = getCard();
        var selectHandler = Container.Resolve<ISelectHandler>();

        card.Select();

        selectHandler.Received().Select(card);
    }

    [Test]
    public void ShouldDelegateDeselect()
    {
        var deselectHandler = Container.Resolve<IDeselectHandler>();

        getCard().Deselect();

        deselectHandler.Received().Deselect();
    }

    [Test]
    public void ShouldDelegateCanDeselect()
    {
        var deselectHandler = Container.Resolve<IDeselectHandler>();

        getCard().CanDeselect();

        deselectHandler.Received().CanDeselect();
    }

    [Test]
    public void CanDeselectShouldBeTrue()
    {
        var deselectHandler = Container.Resolve<IDeselectHandler>();
        deselectHandler.ClearReceivedCalls();
        deselectHandler.CanDeselect().Returns(true);

        var canDeselect = getCard().CanDeselect();

        Assert.IsTrue(canDeselect);
    }

    [Test]
    public void CanDeselectShoulBeFalse()
    {
        var deselectHandler = Container.Resolve<IDeselectHandler>();
        deselectHandler.ClearReceivedCalls();
        deselectHandler.CanDeselect().Returns(false);

        var canDeselect = getCard().CanDeselect();

        Assert.IsFalse(canDeselect);
    }

    public Card getCard()
    {
        return Container.Instantiate<Card>(new object[] { 0 });
    }
    */
}