using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderLightScript : MonoBehaviour {
   
   public GameObject L;
   public Collider LadderColl;
   public float range = 5f;

   private void Update () {
      if (Input.GetMouseButtonDown(0)) {
         Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));

         if (LadderColl.Raycast(ray, out RaycastHit hit, range)) {
            L.SetActive(!L.activeSelf);
            Debug.Log(hit.collider.name);
         }
      }
   }
}
