using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorseInput : MonoBehaviour {

   public Collider DotButton;
   public Collider DashButton;
   public Collider SendButton;

   public MorseCodeFlasher MCF;
   public MainGame Game;

   public string UserInput = "";

   void ProcessMorse () {
      if (UserInput == "-.") {
         MCF.IncrementLetterIndex();
      }
      if (UserInput == ".--.") {
         MCF.DecrementLetterIndex();
      }
      if (UserInput == "..-" && !Game.GameStarted) {
         Game.StartStageOne();
      }
      UserInput = "";
   }

   IEnumerator ButtonAnimation (GameObject a) {
      for (int i = 0; i < 5; i++) {
         a.transform.localPosition += new Vector3(0, -0.03f, 0);
         yield return new WaitForSeconds(0.005f);
      }
      for (int i = 0; i < 5; i++) {
         a.transform.localPosition += new Vector3(0, +0.03f, 0);
         yield return new WaitForSeconds(0.005f);
      }
   }

   private void Update () {
      if (Input.GetMouseButtonDown(0)) {
         Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));

         if (DotButton.Raycast(ray, out RaycastHit hit, 30f)) {
            UserInput += ".";
            StartCoroutine(ButtonAnimation(DotButton.gameObject));
         }
         if (DashButton.Raycast(ray, out hit, 30f)) {
            UserInput += "-";
            StartCoroutine(ButtonAnimation(DashButton.gameObject));
         }
         if (SendButton.Raycast(ray, out hit, 30f)) {
            ProcessMorse();
            StartCoroutine(ButtonAnimation(SendButton.gameObject));
         }
      }
   }
}
