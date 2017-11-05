using UnityEngine;
using UnityEngine.UI;

namespace Gui
{
    public class Gui : MonoBehaviour
    {
        public Text Score;
        public Text Health;
        public Text Special;

        private void Start()
        {
            Game.Bind(Game.PlayerHealthUpdate, OnPlayerHealthUpdate);
            Special.text = "None";
            Score.text = "None";
        }

        private void OnDestroy()
        {
            Game.Unbind(Game.PlayerHealthUpdate, OnPlayerHealthUpdate);
        }

        private void OnPlayerHealthUpdate(GameObject entity)
        {
            var player = entity.GetComponent<Player.Player>();
            Health.text = player.Health.ToString();
        }
    }
}