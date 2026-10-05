using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using rnd = UnityEngine.Random;

public class IndividualCloud : MonoBehaviour {
   public GameObject Self;

   public bool EndOfLife;
   public bool ReadyForReplacement;
   public bool ReplacementSpawned;

   public float CloudSpeed = 7f;

   public void SetCloudProperties (bool InitialCloud, float StartingX) {
      Self.transform.localEulerAngles =
          new Vector3(270f, 0f, rnd.Range(0f, 360f));

      Self.transform.localScale =
          new Vector3(
              rnd.Range(4000f, 6000f),
              4000f,
              rnd.Range(1200f, 2400f)
          );

      Self.transform.localPosition = new Vector3(
          StartingX,
          rnd.Range(80f, 100f),
          rnd.Range(260f, 460f)
      );

      StartCoroutine(Traverse());
   }

   public IEnumerator Traverse () {
      while (Self.transform.localPosition.x < 300f) {
         Self.transform.localPosition +=
             Vector3.right * CloudSpeed * Time.deltaTime;

         // Only trigger this ONCE.
         if (!ReadyForReplacement && Self.transform.localPosition.x >= 200f) {
            ReadyForReplacement = true;
         }

         yield return null;
      }

      EndOfLife = true;
   }
}