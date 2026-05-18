using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour {

   public Camera c;
   float CamRotationX = 0f;
   float CamRotationY = 0f;
   float MouseSensitivity = 1f;

   public float Paranoia;
   public float ElapsedTime;
   public float MouseMovement;

   void Start () {

   }

   // Update is called once per frame
   void Update () {

      if (Input.GetKeyDown(KeyCode.Escape)) {
         if (Cursor.lockState != CursorLockMode.Locked) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
         }
         else {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
         }
         
      }
      CamRotationX += Input.GetAxis("Mouse Y") * -MouseSensitivity;
      CamRotationY += Input.GetAxis("Mouse X") * MouseSensitivity;
      c.transform.localEulerAngles = new Vector3(CamRotationX, CamRotationY, 0);
   }
}
