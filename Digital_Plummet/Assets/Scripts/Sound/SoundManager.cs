using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private AudioListener audioListener;
    [SerializeField] AudioMixer masterMixer;
    private bool muteAudio;
    public static SoundManager Instance;
    OptionsBehav optionsBehav;
    public bool IsAudioMuted => muteAudio;

    private void Awake()
    {
        // Configuración del singleton y suscripción al evento de carga de escena
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        //audioListener = FindObjectOfType<AudioListener>();
        UpdateAudioMixer();
    }

    // Método que se ejecuta cada vez que una nueva escena es cargada
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Buscar un nuevo AudioListener y actualizar su estado de silencio
        //audioListener = FindObjectOfType<AudioListener>();
        UpdateAudioMixer();
    }

    public void ToggleMute()
    {
        muteAudio = !muteAudio;
        UpdateAudioMixer();
    }

    private void UpdateAudioMixer()
    {
        if (muteAudio)
        {
            Debug.Log("Sound: Off");
            masterMixer.SetFloat("MasterVolume", -80);
            //audioListener.enabled = !muteAudio;
        }
        else
        {
            Debug.Log("Sound: On");
            masterMixer.SetFloat("MasterVolume", 0);
            //audioListener.enabled = !muteAudio;
        }
    }

    private void OnDestroy()
    {
        // Desuscribirse del evento al destruir este objeto
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
