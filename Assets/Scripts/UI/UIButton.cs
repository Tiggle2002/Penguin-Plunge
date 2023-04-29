using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PenguinPlunge.UI
{
    public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [FoldoutGroup("Button Feedbacks")]
        [SerializeField] protected MMF_Player pointerEnterFeedback;
        [FoldoutGroup("Button Feedbacks")]
        [SerializeField] protected MMF_Player pointerExitFeedback;
        [FoldoutGroup("Button Feedbacks")]
        [SerializeField] protected MMF_Player pointerClickFeedback;

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            pointerClickFeedback?.PlayFeedbacks();
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            pointerEnterFeedback?.StopFeedbacks();
            pointerExitFeedback?.StopFeedbacks(true);
            pointerEnterFeedback?.PlayFeedbacks();
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            pointerExitFeedback?.StopFeedbacks();
            pointerEnterFeedback?.StopFeedbacks(true);
            pointerExitFeedback?.PlayFeedbacks();
        }
    }
}
