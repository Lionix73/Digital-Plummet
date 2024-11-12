using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SyncTextMeshPro : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI sourceText; // El TextMeshPro que queremos copiar
    private TextMeshProUGUI targetText; // El TextMeshPro que copia el texto

    void Start()
    {
        // Obtén el componente TextMeshProUGUI en este objeto
        targetText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // Sincroniza el texto del target con el texto del source
        if (sourceText != null && targetText != null)
        {
            targetText.text = sourceText.text;
        }
    }
}
