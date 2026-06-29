using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using rnd = UnityEngine.Random;

public class Death : MonoBehaviour {

   public MainGame Game;
   public LadderLightScript LadderLight;

   public int AILevel;

   public AudioSource LeftStep;
   public AudioSource RightStep;

   public AudioClip[] Steps;

   public GameObject DeathPos;

   float MinCooldown = 10f;

   bool CanAttack = true;

   void Start () {
      
   }

   public void InitializeDeath (int AI) {
      AILevel = AI;
      StartCoroutine(MovementOpportunity());
   }

   IEnumerator MovementOpportunity () {
      yield return new WaitForSeconds(MinCooldown);
      while (true) {
         if ((int) (Game.GameTime / (Game.Paranoia + 1)) % 4 == 0 && CanAttack) {
            CanAttack = false;
            StartCoroutine(Move());
            StartCoroutine(Attack());
         }
         yield return null;
      }
   }

   IEnumerator Move () {
      var dur = 2f;
      var elapsed = 0f;
      while (elapsed < dur) {
         elapsed += Time.deltaTime;
         DeathPos.transform.localPosition = new Vector3(Mathf.Lerp(11f, -1.05f, elapsed / dur), -3.16f, 8.85f);
         yield return null;
      }
      
   }

   IEnumerator Attack () {
      for (int i = 0; i < 8; i++) {
         if (i % 2 == 0) {
            //LeftStep.clip = Steps[rnd.Range(0, 6)];
            LeftStep.Play();
         }
         else {
            //RightStep.clip = Steps[rnd.Range(0, 6)];
            RightStep.Play();
         }
         yield return new WaitForSeconds(.25f);
      }
      if (!LadderLight.LitUp) {
         Debug.Log("Dead to rights");
      }
      StartCoroutine(CooldownWait());
   }

   IEnumerator CooldownWait () {
      float Cooldown = 16f - 6f * Game.Paranoia / 100;
      if (MinCooldown > Cooldown) {
         Cooldown = MinCooldown;
      }
      DeathPos.transform.localPosition = new Vector3(11f, -3.16f, 8.85f);
      yield return new WaitForSeconds(Cooldown);
      CanAttack = true;
   }


   void Update () {

   }
}
