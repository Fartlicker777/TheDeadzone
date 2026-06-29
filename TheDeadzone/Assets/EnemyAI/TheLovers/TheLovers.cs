using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheLovers : MonoBehaviour {

   public GameObject[] LeftLoversPos;
   public GameObject[] RightLoversPos;

   public CameraSystem Cams;
   public Doors Door;
   public MainGame Game;

   public AudioSource LeftKnock;
   public AudioSource RightKnock;

   int[] LeftCamPath = new int[] { 4, 1, 0 };
   int[] RightCamPath = new int[] { 4, 2, 3 };

   public GameObject[] LeftLovers;
   public GameObject[] RightLovers;

   public int AILevel = 20;
   public bool LeftWatched;
   public bool RightWatched;
   bool LastWatchedWasLeft;

   public int LeftStage;
   public int RightStage;

   public double LeftStageTimeProgression;
   public double RightStageTimeProgression;

   public float LeftTargetMoveTime;
   public float RightTargetMoveTime;

   public int LeftCycles = 0;
   public int RightCycles = 0;

   public float DefaultMovementTimer = 3f;

   public int WhoDelays = 0; //0 = left, 1 = right

   public float dummytimer;

   private void Start () {
      for (int i = 0; i < 3; i++) {
         LeftLovers[i].SetActive(false);
         RightLovers[i].SetActive(false);
      }
   }

   public void InitializeLovers (int AI) {
      DefaultMovementTimer = (float) (AILevel * AILevel) / 34 - 47f * AILevel / 34 + 321f / 17f;
      Debug.Log(DefaultMovementTimer);
      int temp = (int) (DefaultMovementTimer * 10);
      DefaultMovementTimer = (float) temp / 10 - 0.5f;
      //DefaultMovementTimer = 1;
      StartCoroutine(LeftMovementOpportunity());
      StartCoroutine(RightMovementOpportunity());
   }

   IEnumerator LeftMovementOpportunity () {
      

      while (true) {
         while (LeftStage < 4) {

            for (int i = 0; i < 3; i++) {
               LeftLovers[i].SetActive(false);
            }
            if (LeftStage != 0) {
               for (int i = 1; i < 4; i++) {
                  if (LeftStage == i) {
                     LeftLovers[i - 1].SetActive(true);
                  }
                  else {
                     LeftLovers[i - 1].SetActive(false);
                  }
               }
            }
            LeftTargetMoveTime = Game.GameTime + DefaultMovementTimer;


            while (Game.GameTime < LeftTargetMoveTime) {
               yield return null;
               
               if (LeftStage != 0 && Cams.SelectedCam == LeftCamPath[LeftStage - 1]) {
                  LeftTargetMoveTime += Time.deltaTime;
                  LeftWatched = true;
                  LastWatchedWasLeft = true;
               }
               else {
                  LeftWatched = false;
               }

               /*  This doesn't matter
                     WhoDelays == 0: Just so one is delaying at a time so there isn't any jank

               (Mathf.Abs((LeftTargetMoveTime - RightTargetMoveTime)) > 0.1f                || Mathf.Abs((float) (LeftStageTimeProgression - RightStageTimeProgression)) > 1f)
This bit is so that if they are in the same stage, then they need to move at the same time     This is if they are desynced such that they are in different stages, if this is true, it allows one to continue to go faster until they are in the same stage.

                */
               if (WhoDelays == 0 && (Mathf.Abs((LeftTargetMoveTime - RightTargetMoveTime)) > 0.1f || Mathf.Abs((float) (LeftStageTimeProgression - RightStageTimeProgression)) > 1f) && !RightWatched) {

                  float ParanoiaMult = 0.2f;

                  if (Game.Paranoia > 1500) {
                     ParanoiaMult = .5f;
                  }
                  else if (Game.Paranoia > 500) {
                     ParanoiaMult = .4f;
                  }
                  else if (Game.Paranoia > 250) {
                     ParanoiaMult = .3f;
                  }
                  else {
                     ParanoiaMult = 0.2f;
                  }

                  LeftTargetMoveTime -= Time.deltaTime * ParanoiaMult;

                  if (Mathf.Abs(LeftTargetMoveTime - RightTargetMoveTime) < 0.1f && Mathf.Abs((float) (LeftStageTimeProgression - RightStageTimeProgression)) < 1f) {
                     LeftTargetMoveTime = RightTargetMoveTime;
                  }
               }

               LeftStageTimeProgression = 3 * LeftStage + Game.GameTime - LeftTargetMoveTime + DefaultMovementTimer;
            }

            LeftStage++;
         }

         LeftKnock.Play();

         for (int i = 0; i < 3; i++) {
            LeftLovers[i].SetActive(false);
         }
         if (!Door.LeftBarricaded) {
            Debug.Log("Left died at " + Game.GameTime);
         }
         LeftCycles++;
         LeftStage = 0;
      }
      
   }

   IEnumerator RightMovementOpportunity () {

      while (true) {
         while (RightStage < 4) {

            for (int i = 0; i < 3; i++) {
               RightLovers[i].SetActive(false);
            }
            if (RightStage != 0) {
               for (int i = 1; i < 4; i++) {
                  if (RightStage == i) {
                     RightLovers[i - 1].SetActive(true);
                  }
                  else {
                     RightLovers[i - 1].SetActive(false);
                  }
               }
            }

            RightTargetMoveTime = Game.GameTime + DefaultMovementTimer;


            while (Game.GameTime < RightTargetMoveTime) {
               yield return null;
               if (RightStage != 0 && Cams.SelectedCam == RightCamPath[RightStage - 1]) {
                  RightTargetMoveTime += Time.deltaTime;
                  RightWatched = true;
                  LastWatchedWasLeft = false;
               }
               else {
                  RightWatched = false;
               }

               if (WhoDelays == 1 && (Mathf.Abs((LeftTargetMoveTime - RightTargetMoveTime)) > 0.1f || Mathf.Abs((float)(LeftStageTimeProgression - RightStageTimeProgression)) > 1f) && !LeftWatched) {

                  float ParanoiaMult = 0.2f;

                  if (Game.Paranoia > 1500) {
                     ParanoiaMult = .5f;
                  }
                  else if (Game.Paranoia > 500) {
                     ParanoiaMult = .4f;
                  }
                  else if (Game.Paranoia > 250) {
                     ParanoiaMult = .3f;
                  }
                  else {
                     ParanoiaMult = 0.2f;
                  }

                  RightTargetMoveTime -= Time.deltaTime * ParanoiaMult;

                  if (Mathf.Abs(LeftTargetMoveTime - RightTargetMoveTime) < 0.1f && Mathf.Abs((float) (LeftStageTimeProgression - RightStageTimeProgression)) < 1f) {
                     RightTargetMoveTime = LeftTargetMoveTime;
                  }
               }

               RightStageTimeProgression = 3 * RightStage + Game.GameTime - RightTargetMoveTime + DefaultMovementTimer;
            }

            RightStage++;
         }

         RightKnock.Play();

         if (Door.LeftBarricaded) {
            Debug.Log("Right died at " + Game.GameTime);
         }
         RightCycles++;
         RightStage = 0;
      }

      
   }

   // Update is called once per frame
   void Update () {
      dummytimer = Game.GameTime;

      if (LastWatchedWasLeft && Mathf.Abs((float) LeftStageTimeProgression % (4 * DefaultMovementTimer) - (float) RightStageTimeProgression % (4 * DefaultMovementTimer)) > 0.1f) {
         WhoDelays = 1;
      }
      else if (!LastWatchedWasLeft && Mathf.Abs((float) LeftStageTimeProgression % (4 * DefaultMovementTimer) - (float) RightStageTimeProgression % (4 * DefaultMovementTimer)) > 0.1f) {
         WhoDelays = 0;
      }
      else {
         WhoDelays = 2;
      }

      if (Mathf.Abs((float) LeftStageTimeProgression % (4 * DefaultMovementTimer) - (float) RightStageTimeProgression % (4 * DefaultMovementTimer)) < 0.1f && !LeftWatched && !RightWatched) {
         LeftTargetMoveTime = RightTargetMoveTime;
      }
   }
}
