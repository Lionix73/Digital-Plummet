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
                Debug.LogError("No se encontr� ning�n GameObject con la etiqueta 'Player'. Aseg�rate de que el jugador tenga la etiqueta correcta.");
                return; // Salir del m�todo si el player es null para evitar otros errores
            }

            // Obt�n el componente CinemachineVirtualCamera
            cineMachine = this.GetComponent<CinemachineVirtualCamera>();

            if (cineMachine == null)
            {
                Debug.LogError("No se encontr� el componente 'CinemachineVirtualCamera'. Aseg�rate de que est� adjunto al GameObject que tiene este script.");
                return; // Salir del m�todo si cineMachine es null para evitar otros errores
            }

            // Asigna el Follow al Transform del jugador
            cineMachine.Follow = player.transform;

        }

}
