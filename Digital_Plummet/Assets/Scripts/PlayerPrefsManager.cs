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

    // M�todo para agregar un nuevo PlayerPref y actualizar la lista de claves
    public void SetPlayerPref(string key, int value)
    {
        // Guardar el PlayerPref
        PlayerPrefs.SetInt(key, value);

        // A�adir la clave a la lista
        AddKeyToList(key);
        Debug.Log("Key: "+ key);

        // Guardar los cambios
        PlayerPrefs.Save();
    }

    // M�todo para a�adir una clave a la lista
    private void AddKeyToList(string key)
    {
        // Obtener la lista actual de claves
        List<string> keysList = GetKeysList();

        // Si la clave no est� en la lista, a�adirla
        if (!keysList.Contains(key))
        {
            keysList.Add(key);
            SaveKeysList(keysList);
        }
    }

    // M�todo para obtener la lista de claves
    private List<string> GetKeysList()
    {
        string savedKeys = PlayerPrefs.GetString(keysListKey, "");
        List<string> keysList = new List<string>(savedKeys.Split(','));

        // Remover entradas vac�as (si existen)
        keysList.RemoveAll(string.IsNullOrEmpty);

        return keysList;
    }

    // M�todo para guardar la lista de claves en PlayerPrefs
    private void SaveKeysList(List<string> keysList)
    {
        string keysString = string.Join(",", keysList);
        PlayerPrefs.SetString(keysListKey, keysString);
    }

    // M�todo para borrar todas las claves de la lista
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
