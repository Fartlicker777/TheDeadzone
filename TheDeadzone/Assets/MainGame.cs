using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using rnd = UnityEngine.Random;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class MainGame : MonoBehaviour {

   public CameraSystem CamSystem;

   public MorseCodeFlasher MCF;
   public bool GameStarted;
   public Camera p;
   public Camera c;
   float CamRotationX = 0f;
   float CamRotationY = 0f;
   float MouseSensitivity = 1f;

   public float MouseElement;
   public float GameTime;
   public float Paranoia;
   public float ElapsedTime;
   public float MouseMovement;

   public bool InputtingAnswer;

   public TheLovers Lovers;
   public TheEmperor Emperor;
   public TheMagician Magician;
   public TheHangedMan HangedMan;
   public Death DeathAI;
   public TheFool Fool;

   void Start () {
      float[] Distances = new float[32];
      Distances[6] = 5f;
      //c.nearClipPlane = Distances;

      StartCoroutine(GlobalTimer());
   }

   void StartCampaign () {
      StartCoroutine(WakeUp());

   }

   public void StartStageOne () {
      GameStarted = true;
      MCF.InitializeMorse();
   }

   void StartStageTwo () {
      Lovers.InitializeLovers(3);
      Emperor.InitializeTheEmperor(3);
   }

   void StartStageThree () {
      Magician.InitializeMagician(3);
      DeathAI.InitializeDeath(3);
   }

   void StartStageFour () {
      Fool.InitializeFool(3);
      HangedMan.InitializeHangedMan(3);
   }

   public void ProcessStageAdvance (int s) {
      if (s == 1) {
         StartStageTwo();
      }
      else if (s == 2) {
         StartStageThree();
      }
      else if (s == 3) {
         StartStageFour();
      }
      else {

      }
   }



   IEnumerator WakeUp () {
      yield return null;
   }

   IEnumerator GlobalTimer () {
      while (true) {
         GameTime += Time.deltaTime;
         yield return null;
      }
   }

   // Update is called once per frame
   void Update () {
      if (Input.GetKeyDown(KeyCode.Q)) {
         MouseElement += 100000; 
      }
      Paranoia = GameTime + MouseElement / 100;
      //Debug.Log("Paranoia = " + Paranoia);

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
      if (CamSystem.InCameras) {
         Cursor.lockState = CursorLockMode.None;
         Cursor.visible = true;
         return;
      }
      if (InputtingAnswer) {
         return;
      }
      MouseElement += Mathf.Abs(Input.GetAxis("Mouse Y") * MouseSensitivity);
      MouseElement += Mathf.Abs(Input.GetAxis("Mouse X") * MouseSensitivity);
      CamRotationX += Input.GetAxis("Mouse Y") * -MouseSensitivity;
      CamRotationY += Input.GetAxis("Mouse X") * MouseSensitivity;
      c.transform.localEulerAngles = new Vector3(CamRotationX, CamRotationY, 0);
      p.transform.localEulerAngles = new Vector3(CamRotationX, CamRotationY, 0);
   }
}
