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
            EditorSceneManager.OpenScene("Assets/Scenes/Player.unity");
            EditorSceneManager.OpenScene("Assets/Scenes/Environment.unity", OpenSceneMode.Additive);
            EditorSceneManager.OpenScene("Assets/Scenes/Camera.unity", OpenSceneMode.Additive);
        }

        [MenuItem("Scenes/Wave1")]
        private static void OpenWave1()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Wave1.unity", OpenSceneMode.Additive);
        }
        
        [MenuItem("Scenes/Wave2")]
        private static void OpenWave2()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Wave2.unity", OpenSceneMode.Additive);
        }
        
        [MenuItem("Scenes/Wave3")]
        private static void OpenWave3()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Wave3.unity", OpenSceneMode.Additive);
        }
        
        [MenuItem("Scenes/Boss")]
        private static void OpenBoss()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/BossLevel.unity", OpenSceneMode.Additive);
        }
    }
}
