using UnityEngine;
using Zenject;
using Paired.Scripts.Lobby;
using Paired.Scripts.Game;
using UnityEngine.UI;

public class LobbyInstaller : MonoInstaller<LobbyInstaller>
{
    public Animator ConnectionStatusPanel;
    public Text ConnectionStatusLabel;
    public Animator GameResultsForWinnerPanel;
    public Animator GameResultsForLoserPanel;
    public Button CancelButton;
    public Text CancelButtonLabel;

    public override void InstallBindings()
    {
        Container.BindAllInterfaces<LobbyController>().To<LobbyController>().AsSingle();

        Container.BindInstance(ConnectionStatusPanel).WithId(LobbyUI.ConnectionStatusPanel);
        Container.BindInstance(ConnectionStatusLabel).WithId(LobbyUI.ConnectionStatusLabel);
        Container.BindInstance(GameResultsForWinnerPanel).WithId(LobbyUI.GameResultsForWinner);
        Container.BindInstance(GameResultsForLoserPanel).WithId(LobbyUI.GameResultsForLoser);
        Container.BindInstance(CancelButton).WithId(LobbyUI.CancelButton);
        Container.BindInstance(CancelButtonLabel).WithId(LobbyUI.CancelButtonLabel);

        Container.Bind<LobbyModel>();

        if (FindObjectOfType<GameResults>() != null)
        {
            Container.Unbind<LobbyState>();

            switch (FindObjectOfType<GameResults>().Result)
            {
                case GameResult.Won:
                    Container.BindInstance(LobbyState.GameResult_Won);
                    break;
                case GameResult.Lost:
                    Container.BindInstance(LobbyState.GameResult_Lost);
                    break;
            }
        }
    }
}
