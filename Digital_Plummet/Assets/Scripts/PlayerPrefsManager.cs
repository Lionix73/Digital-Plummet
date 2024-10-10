using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    // Singleton instance
    public static PlayerPrefsManager Instance { get; private set; }

    // Clave para almacenar la lista de keys
    private const string keysListKey = "PlayerPrefsKeys";

    private void Awake()
    {
        // Verificar si ya existe una instancia del singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // No destruir al cargar una nueva escena
        }
        else
        {
            Destroy(gameObject); // Destruir duplicados si ya existe uno
        }
    }

    // Método para agregar un nuevo PlayerPref y actualizar la lista de claves
    public void SetPlayerPref(string key, int value)
    {
        // Guardar el PlayerPref
        PlayerPrefs.SetInt(key, value);

        // Añadir la clave a la lista
        AddKeyToList(key);
        Debug.Log("Key: "+ key);

        // Guardar los cambios
        PlayerPrefs.Save();
    }

    // Método para añadir una clave a la lista
    private void AddKeyToList(string key)
    {
        // Obtener la lista actual de claves
        List<string> keysList = GetKeysList();

        // Si la clave no está en la lista, añadirla
        if (!keysList.Contains(key))
        {
            keysList.Add(key);
            SaveKeysList(keysList);
        }
    }

    // Método para obtener la lista de claves
    private List<string> GetKeysList()
    {
        string savedKeys = PlayerPrefs.GetString(keysListKey, "");
        List<string> keysList = new List<string>(savedKeys.Split(','));

        // Remover entradas vacías (si existen)
        keysList.RemoveAll(string.IsNullOrEmpty);

        return keysList;
    }

    // Método para guardar la lista de claves en PlayerPrefs
    private void SaveKeysList(List<string> keysList)
    {
        string keysString = string.Join(",", keysList);
        PlayerPrefs.SetString(keysListKey, keysString);
    }

    // Método para borrar todas las claves de la lista
    public void DeleteAllStoredPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        /*
        // Obtener la lista de claves
        List<string> keysList = GetKeysList();

        // Recorrer y borrar cada PlayerPref
        foreach (string key in keysList)
        {
            Debug.Log("Key: "+key);
            PlayerPrefs.DeleteKey(key);
        }

        // Vaciar la lista de claves
        PlayerPrefs.DeleteKey(keysListKey);

        // Guardar los cambios
        PlayerPrefs.Save();*/
    }
}
