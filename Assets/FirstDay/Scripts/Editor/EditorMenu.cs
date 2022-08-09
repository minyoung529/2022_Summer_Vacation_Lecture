using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class EditorMenu : MonoBehaviour
{
    [MenuItem("TestMenu/Scenes/MainMenuScene")]
    static void EditorMenu_LoadInGame()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/MainMenuScene.unity");
    }

    [MenuItem("TestMenu/Scenes/Reflection")]
    static void EditorMenu_LoadInSecondScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Reflection.unity");
    }

}
