using UnityEngine.Networking;

public class CustomLobbyPlayer : NetworkLobbyPlayer
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        SendReadyToBeginMessage();
    }
}