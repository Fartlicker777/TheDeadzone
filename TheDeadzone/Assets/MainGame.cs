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

   public bool Ingame = false;
   public GameObject Reticle;

   public CameraSystem CamSystem;

   public MorseCodeFlasher MCF;
   public MorseInput MI;
   public AnswerInput AnsInp;

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

   public GameObject MainMenuObj;
   public GameObject WinScreen;
   public GameObject DeathScreen;
   public TextMeshProUGUI TimeOfDeath;
   public TextMeshProUGUI CauseOfDeath;
   public TextMeshProUGUI CoronersNote;

   public AudioSource CassetteAS;
   public AudioClip[] PhoneCalls;

   Coroutine Timer;

   public Collider ExitDoor;

   public Collider Cassette;

   bool CanWin;

   void Start () {
      p.transform.localPosition = new Vector3(-98.68449f, 31.72f, 124f);
      c.transform.localPosition = new Vector3(-98.68449f, 31.72f, 124f);
   }

   public void StartCampaign () {
      //Debug.Log("boner");
      DeathScreen.SetActive(false);
      MainMenuObj.SetActive(false);
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
      Reticle.SetActive(true);
      p.transform.localPosition = new Vector3(-98.68449f, 3.361412f, -74.13731f);
      c.transform.localPosition = new Vector3(-98.68449f, 3.361412f, -74.13731f);
      Ingame = true;
      Timer = StartCoroutine(GlobalTimer());
      MCF.StopMainMenuFlash();
      StartCoroutine(WakeUp());
   }

   public void StartCustomNight (int[] EnemyAI, int Hazard) {


      MainMenuObj.SetActive(false);
      Ingame = true;
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
      p.transform.localPosition = new Vector3(-98.68449f, 3.361412f, -74.13731f);
      c.transform.localPosition = new Vector3(-98.68449f, 3.361412f, -74.13731f);
      Timer = StartCoroutine(GlobalTimer());
      MCF.StopMainMenuFlash();
      StartCoroutine(WakeUp());

      GameStarted = true;
      MCF.InitializeMorse();

      Fool.InitializeFool(EnemyAI[0]);
      Magician.InitializeMagician(EnemyAI[1]);
      Emperor.InitializeTheEmperor(EnemyAI[2]);
      Lovers.InitializeLovers(EnemyAI[3]);
      HangedMan.InitializeHangedMan(EnemyAI[4]);
      DeathAI.InitializeDeath(EnemyAI[5]);
   }

   public void StartStageOne () {
      GameStarted = true;
      MCF.InitializeMorse();
   }

   IEnumerator StartStageTwo () {
      //CassetteAS.clip = PhoneCalls[1];
      while (CassetteAS.isPlaying) {
         yield return null;
      }
      Lovers.InitializeLovers(3);
      Emperor.InitializeTheEmperor(3);
   }

   IEnumerator StartStageThree () {
      //CassetteAS.clip = PhoneCalls[2];
      while (CassetteAS.isPlaying) {
         yield return null;
      }
      Magician.InitializeMagician(3);
      DeathAI.InitializeDeath(3);
   }

   IEnumerator StartStageFour () {
      //CassetteAS.clip = PhoneCalls[3];
      while (CassetteAS.isPlaying) {
         yield return null;
      }
      Fool.InitializeFool(3);
      HangedMan.InitializeHangedMan(3);
   }

   public void OnRetryButton () {
      StartCampaign();
   }

   void Win () {
      StopCoroutine(Timer);
      Fool.Deactivate();
      DeathAI.Deactivate();
      HangedMan.Deactivate();
      Magician.Deactivate();
      Lovers.Deactivate();
      Emperor.Deactivate();
      WinScreen.SetActive(true);
   }

   public void HandleDeath (string COD) {
      StopCoroutine(Timer);
      Fool.Deactivate();
      DeathAI.Deactivate();
      HangedMan.Deactivate();
      Magician.Deactivate();
      Lovers.Deactivate();
      Emperor.Deactivate();
      MCF.Reset();
      AnsInp.Reset();
      MI.ResetInput();

      string Tip = "";
      switch (COD) {
         case "The Fool":
            Tip = "The landmines change every so often, make sure to check on The Fool to make sure he doesn't path into a mine.";
            break;
         case "The Emperor":
            Tip = "The flare shot signals when The Emperor is about to come, shut the window before he does.";
            break;
         case "The Lovers":
            Tip = "The Lovers can't move if their camera is selected, even if you are not actively in the camera system.";
            break;
         case "Death":
            Tip = "Make the lights are on exactly when Death reaches the ladder, the lights turn off automatically after a few seconds.";
            break;
         case "The Hanged Man":
            Tip = "Find time when desyncing The Lovers to watch the basement to push back The Hanged Man.";
            break;
         case "The Magician":
            Tip = "Remember which direction The Magician is coming down, and have the door shut when he arrives.";
            break;
         default:
            break;
      }
      TimeOfDeath.text = "Time of death - " + GameTime.ToString();
      CauseOfDeath.text = "Cause of death - " + COD;
      CoronersNote.text = "Coroner's note - " + Tip;
      DeathScreen.SetActive(true);
      Ingame = false;
   }

   public void ProcessStageAdvance (int s) {
      if (s == 1) {
         StartCoroutine(StartStageTwo());
      }
      else if (s == 2) {
         StartCoroutine(StartStageThree());
      }
      else if (s == 3) {
         StartCoroutine(StartStageFour());
      }
      else {
         CanWin = true;
      }
   }

   IEnumerator WakeUp () {
      yield return new WaitForSeconds(1f);
      CassetteAS.clip = PhoneCalls[0];
      CassetteAS.Play();
      while (CassetteAS.isPlaying) {
         yield return null;
      }
      StartStageOne();
   }

   IEnumerator GlobalTimer () {
      while (true) {
         GameTime += Time.deltaTime;
         yield return null;
      }
   }

   // Update is called once per frame
   void Update () {
      if (!Ingame) {
         Reticle.SetActive(false);
         Cursor.lockState = CursorLockMode.None;
         Cursor.visible = true;
         return;
      }
      if (Input.GetKeyDown(KeyCode.Q)) {
         MouseElement += 100000; 
      }
      Paranoia = GameTime + MouseElement / 100;
      //Debug.Log("Paranoia = " + Paranoia);

      if (Input.GetMouseButtonDown(0)) {
         Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));

         if (ExitDoor.Raycast(ray, out RaycastHit hit, 20) && CanWin) {
            Win();
         }
         if (Cassette.Raycast(ray, out hit, 20) && CassetteAS.isPlaying) {
            CassetteAS.Stop();
         }
      }

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
