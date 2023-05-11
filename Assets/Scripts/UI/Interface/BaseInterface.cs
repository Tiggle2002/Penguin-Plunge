using MoreMountains.Feedbacks;
using PenguinPlunge.Core;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PenguinPlunge.UI
{
    public class BaseInterface : SerializedMonoBehaviour
    {
        #region Protected Fields
        public bool IsOpen { get { return isOpen; } }
        private bool isOpen = false;

        [Title("Canvas", TitleAlignment = TitleAlignments.Centered), Required, HideLabel, SerializeField]
        private Canvas canvas;
        #endregion

        #region Interface Feedbacks
        [FoldoutGroup("Interface Feedbacks"), SerializeField]
        private MMF_Player interfaceOpened;
        [FoldoutGroup("Interface Feedbacks"), SerializeField]
        private MMF_Player interfaceClosed;
        #endregion

        public void ToggleInterface()
        {
            canvas.enabled = !canvas.enabled;
            isOpen = !isOpen;
            PlayOpenOrClosedFeedback();
        }

        public void OpenInterface()
        {
            canvas.enabled = true;
            isOpen = true;
            PlayOpenOrClosedFeedback();
        }

        public void CloseInterface()
        {
            canvas.enabled = false;
            isOpen = false;
            PlayOpenOrClosedFeedback();
        }

        private void PlayOpenOrClosedFeedback()
        {
            if (isOpen)
            {
                interfaceOpened?.PlayFeedbacks();
            }
            else
            {
                interfaceClosed?.PlayFeedbacks();
            }
        }
    }
}