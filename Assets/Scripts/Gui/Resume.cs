using GameState;
using UnityEngine;

namespace Gui
{
    public class Resume : MonoBehaviour
    {
        public void OnResume()
        {
            Time.timeScale = 1f;
            Game.UnLoadScene(Game.Pause);
            Game.CurrentState = new PlayerState();
        }
    }
}