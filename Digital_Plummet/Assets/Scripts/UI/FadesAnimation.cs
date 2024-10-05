using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadesAnimation : MonoBehaviour
{

    public CanvasGroup canvasGroup;
    public float fadeTime;

    // Start is called before the first frame update
    void Start()
    {
       FadeOut();
    }

    public void FadeOut()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.DOFade(0, fadeTime).SetUpdate(true);
    }
    public void FadeIn()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.DOFade(1, fadeTime).SetUpdate(true);

    }

    public void LoadIntScene(int SceneNumber)
    {
        StartCoroutine(FadeAndLoadNumberScene(SceneNumber));
    }

    public void LoadNameScene(string SceneName)
    {
        StartCoroutine(FadeAndLoadStringScene(SceneName));
    }


    private IEnumerator FadeAndLoadNumberScene(int sceneNumber)
    {
        FadeIn();

        // Espera el tiempo de la animación de fade antes de cambiar la escena
        yield return new WaitForSecondsRealtime(fadeTime);
        // Cambia a la escena del menú principal
        SceneManager.LoadScene(sceneNumber);

    }


    private IEnumerator FadeAndLoadStringScene(string sceneName)
    {
        FadeIn();

        // Espera el tiempo de la animación de fade antes de cambiar la escena
        yield return new WaitForSecondsRealtime(fadeTime);
        // Cambia a la escena del menú principal
        SceneManager.LoadScene(sceneName);

    }
}
