using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour {

   public Collider LeftDoorColl;
   public Collider RightDoorColl;
   public bool LeftBarricaded = false;

   float range = 30f;

   public GameObject LeftBarricadeOBJ;
   public GameObject RightBarricadeOBJ;

   private void Update () {
      if (Input.GetMouseButtonDown(0)) {
         Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));

         if (LeftDoorColl.Raycast(ray, out RaycastHit hit, range) && !LeftBarricaded) {
            RightBarricadeOBJ.SetActive(false);
            LeftBarricadeOBJ.SetActive(true);
            LeftBarricaded = true;
         }
         if (RightDoorColl.Raycast(ray, out hit, range) && LeftBarricaded) {
            RightBarricadeOBJ.SetActive(true);
            LeftBarricadeOBJ.SetActive(false);
            LeftBarricaded = false;
         }
      }
   }
}
