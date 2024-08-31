using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBar : MonoBehaviour
{
    [SerializeField] float lifeTime;
    private void Awake() {
        Destroy(gameObject,lifeTime);
    }
}