using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject scoreUI;
    public GameObject gameOverText;
    public GameObject lineDrawerObj;
    public GameObject cameraObj;
    public GameObject objectSpawnerObj;
    public GameObject tree;
    public float delayAmount;
    public float startSpeed;
    public float speedDecreaseRate;
    public float obstacleReduceSpeed;
    public float itemBoostSpeed;

    TMPro.TMP_Text scoreTxt;
    LineDrawer lineDrawer;
    Move moveScript;
    CameraFollow cameraFollow;
    GyroController gyroController;
    ObjectSpawner objectSpawner;
    public AudioSource sfx;

    float timer;
    float score;
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
        cameraFollow = cameraObj.GetComponent<CameraFollow>();
        gyroController = cameraObj.GetComponent<GyroController>();
        objectSpawner = objectSpawnerObj.GetComponent<ObjectSpawner>();

        score = 0;
        isGameOver = false;
        gameOverText.SetActive(false);
        moveScript.yspeed = startSpeed;
        sfx.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver) {
             timer += Time.deltaTime;
 
            if (timer >= delayAmount)
            {
                timer = 0f;
                score += moveScript.yspeed * 0.1f;

                objectSpawner.IncreaseSpawnRate();
            }
            scoreTxt.text = score.ToString("n2") + " cm.";

            moveScript.yspeed -= speedDecreaseRate;

            if (moveScript.yspeed <= 0) {
                TriggerGameOver();
            }
        }
    }

    public void DecreaseSpeed() {
        moveScript.yspeed -= obstacleReduceSpeed;
    }

    public void IncreaseSpeed() {
        moveScript.yspeed += itemBoostSpeed;
    }

    public void TriggerGameOver() {
        isGameOver = true;
        objectSpawner.StopSpawning();
        sfx.Stop();
        
        lineDrawer.isRunning = false;
        lineDrawer.SetLineWidth(score / 10.0f);
        moveScript.yspeed = 0;
        gyroController.treshold = 9999999999;   // disable input

        GameObject line = lineDrawerObj.transform.GetChild(0).gameObject;
        RandomSpawnRoots(line);
        RescaleTree(score);
        ZoomOutCamera(score);
        // gameOverText.SetActive(true);
    }

    public void RescaleTree(float factor) {
        tree.transform.localScale = new Vector3(factor, factor, 1);
        tree.transform.position = new Vector3(0, score * 1.5f, 0);
    }

    public void RandomSpawnRoots(GameObject mainRoot) {
        for (int i=0; i<7; i++) {
            GameObject copyRoot = Instantiate(mainRoot, mainRoot.transform.position, Quaternion.Euler(0, 0, Random.Range(-80.0f, 80.0f)));
            copyRoot.transform.localScale *= Random.Range(0.25f, 0.6f);
        }
    }

    public void ZoomOutCamera(float yPosition)
    {
        float approxCameraDistance = distanceCalculator(yPosition);

        Vector3 lastCameraPosition = cameraObj.transform.position;
        Vector3 newCameraPosition = new Vector3(0, 0, approxCameraDistance * (-1.0f));
        cameraFollow.enabled = false;
        iTween.MoveTo(cameraObj, newCameraPosition, 1.5f);
    }

    private float distanceCalculator(float yPosition)
    {
        float approxCameraDistance = 0;
        if (yPosition <= 60)
        {
            approxCameraDistance = 120;
        }
        else if (yPosition <= 120)
        {
            approxCameraDistance = 600;
        }
        else if (yPosition <= 300)
        {
            approxCameraDistance = 800;
        }
        else
        {
            approxCameraDistance = 1000;
        }

        return approxCameraDistance;
    }
}
