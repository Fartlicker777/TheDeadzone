using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowBlind : MonoBehaviour {

   public Collider Window;
   public MorseCodeFlasher MCF;

   public bool ClosedWindow;
   public GameObject Cover;

   Coroutine CloseWindowCor;

   IEnumerator CloseWindow () {
      MCF.Flasher.gameObject.SetActive(false);
      float from = 0;
      float to = 0;

      if (ClosedWindow) {
         from = 3.2348f;
         to = 5.5f;
      }
      else {
         to = 3.2348f;
         from = 5.5f;
      }

      var dur = .1f;
      var elapsed = 0f;

      while (elapsed <= dur) {
         Cover.transform.localPosition = new Vector3(-98.6933f, Mathf.Lerp(from, to, elapsed / dur), -69.852f);
         yield return null;
         elapsed += Time.deltaTime;
         
      }

      Cover.transform.localPosition = new Vector3(-98.6933f, to, -69.852f);

      //Debug.Log(to);
      ClosedWindow = !ClosedWindow;
      CloseWindowCor = null;
   }

   void Update () {
      if (Input.GetMouseButtonDown(0)) {
         Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));

         if (Window.Raycast(ray, out RaycastHit hit, 30f)) {
            if (CloseWindowCor == null) {
               CloseWindowCor = StartCoroutine(CloseWindow());
            }
         }
      }
   }
}
