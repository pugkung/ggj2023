using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    GameController mainController;

    void Start() {
        mainController = FindObjectOfType<GameController>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") {
            mainController.DecreaseSpeed();
        }

        if (collision.gameObject.tag == "Item") {
            Destroy(collision.gameObject);
            mainController.IncreaseSpeed();
        }
    }
}
