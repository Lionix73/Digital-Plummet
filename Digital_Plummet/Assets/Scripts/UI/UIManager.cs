using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening.Core.Easing;
using System;
using UnityEditor;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] public float fadeTime = 1.0f;
    GameObject backgroundObject;
    [SerializeField] GameObject menu;
    [SerializeField] GameObject buttonsMenu;
    [SerializeField] GameObject bGTimer;
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] bool playMenu;
    private bool gameIsPaused;
    public Canvas myCanvas;
    public CanvasGroup canvasGroup;
    public RectTransform rectTransform;
    private float canvasWidth;
    public List<GameObject> items = new List<GameObject>();
    [SerializeField] TextMeshProUGUI scoreText;
    private GameManager gameManager;
    [SerializeField] FadesAnimation animatedPanel;
    //Respawn
    [SerializeField] private RespawnManager respawnManager;

    private int indexTutorial;

    void Awake(){
        //Fingind RespawnManager
        backgroundObject = GameObject.FindGameObjectWithTag("Background");

        if (respawnManager == null){
            respawnManager = FindObjectOfType<RespawnManager>();
        }
        if (animatedPanel == null)
        {
            animatedPanel = FindObjectOfType<FadesAnimation>();
        }
    }

    public void Start()
    {
        
        menu.SetActive(false);
        if (playMenu )
        {
            countdownText.text = "";
            buttonsMenu.SetActive(false);
        }
        
        gameIsPaused = false;
        // Obtener el componente RectTransform del Canvas
        RectTransform canvasRect = myCanvas.GetComponent<RectTransform>();

        // Obtener el ancho del Canvas
        canvasWidth = canvasRect.rect.width;

        Debug.Log("Ancho del Canvas: " + canvasWidth);
    }


    public void PanelFadeIn()
    {
            Debug.Log("Abrir Menu");
            menu.SetActive(true);
            if (playMenu )
            {
                buttonsMenu.SetActive(true);
            }
            
            canvasGroup.alpha = 0f;
            rectTransform.transform.localPosition = new Vector3(-canvasWidth, 0f, 0f);
        /*if (gameIsPaused == false)
         {
            rectTransform.DOAnchorPos(new Vector2(0f, 0f), fadeTime, false).SetEase(Ease.OutElastic);
            canvasGroup.DOFade(1, fadeTime);
         }
         else
         {
             DOTween.Sequence()
             .Append(rectTransform.DOAnchorPos(new Vector2(0f, 0f), fadeTime, false).SetEase(Ease.OutElastic))
             .Append(canvasGroup.DOFade(1, fadeTime)).OnComplete(PauseGame);
         }*/
        rectTransform.DOAnchorPos(new Vector2(0f, 0f), fadeTime, false).SetEase(Ease.OutElastic).SetUpdate(true);
        canvasGroup.DOFade(1, fadeTime).SetUpdate(true);

        StartCoroutine("ItemsAnimation");
    }

    public void PanelFadeOut()
    {
        //gameIsPaused = false;
        canvasGroup.alpha = 1f;
        rectTransform.transform.localPosition = new Vector3(0f, 0f, 0f);
        rectTransform.DOAnchorPos(new Vector2(canvasWidth, 0f), fadeTime, false).SetEase(Ease.InOutQuint);
        canvasGroup.DOFade(1, fadeTime);
        if (gameIsPaused == true)
        {
            StartCoroutine(StartCountdown());  // Iniciar la cuenta regresiva
        }
        

    }

    public void PanelZoomIn()
    {
        menu.SetActive(true);
        GameObject backgroundObject = GameObject.FindGameObjectWithTag("Background");
        RectTransform rectTransformBG = backgroundObject.GetComponent<RectTransform>();
        CanvasGroup canvasGroupBG = backgroundObject.GetComponent<CanvasGroup>();
        canvasGroupBG.DOFade(1, fadeTime);


        Debug.Log("Abrir Menu");
        if (playMenu)
        {
            buttonsMenu.SetActive(true);
        }

       // canvasGroup.alpha = 0f;
       // rectTransform.transform.localPosition = new Vector3(-canvasWidth, 0f, 0f);

        rectTransformBG.DOScale(1.75f, fadeTime);
       // rectTransform.DOAnchorPos(new Vector2(0f, 0f), fadeTime, false).SetEase(Ease.OutElastic).SetUpdate(true);
        //.DOFade(1, fadeTime).SetUpdate(true);

        StartCoroutine("ItemsAnimation");
    }
    public void PanelZoomOut()
    {
        GameObject backgroundObject = GameObject.FindGameObjectWithTag("Background");
        RectTransform rectTransformBG = backgroundObject.GetComponent<RectTransform>();
        Debug.Log("Cerrar Menu");

        StartCoroutine("ItemsDissapear");
        rectTransformBG.DOScale(1, fadeTime);
        StartCoroutine(DisableMenuAfterFade());
        //StartCoroutine(DisableMenuAfterFade());
        
        // rectTransform.DOAnchorPos(new Vector2(0f, 0f), fadeTime, false).SetEase(Ease.OutElastic).SetUpdate(true);

    }
    private IEnumerator DisableMenuAfterFade()
    {
        // Espera el tiempo de fadeTime
        yield return new WaitForSeconds(fadeTime*2);
        // Desactiva el menú
        menu.SetActive(false);

    }

    IEnumerator ItemsAnimation()
    {
        foreach(var item in items)
        {
            item.transform.localScale = Vector3.zero;
        }
        foreach(var item in items)
        {
            item.transform.DOScale(1f, fadeTime).SetEase(Ease.OutBounce);
            yield return new WaitForSeconds(0.25f);
        }

    }   
    IEnumerator ItemsDissapear()
    {
        foreach (var item in items)
        {
            item.transform.DOScale(0f, fadeTime/2);
            yield return new WaitForSeconds(fadeTime/8);
        }
    }

    public void BackToMenu()
    {
        animatedPanel.LoadIntScene(0);
        UnpauseGame();

    }


    public void RestartPlayerPrefs()
    {
        PlayerPrefsManager.Instance.DeleteAllStoredPlayerPrefs();
        CharacterManager.Instance.DisplayScore();
    }
    public void Restart()
    {
        //Respawn from the Respawn Manager
        UnpauseGame() ;
        respawnManager.Respawn();
        PanelFadeOut();
    }
    public void PauseMenu()
    {
       // gameIsPaused=true;
    }
    public void PauseGame()
    {
        gameIsPaused = true;
        Time.timeScale = 0f;
    }
    public void UnpauseGame()
    {
        gameIsPaused = false;
        Time.timeScale = 1f;
    }

    public void StartGame()
    {
        indexTutorial = PlayerPrefs.GetInt("IndexTutorial");
        Debug.Log("Cargando escena # " + indexTutorial);
        if (indexTutorial == 0)
        {
            animatedPanel.LoadIntScene(1);
            //StartCoroutine(FadeAndLoadScene(1));
        }
        else if (PlayerPrefs.GetInt("SelectedLevel") == 0 && indexTutorial == 1)
        {
            animatedPanel.LoadIntScene(2);
            //StartCoroutine(FadeAndLoadScene(2));
        }
        else
        {
            animatedPanel.LoadIntScene(PlayerPrefs.GetInt("SelectedLevel"));
            //SceneManager.LoadScene(PlayerPrefs.GetInt("SelectedLevel"));
        }

    }
    // Funci�n para la cuenta regresiva
    IEnumerator StartCountdown()
    {
        bGTimer.SetActive(true);
        int countdown = 3;  // Tiempo en segundos
        buttonsMenu.SetActive(false);

        while (countdown > 0)
        {
            countdownText.text = countdown.ToString();  // Mostrar el tiempo en pantalla
            yield return new WaitForSecondsRealtime(1f);  // Esperar 1 segundo en tiempo real (no afectado por Time.timeScale)
            countdown--;
        }

        countdownText.text = "GO!";  // Mensaje final antes de reanudar el juego
        yield return new WaitForSecondsRealtime(1f);  // Mostrar "GO!" por un segundo
        UnpauseGame();  // Reanudar el juego
        countdownText.text = "";
        bGTimer.SetActive(false);
        
    }
}
