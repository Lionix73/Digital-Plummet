using UnityEngine;
using UnityEngine.UI;

public class FloatingSprite : MonoBehaviour
{
    [SerializeField] public Image UpArrow;
    [SerializeField] public Image DownArrow;
    [SerializeField] private RectTransform spriteTransform; // El RectTransform del objeto Image
    [SerializeField] private float amplitude = 10.0f; // La amplitud del movimiento (distancia que recorre)
    [SerializeField] private float speed = 2.0f; // La velocidad del movimiento (ciclos por segundo)
    [SerializeField] private float ArrowDisolveSpeed = 0.2f; // La velocidad del movimiento (ciclos por segundo)

    private Vector3 initialPosition;

    void Start()
    {
        // Guardar la posición inicial del sprite
        initialPosition = spriteTransform.anchoredPosition;
    }

    void Update()
    {
        // Calcular la nueva posición en función de la función seno
        float newY = initialPosition.y + Mathf.Sin(Time.time * speed) * amplitude;

        Debug.Log(Mathf.Sin(Time.time));

        // Obtener el color actual del Image
        Color currentColor1 = UpArrow.color;
        Color currentColor2 = DownArrow.color;

        // Modificar el valor del alfa restando Mathf.Sin(Time.time) * fadeSpeed
        float newAlpha1 = currentColor1.a - (Mathf.Sin(Time.time * speed));
        float newAlpha2 = currentColor2.a + (Mathf.Sin(Time.time * speed));

        // Limitar el valor del alfa entre 0 y 1
        newAlpha1 = Mathf.Clamp(newAlpha1, 0f, 1f);
        newAlpha2 = Mathf.Clamp(newAlpha2, 0f, 1f);

        // Asignar el nuevo valor del alfa al color
        currentColor1.a = newAlpha1;
        currentColor2.a = newAlpha2;

        // Aplicar el nuevo color al Image
        UpArrow.color = currentColor1;
        DownArrow.color = currentColor2;

        // Aplicar la nueva posición al RectTransform
        spriteTransform.anchoredPosition = new Vector2(initialPosition.x, newY);
    }
}
