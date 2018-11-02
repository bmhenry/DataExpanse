using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoom : MonoBehaviour {

	float maxFov = 90f;
	float minFov = 15f;
	float sensitivity = 10f;
 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float fov = Camera.main.fieldOfView;
		Debug.Log(fov);
		fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
		fov = Mathf.Clamp(fov, minFov, maxFov);
		Camera.main.fieldOfView = fov;
	}
}
/*  float camZoom = -10f;
 float camZoomSpeed = 2f;
 Transform Cam; 
 
 void Start (){
 Cam = this.transform;
 }
 
 void Update (){
 camZoom += Input.GetAxis("Mouse ScrollWheel") * camZoomSpeed;
 transform.position = new Vector3(Cam.position.x, Cam.position.y, camZoom );   // use localPosition if parented to another GameObject.
 }  */
