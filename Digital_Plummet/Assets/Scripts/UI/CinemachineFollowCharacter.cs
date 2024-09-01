using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CinemachineFollowCharacter : MonoBehaviour
{
    private GameObject player;
    private CinemachineVirtualCamera cineMachine;

        void Start()
        {
            // Busca el GameObject con la etiqueta "Player"
            player = GameObject.Find("CameraFollowObject");

            if (player == null)
            {
                Debug.LogError("No se encontró ningún GameObject con la etiqueta 'Player'. Asegúrate de que el jugador tenga la etiqueta correcta.");
                return; // Salir del método si el player es null para evitar otros errores
            }

            // Obtén el componente CinemachineVirtualCamera
            cineMachine = this.GetComponent<CinemachineVirtualCamera>();

            if (cineMachine == null)
            {
                Debug.LogError("No se encontró el componente 'CinemachineVirtualCamera'. Asegúrate de que esté adjunto al GameObject que tiene este script.");
                return; // Salir del método si cineMachine es null para evitar otros errores
            }

            // Asigna el Follow al Transform del jugador
            cineMachine.Follow = player.transform;

        }

}
