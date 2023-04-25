using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PenguinPlunge.Core
{
    public class LevelScroller : MonoBehaviour, TEventListener<DeathEvent>
    {
        


        public void OnEvent(DeathEvent e)
        {
            //Slow Scrolling
        }
    }
}
