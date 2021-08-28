using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace EventChannels
{
    [CreateAssetMenu(fileName = "newGameEventChannel", menuName = "Events/Game Event Channel")]
    public class GameEventChannelSO : ScriptableObject
    {
        public UnityAction OnGameStarted;
        public UnityAction OnGameWon;
        public UnityAction OnGameLost;
        public UnityAction OnCutsceneStarted;
        public UnityAction OnCutsceneEnded;

        public void StartGame()
        {
            if (OnGameStarted != null)
                OnGameStarted.Invoke();
        }

        public void Win()
        {
            if (OnGameWon != null)
                OnGameWon.Invoke();
        }

        public void Lose()
        {
            if (OnGameLost != null)
                OnGameLost.Invoke();
        }

        public void StartCutscene()
        {
            if (OnCutsceneStarted != null)
                OnCutsceneStarted.Invoke();
        }

        public void EndCutscene()
        {
            if (OnCutsceneEnded != null)
                OnCutsceneEnded.Invoke();
        }
    }
}