using UnityEditor;
using UnityEditor.SceneManagement;

namespace Editor
{
    public class SceneEditor 
    {
        [MenuItem("Scenes/Game %g")]
        private static void OpenGame()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Game.unity");
        }

        [MenuItem("Scenes/Environment %e")]
        private static void OpenEnvironment()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Game.unity");
            EditorSceneManager.OpenScene("Assets/Scenes/Player.unity", OpenSceneMode.Additive);
            EditorSceneManager.OpenScene("Assets/Scenes/Environment.unity", OpenSceneMode.Additive);
            EditorSceneManager.OpenScene("Assets/Scenes/Camera.unity", OpenSceneMode.Additive);
        }
    }
}
