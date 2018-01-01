using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ui
{
	public class Tutorial : MonoBehaviour 
	{
		public void OnBack()
		{
			SceneManager.LoadScene("Start");
		}
	}
}
