using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilouetteTrail : MonoBehaviour
{
    [Header("Silouette Trail Variables")]
    public static SilouetteTrail me;
    [SerializeField] private GameObject silouettePrefab;
    [SerializeField] List<GameObject> pool = new List<GameObject>();

    [Header("Trail Control Variables")]
    [SerializeField] private float speed;
    [SerializeField] private Color color;

    private float crono;

    void Awake(){
        me = this;
    }

    public GameObject GetSilouette(){
        for (int i = 0; i < pool.Count; i++)
        {
            if(!pool[i].activeInHierarchy){
                pool[i].SetActive(true);
                pool[i].transform.position = transform.position;
                pool[i].transform.rotation = transform.rotation;
                pool[i].GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
                pool[i].GetComponent<Silouette>().Color = color;
                return pool[i];
            }
        }

        GameObject obj = Instantiate(silouettePrefab, transform.position, transform.rotation);
        obj.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
        obj.GetComponent<Silouette>().Color = color;
        pool.Add(obj);
        return obj;
    }

    public void Silouette_Trail(){
        crono += Time.deltaTime * speed;
        if(crono > 1){
            GetSilouette();
            crono = 0;
        }
    }
}
