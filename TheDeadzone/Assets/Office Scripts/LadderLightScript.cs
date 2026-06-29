using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderLightScript : MonoBehaviour {
   
   public GameObject L;
   public Collider LadderColl;
   public float range = 5f;

   public bool LitUp;
   bool CanLight = true;

   public Death DeathAI;

   public void TurnOnLight () {
      L.SetActive(true);
      LitUp = true;
      StartCoroutine(DeathWaitTime());
   }

   IEnumerator DeathWaitTime () {
      yield return new WaitForSeconds(1 + 2 * (1 - (float)DeathAI.AILevel / 20));
      CanLight = true;
      L.SetActive(false);
      LitUp = false;
   }

   private void Update () {
      if (Input.GetMouseButtonDown(0)) {
         Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));

         if (LadderColl.Raycast(ray, out RaycastHit hit, range) && CanLight) {
            CanLight = false;
            TurnOnLight();
         }
      }
   }
}
