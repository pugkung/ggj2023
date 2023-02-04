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
        // accelerometer y-axis should influence game just a little bit
        float yMove = Mathf.Abs(Input.acceleration.y) > treshold ? Input.acceleration.y * sensitivity * 0.1f : 0;
        transform.Translate (Time.deltaTime * xMove, Time.deltaTime * yMove, 0);
    }
}
