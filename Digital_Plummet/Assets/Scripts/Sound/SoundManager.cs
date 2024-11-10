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

        optionsBehav = FindObjectOfType<OptionsBehav>();
        audioListener = FindObjectOfType<AudioListener>();
        UpdateAudioListener();
    }

    // Método que se ejecuta cada vez que una nueva escena es cargada
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Buscar un nuevo AudioListener y actualizar su estado de silencio
        audioListener = FindObjectOfType<AudioListener>();
        UpdateAudioListener();
    }

    public void ToggleMute()
    {
        muteAudio = !muteAudio;
        UpdateAudioListener();
    }

    private void UpdateAudioListener()
    {
        if (muteAudio)
        {
            audioListener.enabled = !muteAudio;
        }
        else
        {
            audioListener.enabled = !muteAudio;
        }
    }

    private void OnDestroy()
    {
        // Desuscribirse del evento al destruir este objeto
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
