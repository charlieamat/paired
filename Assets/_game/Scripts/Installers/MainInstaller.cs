using Paired.Scripts.Main;
using UnityEngine.UI;
using Zenject;

public class MainInstaller : MonoInstaller<MainInstaller>
{
    public Button PlayButton;

    public override void InstallBindings()
    {
        Container.Bind<MainModel>();

        Container.BindInstance(PlayButton).WithId(MainUI.PlayButton);

        Container.BindAllInterfaces<MainController>().To<MainController>();
    }
}