using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheMagician : MonoBehaviour {

   public MainGame Game;
   public Doors Door;
   public CameraSystem Cams;

   public int AILevel;
   public float DefaultMovementTimer;
   public bool GoingLeft;
   public GameObject[] LeftPositions;
   public GameObject[] RightPositions;
   int Stage;
   float TargetMoveTime;

   public AudioSource[] Knocks;

   void Start () {
      for (int i = 0; i < 3; i++) {
         LeftPositions[i].SetActive(false);
         RightPositions[i].SetActive(false);
      }
   }

   public void InitializeMagician (int AI) {
      AILevel = AI;

      DefaultMovementTimer = 7f * AILevel * AILevel / 85 - 261f * AILevel / 85 + 603f / 17;

      StartCoroutine(MovementOpportunity());
   }

   IEnumerator MovementOpportunity () {
      while (true) {
         while (Stage < 4) {
            for (int i = 0; i < 3; i++) {
               LeftPositions[i].SetActive(false);
               RightPositions[i].SetActive(false);
            }

            if (Stage != 0) {
               for (int i = 1; i < 4; i++) {
                  if (Stage == i) {
                     if (GoingLeft) {
                        LeftPositions[i - 1].SetActive(true);
                     }
                     else {
                        RightPositions[i - 1].SetActive(true);
                     }
                  }
                  else {
                     LeftPositions[i - 1].SetActive(false);
                     RightPositions[i - 1].SetActive(false);
                  }
               }
            }

            TargetMoveTime = Game.GameTime + DefaultMovementTimer;

            while (Game.GameTime < TargetMoveTime) {
               yield return null;
            }

            Stage++;
         }

         if (GoingLeft) {
            Knocks[0].Play();
         }
         else {
            Knocks[1].Play();
         }

         for (int i = 0; i < 3; i++) {
            LeftPositions[i].SetActive(false);
            RightPositions[i].SetActive(false);
         }

         if (!Door.LeftBarricaded && GoingLeft) {
            Debug.Log("Left mag died at " + Game.GameTime);
         }
         if (Door.LeftBarricaded && !GoingLeft) {
            Debug.Log("Right mag died at " + Game.GameTime);
         }

         Stage = 0;
      }
   }

   // Update is called once per frame
   void Update () {

   }
}
