using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

namespace PenguinPlunge.Core
{
    public class Obstacle : SerializedMonoBehaviour
    {
        public ObstacleType Type => obstacleType;

        [SerializeField, HideLabel, EnumToggleButtons]
        private ObstacleType obstacleType;
        [SerializeField]
        private MMF_Player playerHitFeedback;

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                playerHitFeedback?.PlayFeedbacks();

                ApplyShock(collision.gameObject);
            }
        }

        /// <summary>
        /// Although this solution is simple and limited in its extensibility, it achieves its purpose. If each obstacle had different behaviour, an array of IBehaviour could be held, and each behaviour could be called in a loop.
        /// </summary>
        /// <param name="obj"></param>
        private void ApplyShock(GameObject obj)
        {
            if (obstacleType == ObstacleType.Shark) return;

            if (obj.TryGetComponent(out IGravity IGravity))
            {
                IGravity.SetScale(0f, 2f);
            }
        }
    }
    public enum ObstacleType
    {
        Jellyfish,
        Eel,
        Shark
    }
}