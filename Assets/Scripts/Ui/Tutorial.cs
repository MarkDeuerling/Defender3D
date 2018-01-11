using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ui
{
	public class Tutorial : MonoBehaviour 
	{
		public void OnBack()
		{
			SceneManager.LoadScene("Start");
			SceneManager.LoadScene("Environment", LoadSceneMode.Additive);
			SceneManager.LoadScene("Camera", LoadSceneMode.Additive);
		}
	}
}
