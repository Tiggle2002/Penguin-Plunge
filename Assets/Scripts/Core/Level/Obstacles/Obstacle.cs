using MoreMountains.Feedbacks;
using System.Collections;
using UnityEngine;

namespace PenguinPlunge.Core
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField]
        private MMF_Player playerHitFeedback;

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                playerHitFeedback?.PlayFeedbacks();
            }
        }
    }
}