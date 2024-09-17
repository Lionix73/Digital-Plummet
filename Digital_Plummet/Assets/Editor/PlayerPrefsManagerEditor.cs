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
            RestartTutorial(); 
        }
        // Bot�n para eliminar todos los PlayerPrefs
        if (GUILayout.Button("Eliminar todos los PlayerPrefs"))
        {
            DeleteAllPlayerPrefs();
        }

    }

    private void RestartTutorial()
    {
        if (EditorUtility.DisplayDialog("Confirmaci�n", "�Est�s seguro de que deseas jugar el tutorial?", "S�", "No"))
        {
            PlayerPrefs.DeleteKey("IndexTutorial");
            PlayerPrefs.Save();
            EditorUtility.DisplayDialog("�xito", "Ahora podr�s jugar el tutorial", "OK");
        }
    }

    private void DeleteAllPlayerPrefs()
    {
        if (EditorUtility.DisplayDialog("Confirmaci�n", "�Est�s seguro de que deseas eliminar todos los PlayerPrefs?", "S�", "No"))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            EditorUtility.DisplayDialog("�xito", "Todos los PlayerPrefs han sido eliminados.", "OK");
        }
    }
}
