using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace Paired.Scripts.NetworkPlay
{
    public class GamePlayerInitializer : NetworkBehaviour
    {
        private NetworkPlayModel _model;

        private void Awake()
        {
            _model = FindObjectOfType<NetworkPlayModel>();

            if (PlaySceneLoaded)
            {
                FindObjectOfType<NetworkDependencyInjector>().InjectGamePlayer(gameObject);
            }
            else
            {
                SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            }
        }

        private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == _model.PlaySceneName)
            {
                FindObjectOfType<NetworkDependencyInjector>().InjectGamePlayer(gameObject);
            }
        }

        public override void OnStartLocalPlayer()
        {
            StartCoroutine(InjectWhenPlaySceneLoaded());
        }

        private IEnumerator InjectWhenPlaySceneLoaded()
        {
            yield return new WaitUntil(IsPlaySceneLoaded);

            FindObjectOfType<NetworkDependencyInjector>().InjectLocalPlayer(gameObject);
        }

        private bool PlaySceneLoaded
        {
            get
            {
                return SceneManager.GetSceneByName(_model.PlaySceneName).isLoaded;
            }
        }

        private bool IsPlaySceneLoaded()
        {
            return PlaySceneLoaded;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        }
    }
}