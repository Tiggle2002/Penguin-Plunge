using PenguinPlunge.Core;
using PenguinPlunge.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PenguinPlunge.UI
{
    public class StartGameButton : UIButton
    {
        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            GameEvent.Trigger(GameEventType.GameStarted);
            gameObject.SetActive(false);
        }
    }
}