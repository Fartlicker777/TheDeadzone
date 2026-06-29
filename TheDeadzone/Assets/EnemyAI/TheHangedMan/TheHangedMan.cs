using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheHangedMan : MonoBehaviour {

   public MainGame Game;
   public CameraSystem Cams;

   public int AILevel = 20;
   int Progression = 10000;
   int Subtractor;

   public GameObject HangedManBody;

   void Start () {
      HangedManBody.SetActive(false);
   }

   public void InitializeHangedMan (int AI) {
      AILevel = AI;
      Subtractor = (int) (10000 / ((0.0672269 * AILevel * AILevel - 3.01681 * AILevel + 53.4454)));
      StartCoroutine(MovementOpportunity());
   }

   IEnumerator MovementOpportunity () {
      int butt = 0;
      while (Progression > 0) {
         if (Cams.SelectedCam == 5) {

            float ParanoiaMult = 3;

            if (Game.Paranoia > 1500) {
               ParanoiaMult = 2;
            }
            else if (Game.Paranoia > 500) {
               ParanoiaMult = 2.5f;
            }
            else if (Game.Paranoia > 250) {
               ParanoiaMult = 2.75f;
            }
            else {
               ParanoiaMult = 3f;
            }

            Progression += (int)((float) Subtractor * Time.deltaTime * ParanoiaMult);
            if (Progression > 10000) {
               Progression = 10000;
            }
         }
         else {
            Progression -= (int)((float) Subtractor * Time.deltaTime);
         }
         butt++;
         yield return null;
      }

      Debug.Log("Time of Death: " + Game.GameTime + " " + butt);
   }


   void Update () {
      /*if (Progression > 0) {
         Debug.Log("Progression = " + Progression);
      }*/

      if (!(Cams.SelectedCam == 5 && Cams.InCameras)) {
         HangedManBody.transform.localPosition = new Vector3(Mathf.Lerp(-8.5f, 2.7f, (10000 - Progression) / 10000f), -5.361f, -2.767311f);
      }
      
   }
}
