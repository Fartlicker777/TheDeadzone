using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using rnd = UnityEngine.Random;

public class Clouds : MonoBehaviour {
   public GameObject[] CloudsGO;

   public int MaxCloudCount = 15;

   public float GameTime = 1f;

   private List<GameObject> CloudInstances =
       new List<GameObject>();

   void Start () {
      StartCoroutine(GenerateClouds());
   }

   IEnumerator GenerateClouds () {
      // Initial clouds
      for (int i = 0; i < MaxCloudCount; i++) {
         float startingX = Mathf.Lerp(
             -520f,
             200f,
             (float) i / (MaxCloudCount - 1)
         );

         SpawnCloud(true, startingX);
      }

      while (true) {
         for (int i = CloudInstances.Count - 1; i >= 0; i--) {
            GameObject cloudObject = CloudInstances[i];

            // Safety check
            if (cloudObject == null) {
               CloudInstances.RemoveAt(i);
               continue;
            }

            IndividualCloud cloud =
                cloudObject.GetComponent<IndividualCloud>();

            // Spawn ONE replacement when this cloud
            // reaches X = 200.
            if (cloud.ReadyForReplacement && !cloud.ReplacementSpawned) {
               cloud.ReplacementSpawned = true;

               SpawnCloud(false, -520f);
            }

            // Destroy the old cloud at X = 300.
            if (cloud.EndOfLife) {
               Destroy(cloudObject);
               CloudInstances.RemoveAt(i);
            }
         }

         yield return null;
      }
   }

   void SpawnCloud (bool InitialCloud, float startingX) {
      GameObject cloudObject = Instantiate(
          CloudsGO[rnd.Range(0, CloudsGO.Length)],
          new Vector3(
              -9f,
              rnd.Range(120f, 150f),
              rnd.Range(-4f, 0f)
          ),
          Quaternion.identity
      );

      CloudInstances.Add(cloudObject);

      cloudObject
          .GetComponent<IndividualCloud>()
          .SetCloudProperties(InitialCloud, startingX);
   }

   void Update () {
      Time.timeScale = GameTime;
   }
}