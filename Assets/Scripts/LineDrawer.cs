using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
public GameObject linePrefab;
	[HideInInspector]
	public Line currentLine;
	public Transform lineParent;
	public RigidbodyType2D lineRigidBodyType = RigidbodyType2D.Kinematic;
	public LineEnableMode lineEnableMode = LineEnableMode.ON_CREATE;
	public static LineDrawer instance;
	public bool isRunning;

	void Awake ()
	{
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start ()
	{
		if (lineParent == null) {
			lineParent = GameObject.Find ("Lines").transform;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!isRunning) {
            RelaseCurrentLine();
			return;
		}


        if (isRunning && currentLine == null) {
            CreateNewLine();
        }

		if (currentLine != null) {
            Vector2 screenCenter = new Vector2(Screen.width/2.0f, Screen.height/2.0f);
			currentLine.AddPoint (Camera.main.ScreenToWorldPoint(screenCenter));
		}
	}

	private void CreateNewLine ()
	{
		currentLine = (Instantiate (linePrefab, Vector3.zero, Quaternion.identity) as GameObject).GetComponent<Line> ();
		currentLine.name = "Line";
		currentLine.transform.SetParent (lineParent);
		currentLine.SetRigidBodyType (lineRigidBodyType);

		if (lineEnableMode == LineEnableMode.ON_CREATE) {
			EnableLine ();
		}
	}

	private void EnableLine ()
	{
		currentLine.EnableCollider ();
		currentLine.SimulateRigidBody ();
	}

	private void RelaseCurrentLine ()
	{
		if (lineEnableMode == LineEnableMode.ON_RELASE) {
			EnableLine ();
		}

		currentLine = null;
	}

	public enum LineEnableMode
	{
		ON_CREATE,
		ON_RELASE}

	;
}
