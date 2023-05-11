using UnityEngine;
using System;

namespace PenguinPlunge.Core
{
    public struct GameEvent 
    {
        public GameEventType type;
        
        public GameEvent(GameEventType type) 
        {
            this.type = type; 
        }

        static GameEvent instance;

        public static void Trigger(GameEventType type)
        {
            instance.type = type;

            EventBus.TriggerEvent(instance);
        }
    }

    public enum GameEventType { None, GameStart, GameTogglePause, GameRestart, GameOver }
}