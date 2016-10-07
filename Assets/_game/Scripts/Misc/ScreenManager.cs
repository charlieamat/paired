using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

namespace Paired.Scripts.UI
{
    public class ScreenManager : MonoBehaviour
    {
        private Animator _open;
        private int _openParameterId;
        private GameObject _previouslySelected;

        private const string _openTransitionName = "Open";
        private const string _closedStateName = "Closed";

        private void Awake()
        {
            SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
        }

        private void SceneManager_sceneUnloaded(Scene arg0)
        {
            _open = null;
            _previouslySelected = null;
        }

        public void OnEnable()
        {
            _openParameterId = Animator.StringToHash(_openTransitionName);
        }

        public void OpenPanel(Animator animator, Action openCallback = null)
        {
            if (_open == animator)
            {
                return;
            }

            animator.gameObject.SetActive(true);

            var newPreviouslySelected = EventSystem.current.currentSelectedGameObject;

            animator.transform.SetAsLastSibling();

            CloseCurrent();

            _previouslySelected = newPreviouslySelected;

            _open = animator;
            _open.SetBool(_openParameterId, true);

            StartCoroutine(WaitForPanelToOpen(_open, openCallback));

            SetSelected(FindFirstEnabledSelectalbe(animator.gameObject));
        }

        public void CloseCurrent()
        {
            if (_open == null)
            {
                return;
            }

            _open.SetBool(_openParameterId, false);

            SetSelected(_previouslySelected);

            StartCoroutine(DisablePanelDelayed(_open));

            _open = null;
        }

        private IEnumerator WaitForPanelToOpen(Animator animator, Action openCallback = null)
        {
            bool openStateReached = false;
            while (!openStateReached)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= animator.GetCurrentAnimatorStateInfo(0).length)
                {
                    openStateReached = animator.GetCurrentAnimatorStateInfo(0).IsName(_openTransitionName);
                }

                yield return new WaitForEndOfFrame(); 
            }

            if (openCallback != null)
                openCallback();
        }

        private IEnumerator DisablePanelDelayed(Animator animator)
        {
            bool closedStateReached = false;
            bool wantToClose = true;
            while (!closedStateReached && wantToClose)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= animator.GetCurrentAnimatorStateInfo(0).length)
                {
                    closedStateReached = animator.GetCurrentAnimatorStateInfo(0).IsName(_closedStateName);
                }

                wantToClose = !animator.GetBool(_openParameterId);

                yield return new WaitForEndOfFrame();
            }

            if (wantToClose)
            {
                animator.gameObject.SetActive(false);
            }
        }

        private void SetSelected(GameObject go)
        {
            EventSystem.current.SetSelectedGameObject(go);

            var standaloneInputModule = EventSystem.current.currentInputModule as StandaloneInputModule;
            if (standaloneInputModule != null)
            {
                return;
            }
            EventSystem.current.SetSelectedGameObject(null);
        }

        private static GameObject FindFirstEnabledSelectalbe(GameObject gameObject)
        {
            GameObject go = null;
            var selectables = gameObject.GetComponentsInChildren<Selectable>(true);
            foreach (var selectable in selectables)
            {
                if (selectable.IsActive() && selectable.IsInteractable())
                {
                    go = selectable.gameObject;
                    break;
                }
            }
            return go;
        }
    }

    public enum GamePanel
    {
        ConnectionStatus,
        GameResults
    }
}