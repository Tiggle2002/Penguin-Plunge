using MoreMountains.Feedbacks;
using PenguinPlunge.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PenguinPlunge.Core
{
    public class GameManager : MonoBehaviour, TEventListener<GameEvent>
    {
        [SerializeField, Required]
        private MMF_Player restartLevel;

        public void OnEvent(GameEvent eventData)
        {
            switch (eventData.type) 
            {
                case GameEventType.GameStart:
                    TimeScale.SetFreezeEnabled(false);
                    break;
                case GameEventType.GameTogglePause:
                    TimeScale.ToggleFreeze();
                    PlayerInput.ToggleInput();
                    break;
                case GameEventType.GameRestart:
                    restartLevel.PlayFeedbacks();
                    break;
            }
        }

        public void OnEnable()
        {
            this.Subscribe();
        }

        public void OnDisable()
        {
            this .Unsubscribe();
        }
    }

    public static class PlayerInput
    {
        public static bool InputEnabled => inputEnabled;
        private static bool inputEnabled = true;

        public static void ToggleInput()
        {
            inputEnabled = !inputEnabled;
        }

        public static void SetInputEnabled(bool enabled)
        {
            inputEnabled = enabled;
        }
    }
}
