using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CinemachineFollowCharacter : MonoBehaviour
{
    private GameObject player;
    private GameObject realPlayer;
    private CinemachineVirtualCamera cineMachine;

    void Start()
    {
        FindAndFollow();
    }

    void Update(){
        player.transform.position = realPlayer.transform.position;
    }

    public void FindAndFollow(){
        // The REAL player
        realPlayer = GameObject.FindWithTag("Player");

        // Busca el GameObject que hace de "player" para la camara
        player = GameObject.Find("CameraFollowObject");

        player.transform.position = realPlayer.transform.position;

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
