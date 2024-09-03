using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] public float fadeTime = 1.0f;
    [SerializeField] GameObject menu;
    public Canvas myCanvas;
    public CanvasGroup canvasGroup;
    public RectTransform rectTransform;
    private float canvasWidth;
    public List<GameObject> items = new List<GameObject>();

    public void Start()
    {
        menu.SetActive(false);
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
            canvasGroup.alpha = 0f;
            rectTransform.transform.localPosition = new Vector3(-canvasWidth, 0f, 0f);
            rectTransform.DOAnchorPos(new Vector2(0f, 0f), fadeTime, false).SetEase(Ease.OutElastic);
            canvasGroup.DOFade(1, fadeTime);
            StartCoroutine("ItemsAnimation");
    }

    public void PanelFadeOut()
    {
        canvasGroup.alpha = 1f;
        rectTransform.transform.localPosition = new Vector3(0f, 0f, 0f);
        rectTransform.DOAnchorPos(new Vector2(canvasWidth, 0f), fadeTime, false).SetEase(Ease.InOutQuint);
        canvasGroup.DOFade(1, fadeTime);
        
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

    public void BackToMenu()
    {
        SceneManager.LoadScene("Main_Menu");

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
