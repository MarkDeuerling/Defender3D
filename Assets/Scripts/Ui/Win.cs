using UnityEngine;


using UnityEngine.SceneManagement;


public class Win : MonoBehaviour 
{
	public void OnTitle() 
	{
		SceneManager.LoadScene("Start");
		SceneManager.LoadScene("Environment", LoadSceneMode.Additive);
		SceneManager.LoadScene("Camera", LoadSceneMode.Additive);
	}
}
