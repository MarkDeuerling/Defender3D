using UnityEngine;
using UnityEngine.UI;

namespace GUI
{
    public class Gui : MonoBehaviour
    {
        public Text Score;
        public Text Health;
        public Text Special;

        private void Update()
        {
            Health.text = "None";
            Special.text = "None";
            Score.text = "None";
        }
    }
}