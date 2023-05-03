using MoreMountains.Feedbacks;
using PenguinPlunge.Pooling;
using PenguinPlunge.Utility;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PenguinPlunge.Core
{
    public class CollectableFish : MonoBehaviour, IPoolable<CollectableFish>
    {
        public Action<CollectableFish> ReturnObject { get; set; }

        [SerializeField, FoldoutGroup("Component References")]
        private SpriteRenderer spriteRenderer;
        [SerializeField, Title("Collection Feedback", TitleAlignment = TitleAlignments.Centered), HideLabel]
        private MMF_Player collectionFeedback;

        public void OnGet()
        {
            spriteRenderer.enabled = true;
            this.ReturnToPoolOnCondition(CanDespawn);
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                Collect();
            }
        }

        private void Collect()
        {
            spriteRenderer.enabled = false;
            collectionFeedback.PlayFeedbacks();
            int currentCount = PlayerPrefs.GetInt("fishCount");
            PlayerPrefs.SetInt("fishCount", ++currentCount);
        }

        private bool CanDespawn() => !spriteRenderer.VisibleByCamera() && transform.IsLeftOfPlayer();
    }
}
