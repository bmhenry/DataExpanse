 using UnityEngine;
 using System.Collections;
 
 public class Move : MonoBehaviour {
     
     float panSpeed = 100.0f;
	 float zoomSpeed = 30f;
     
     void Update() {
         var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), zoomSpeed * Input.GetAxis("Mouse ScrollWheel"));
         transform.position += move * panSpeed * Time.deltaTime;
		 
/* 		Vector3 camPos = this.transform.position;
		Debug.Log(camPos); */
/* 		fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
		fov = Mathf.Clamp(fov, minFov, maxFov);
		Camera.main.fieldOfView = fov; */
     }
 }