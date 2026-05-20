using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorseInput : MonoBehaviour {

   public Collider DotButton;
   public Collider DashButton;
   public Collider SendButton;

   public MorseCodeFlasher MCF;

   public string UserInput = "";

   void ProcessMorse () {
      if (UserInput == "-.") {
         MCF.IncrementLetterIndex();
      }
      if (UserInput == ".--.") {
         MCF.DecrementLetterIndex();
      }
      UserInput = "";
   }

   private void Update () {
      if (Input.GetMouseButtonDown(0)) {
         Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));

         if (DotButton.Raycast(ray, out RaycastHit hit, 30f)) {
            UserInput += ".";
         }
         if (DashButton.Raycast(ray, out hit, 30f)) {
            UserInput += "-";
         }
         if (SendButton.Raycast(ray, out hit, 30f)) {
            ProcessMorse();
         }
      }
   }
}
