using UnityEngine;
using Zenject;
using Paired.Scripts.Lobby;
using System;
using UnityEngine.Networking.Match;

public class ConnectionStatusPanel : MonoBehaviour, IInitializable, IDisposable
{
    private LobbyModel _model;

    [Inject]
    public void Construct(LobbyModel model)
    {
        _model = model;
    }

    public void Initialize()
    {
        _model.MatchCreateSignal.Event += OnMatchCreate;
        _model.MatchJoinedSignal.Event += OnMatchJoined;
    }

    private void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        if (success)
        {
            _model.ConnectionStatusLabel.text = "Searching for an opponent";
        }
        else
        {
            _model.ConnectionStatusLabel.text = "An error occurred, please try again later";
        }
    }

    private void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        if (success)
        {
            _model.ConnectionStatusLabel.text = "Opponent found, starting game";
        }
        else
        {
            _model.ConnectionStatusLabel.text = "An error occurred, please try again later";
        }
    }

    public void Dispose()
    {
        _model.MatchCreateSignal.Event -= OnMatchCreate;
        _model.MatchJoinedSignal.Event -= OnMatchJoined;
    }
}
