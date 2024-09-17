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
        // Botón para eliminar todos los PlayerPrefs
        if (GUILayout.Button("Eliminar todos los PlayerPrefs"))
        {
            DeleteAllPlayerPrefs();
        }

    }

    private void RestartTutorial()
    {
        if (EditorUtility.DisplayDialog("Confirmación", "¿Estás seguro de que deseas jugar el tutorial?", "Sí", "No"))
        {
            PlayerPrefs.DeleteKey("IndexTutorial");
            PlayerPrefs.Save();
            EditorUtility.DisplayDialog("Éxito", "Ahora podrás jugar el tutorial", "OK");
        }
    }

    private void DeleteAllPlayerPrefs()
    {
        if (EditorUtility.DisplayDialog("Confirmación", "¿Estás seguro de que deseas eliminar todos los PlayerPrefs?", "Sí", "No"))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            EditorUtility.DisplayDialog("Éxito", "Todos los PlayerPrefs han sido eliminados.", "OK");
        }
    }
}
