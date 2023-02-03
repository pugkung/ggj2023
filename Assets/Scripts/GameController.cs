using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject scoreUI;
    public GameObject gameOverText;
    public GameObject lineDrawerObj;
    public GameObject cameraObj;
    public float delayAmount;

    TMPro.TMP_Text scoreTxt;
    LineDrawer lineDrawer;
    Move moveScript;
    GyroController gyroController;

    float timer;
    int score;
    bool isGameOver;

    void Awake() {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreTxt = scoreUI.GetComponent<TMPro.TMP_Text>();
        lineDrawer = lineDrawerObj.GetComponent<LineDrawer>();
        moveScript = cameraObj.GetComponent<Move>();
        gyroController = cameraObj.GetComponent<GyroController>();

        score = 0;
        isGameOver = false;
        gameOverText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver) {
             timer += Time.deltaTime;
 
            if (timer >= delayAmount)
            {
                timer = 0f;
                score++;
            }
            scoreTxt.text = score.ToString();
        }
    }

    public void TriggerGameOver() {
        isGameOver = true;
        gameOverText.SetActive(true);
        lineDrawer.isRunning = false;
        moveScript.yspeed = 0;
        gyroController.treshold = 9999999999;
    }

    public void RestartGame(){
        Application.LoadLevel(Application.loadedLevel);
    }
}
