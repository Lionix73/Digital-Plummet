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
        if (EditorUtility.DisplayDialog("Confirmaci�n", "�Est�s seguro de que deseas jugar el tutorial?", "S�", "No"))
        {
            PlayerPrefs.DeleteKey("IndexTutorial");
            PlayerPrefs.Save();
            EditorUtility.DisplayDialog("�xito", "Ahora podr�s jugar el tutorial", "OK");
        }
    }
}
