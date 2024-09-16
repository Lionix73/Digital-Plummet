using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAnimation : MonoBehaviour
{
    [SerializeField] GameObject animationTutorial;
    // Start is called before the first frame update
    void Start()
    {
        animationTutorial.SetActive(false);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animationTutorial.SetActive(true);
        }
    }
}
