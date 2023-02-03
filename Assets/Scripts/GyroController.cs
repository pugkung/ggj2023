using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroController : MonoBehaviour
{
    public float sensitivity;
    public float treshold;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Mathf.Abs(Input.acceleration.x) > treshold ? Input.acceleration.x * sensitivity: 0;
        float yMove = Mathf.Abs(Input.acceleration.y) > treshold ? Input.acceleration.y * sensitivity: 0;
        transform.Translate (Time.deltaTime * xMove, Time.deltaTime * yMove, 0);
    }
}
