using Paired.Scripts.Network;
using Paired.Scripts.UI;
using Paired.Scripts.Game;
using UnityEngine;
using Zenject;
using System;
using System.Collections.Generic;
using UnityEngine.Networking.Match;
using UnityEngine.SceneManagement;

namespace Paired.Scripts.Lobby
{
    public class LobbyController : IInitializable, ITickable, IDisposable
    {
        private LobbyModel _model;
        private ScreenManager _screenManager;
        private NetworkGameService _networkGameService;
        
        public LobbyController(LobbyModel model, ScreenManager screenManager, NetworkGameService networkGameService)
        {
            _model = model;
            _screenManager = screenManager;
            _networkGameService = networkGameService;
        }

        public void Initialize()
        {
            switch (_model.State)
            {
                case LobbyState.Cancelled:
                    LeaveLobby();
                    break;
                case LobbyState.Connecting:
                    _screenManager.OpenPanel(_model.ConnectionStatusPanel, _networkGameService.StartGame);
                    break;
                case LobbyState.GameResult_Won:
                    _screenManager.OpenPanel(_model.GameResultsForWinnerPanel);
                    break;
                case LobbyState.GameResult_Lost:
                    _screenManager.OpenPanel(_model.GameResultsForLoserPanel);
                    break;
            }

            if (GameObject.FindObjectOfType<GameResults>())
            {
                GameObject.Destroy(GameObject.FindObjectOfType<GameResults>().gameObject);
            }

            _model.CancelButton.onClick.AddListener(LeaveLobby);

            _model.MatchListSignal.Event += OnMatchList;
            _model.MatchCreateSignal.Event += OnMatchCreate;
            _model.MatchJoinedSignal.Event += OnMatchJoined;
        }

        public void Tick()
        {
            if (_model.State != LobbyState.Connecting && Input.GetMouseButtonUp(0))
            {
                LeaveLobby();
            }
        }

        private void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
        {
            _model.CancelButton.onClick.AddListener(_networkGameService.Disconnect);
        }

        private void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchList)
        {
            _model.CancelButton.onClick.RemoveListener(LeaveLobby);
        }

        private void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchList)
        {
            _model.CancelButton.onClick.RemoveListener(LeaveLobby);
        }

        private void LeaveLobby()
        {
            SceneManager.LoadScene(_model.MainSceneName);
        }

        public void Dispose()
        {
            _model.MatchListSignal.Event -= OnMatchList;
            _model.MatchCreateSignal.Event -= OnMatchCreate;
            _model.MatchJoinedSignal.Event -= OnMatchJoined;
        }
    }
}