using UnityEditor;
using UnityEngine;

public class PlayerPrefsManagerEditor : EditorWindow
{
    [MenuItem("Tools/PlayerPrefs Manager")]
    public static void ShowWindow()
    {
        GetWindow<PlayerPrefsManagerEditor>("PlayerPrefs Manager");
    }

    private void OnGUI()
    {
        GUILayout.Label("PlayerPrefs Manager", EditorStyles.boldLabel);

        if (GUILayout.Button("Jugar Tutorial"))
        {
            DeletePlayerPrefs();
        }
    }

    private void DeletePlayerPrefs()
    {
        if (EditorUtility.DisplayDialog("Confirmación", "¿Estás seguro de que deseas jugar el tutorial?", "Sí", "No"))
        {
            PlayerPrefs.DeleteKey("IndexTutorial");
            PlayerPrefs.Save();
            EditorUtility.DisplayDialog("Éxito", "Ahora podrás jugar el tutorial", "OK");
        }
    }
}
