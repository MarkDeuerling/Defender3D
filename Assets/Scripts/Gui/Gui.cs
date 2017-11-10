using UnityEngine;
using UnityEngine.UI;

namespace Gui
{
    public class Gui : MonoBehaviour
    {
        public Text Score;
        public Text Health;
        public Text Special;

        private int score;

        private void Start()
        {
            Game.Bind(Game.PlayerHealthUpdate, OnPlayerHealthUpdate);
            Game.Bind(Game.ScoreUpdate, OnScoreUpdate);
            Game.Bind(Game.SpecialUpdate, OnSpecialUpdate);
            Score.text = score.ToString();
        }

        private void OnDestroy()
        {
            Game.Unbind(Game.PlayerHealthUpdate, OnPlayerHealthUpdate);
            Game.Unbind(Game.ScoreUpdate, OnScoreUpdate);
            Game.Unbind(Game.SpecialUpdate, OnSpecialUpdate);
        }

        private void OnPlayerHealthUpdate(GameObject entity)
        {
            var player = entity.GetComponent<Player.Player>();
            Health.text = player.Health.ToString();
        }

        private void OnScoreUpdate(GameObject entity)
        {
            Score.text = (++score).ToString();
        }

        private void OnSpecialUpdate(GameObject entity)
        {
            var player = entity.GetComponent<Player.Player>();
            Special.text = player.SpecialAtk.UseCount.ToString();
        }
    }
}