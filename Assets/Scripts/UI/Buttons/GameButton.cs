using UnityEngine;
using UnityEngine.EventSystems;
using PenguinPlunge.Core;
using Sirenix.OdinInspector;
using UnityEngine.Events;

namespace PenguinPlunge.UI
{
    public class GameButton : UIButton
    {
        [SerializeField, EnumToggleButtons, HideLabel, Title("Event to Trigger", TitleAlignment = TitleAlignments.Centered)]
        private GameEventType eventToTrigger;

        [SerializeField, HideLabel, Title("Unity Event", TitleAlignment = TitleAlignments.Centered)]
        private UnityEvent onClicked;

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            GameEvent.Trigger(eventToTrigger);
            onClicked?.Invoke();
        }
    }
}
