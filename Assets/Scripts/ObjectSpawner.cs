using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] spawnPool;
    public float spawnrate;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float spawnProbably = Random.Range(0.0f, 1.0f);
        if (spawnProbably < spawnrate) {
            GameObject spawnItem = spawnPool[Random.Range(0, spawnPool.Length - 1)];

            Vector3 relativeSpawnPosition = new Vector3(Random.Range(0,Screen.width), Screen.height, -30f);
            Vector2 spawnPosition = Camera.main.ScreenToWorldPoint(relativeSpawnPosition);
            
            Vector3 randomRotation = new Vector3(0, 0, Random.Range(0,360));
            
            GameObject item = Instantiate (spawnItem, spawnPosition, Quaternion.Euler(randomRotation));
            item.transform.localScale = new Vector2(Random.Range(0.75f, 1.5f), Random.Range(0.75f, 1.5f));
        }
    }
}
