using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using rnd = UnityEngine.Random;

public class TheFool : MonoBehaviour {

   public MainGame Game;

   public Collider[] Lights;
   public GameObject[] LightGO;

   public int AILevel;

   public Material Off;
   public Material FoolColored;
   public Material LandmineColored;
   public Material LureColored;

   public int FoolPos;

   public int LureIndex = -1;

   float DefaultMoveTime = 2f;

   float DefaultLandSwitchTime;

   int[] Landmines = new int[6];

   void Start () {
      for (int i = 0; i < 36; i++) {
         LightGO[i].GetComponent<MeshRenderer>().material = Off;
      }
   }

   public void InitializeFool (int AI) {
      AILevel = AI;
      DefaultMoveTime = Mathf.Ceil(35.2028f * Mathf.Exp(-0.0548394f * AILevel));

      for (int i = 0; i < 36; i++) {
         LightGO[i].GetComponent<MeshRenderer>().material = Off;
      }
      for (int i = 0; i < 6; i++) {
         Landmines[i] = rnd.Range(0, 6);
         LightGO[i * 6 + Landmines[i]].GetComponent<MeshRenderer>().material = LandmineColored;
      }
      do {
         FoolPos = rnd.Range(0, 6);
      } while (Landmines[0] == FoolPos && Landmines[1] != FoolPos);
      LightGO[FoolPos].GetComponent<MeshRenderer>().material = FoolColored;
      StartCoroutine(MovementOpportunity());
   }

   void SetLure (int index) {
      if (index == FoolPos) {
         return;
      }

      for (int i = 0; i < 6; i++) {
         if (i * 6 + Landmines[i] == index) {
            return;
         }
      }
      if (LureIndex != -1) {
         LightGO[LureIndex].GetComponent<MeshRenderer>().material = Off;
      }
      LureIndex = index;
      LightGO[index].GetComponent<MeshRenderer>().material = LureColored;
   }

   IEnumerator MovementOpportunity () {
      StartCoroutine(ChangeLandmines());
      while (true) {
         LightGO[FoolPos].GetComponent<MeshRenderer>().material = Off;
         if (LureIndex == -1) {
            FoolPos += 6;
         }
         else {
            if (FoolPos % 6 != LureIndex % 6) {
               if (FoolPos % 6 > LureIndex % 6) {
                  FoolPos--;
               }
               else {
                  FoolPos++;
               }
            }
            else {
               if (FoolPos / 6 > LureIndex / 6) {
                  FoolPos -= 6;
               }
               else {
                  FoolPos += 6;
               }
            }
         }

         if (LureIndex == FoolPos) {
            LureIndex = -1;
         }

         if (FoolPos > 35) {
            Debug.Log("Fooled");
            yield break;
         }

         for (int i = 0; i < 6; i++) {
            if (Landmines[i] + i * 6 == FoolPos) {
               Debug.Log("Fooled");
               yield break;
            }
         }

         LightGO[FoolPos].GetComponent<MeshRenderer>().material = FoolColored;

         yield return new WaitForSeconds(DefaultMoveTime);
      }
   }

   IEnumerator ChangeLandmines () {
      float WaitTime = 0f;
      while (true) {
         WaitTime = DefaultLandSwitchTime;
         yield return new WaitForSeconds(WaitTime);
         for (int i = 0; i < 6; i++) {
            LightGO[Landmines[i] + i * 6].GetComponent<MeshRenderer>().material = Off;
         }
         for (int i = 0; i < 6; i++) {
            do {
               Landmines[i] = rnd.Range(0, 6);
            } while (Landmines[i] + i * 6 == FoolPos || Landmines[i] + i * 6 == LureIndex);
            LightGO[Landmines[i] + i * 6].GetComponent<MeshRenderer>().material = LandmineColored;
         }
      }
   }

   void Update () {
      if (Input.GetMouseButtonDown(0)) {
         Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));

         for (int i = 0; i < Lights.Length; i++) {
            if (Lights[i].Raycast(ray, out RaycastHit hit, 20)) {
               SetLure(i);
            }
         }
      }

      if (Game.Paranoia > 2000) {
         DefaultLandSwitchTime = 25f;
      }
      else if (Game.Paranoia > 1000) {
         DefaultLandSwitchTime = 30f;
      }
      else if (Game.Paranoia > 500) {
         DefaultLandSwitchTime = 35f;
      }
      else if (Game.Paranoia > 250) {
         DefaultLandSwitchTime = 40f;
      }
      else {
         DefaultLandSwitchTime = 45f;
      }
   }
}
