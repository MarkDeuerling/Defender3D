using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    public Text Score;
    public Text Health;
    public Text Special;
    private Game game;
    
    private void Start()
    {
        game = Game.Singleton;
    }

    private void Update()
    {
        Health.text = game.ThePlayer.Health.ToString();
        Special.text = "None";
        Score.text = "None";
    }
}