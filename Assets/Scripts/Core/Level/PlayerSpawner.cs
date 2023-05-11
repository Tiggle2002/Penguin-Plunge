using MoreMountains.Feedbacks;
using PenguinPlunge.Utility;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PenguinPlunge.Core
{
    public class PlayerSpawner : MonoBehaviour, TEventListener<GameEvent>
    {
        private GameObject playerPrefab;
        [SerializeField, HideLabel, TitleGroup("Feedback", alignment: TitleAlignments.Centered)]
        private MMF_Player spawnFeedback;

        public void Awake()
        {
            playerPrefab = Resources.Load<GameObject>("RuntimePrefabs/PlayerPrefab");
            Transform transform = SpawnPlayer().transform;
            spawnFeedback.GetFeedbackOfType<MMF_DestinationTransform>().TargetTransform = transform;
            PlayerInput.SetInputEnabled(false);
        }

        public void EnablePlayerAfterFeedbacks()
        {
            EnableInputAfterFeedback().StartAsCoroutine();    

            IEnumerator EnableInputAfterFeedback()
            {
                yield return spawnFeedback.PlayFeedbacksCoroutine(transform.position);
                PlayerInput.SetInputEnabled(true);
            }
        }

        public GameObject SpawnPlayer() => Instantiate(playerPrefab, transform.position, Quaternion.identity);
        public void OnEvent(GameEvent eventData)
        {
            if (eventData.type == GameEventType.GameStart)
            {
                EnablePlayerAfterFeedbacks();
            }
        }
        public void OnEnable() => this.Subscribe();
        public void OnDisable() => this.Unsubscribe();
    }
}