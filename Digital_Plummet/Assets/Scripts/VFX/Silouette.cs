using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Silouette : MonoBehaviour
{
    [SerializeField] private Color color;
    private SpriteRenderer myRenderer;
    private Shader myMaterial;

    public Color Color { get => color; set => color = value; }

    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        myMaterial = Shader.Find("GUI/Text Shader");
    }

    void Update()
    {
        ColorSprite();
    }

    private void ColorSprite(){
        myRenderer.material.shader = myMaterial;
        myRenderer.color = color;
    }

    public void Finish(){
        gameObject.SetActive(false);
    }
}
