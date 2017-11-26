﻿using GameState;
using UnityEngine;

namespace Gui
{
    public class Pause : MonoBehaviour
    {
        public void OnResume()
        {
            Time.timeScale = 1f;
            Game.UnloadScene(Game.Pause);
            Game.CurrentState = new PlayerState();
        }

        public void OnClose()
        {
            Application.Quit();
        }
    }
}