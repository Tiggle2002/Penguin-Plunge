using MoreMountains.Feedbacks;
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
        }
        public IEnumerator SpawnPlayerAfterFeedbacks()
        {
            SpawnPlayer();
 
            yield return spawnFeedback?.PlayFeedbacksCoroutine(transform.position);
        }
        public GameObject SpawnPlayer() => Instantiate(playerPrefab, transform.position, Quaternion.identity);
        public void OnEvent(GameEvent eventData)
        {
            if (eventData.type == GameEventType.GameStarted)
            {
                StartCoroutine(SpawnPlayerAfterFeedbacks());
            }
        }
        public void OnEnable() => this.Subscribe();
        public void OnDisable() => this.Unsubscribe();
    }
}