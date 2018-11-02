using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class orbit : MonoBehaviour {
	
	public GameObject center;
	
	public float orbitSpeed = -0.01f; // degrees/frame
	float currentAngle;
	float magnitude;
	Vector3 centerPos;

	// Use this for initialization
	void Start () {
		//center.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
		Vector3 position = gameObject.transform.position;
		currentAngle = Vector3.Angle(position, new Vector3(1.0f, 0.0f, 0.0f));
		magnitude = (position - center.transform.position).magnitude;
	}
	
	// Update is called once per frame
	void Update () {
		centerPos = center.transform.position;
		Vector3 position = gameObject.transform.position;
		currentAngle += orbitSpeed;
		gameObject.transform.position = new Vector3(centerPos.x + Mathf.Cos(currentAngle) * magnitude, centerPos.y + Mathf.Sin(currentAngle) * magnitude, position.z);
	}
}
