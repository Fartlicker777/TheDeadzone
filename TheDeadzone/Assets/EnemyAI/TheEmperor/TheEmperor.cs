using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheEmperor : MonoBehaviour {

   public MainGame Game;
   public WindowBlind Window;
   public int AILevel = 20;
   public GameObject Battleship;

   public AudioSource FoghornAS;
   public AudioSource FlareShotAS;
   public GameObject Flare;

   public Coroutine Attacking;
   public Coroutine AttackAnim;
   public bool CanKillYou;
   public float MinCooldown = 15f;
   bool CanAttack = true;

   void Start () {
      Battleship.transform.localPosition = new Vector3(-171.4f, 1.15f, -29.1f);
   }

   public void InitializeTheEmperor (int AI) {
      AILevel = AI;
      Battleship.transform.localPosition = new Vector3(-171.4f, 1.15f, -29.1f);
      Attacking = StartCoroutine(MovementOpportunities());
   } 
   
   IEnumerator Attack () {
      Battleship.transform.localPosition = new Vector3(-171.4f, 1.15f, -29.1f);
      Flare.SetActive(true);
      Flare.transform.localPosition = new Vector3(-171.4f, 1.15f, -65f);
      var elapsed = 0f;
      var dur = FlareShotAS.clip.length;
      FlareShotAS.Play();
      while (elapsed <= dur) {
         double xpos = Mathf.Lerp(-171.4f, 11.4f, elapsed / dur);
         Flare.transform.localPosition = new Vector3((float) xpos, (float) (-0.0041778 * xpos * xpos - 0.66426 * xpos + 13.263), -65f);
         yield return null;
         elapsed += Time.deltaTime;
      }
      float ParanoiaOffset = 100 / Game.Paranoia * 2;
      if (ParanoiaOffset > 3f) {
         ParanoiaOffset = 3f;
      }
      yield return new WaitForSeconds(3f + ParanoiaOffset);
      Flare.transform.localPosition = new Vector3(11.4f, 5.1f, -65f);
      FoghornAS.Play();
      Flare.SetActive(false);
      elapsed = 0f;
      dur = FoghornAS.clip.length;
      float to = -33.8f;
      while (elapsed <= dur) {
         if (elapsed >= 1f) {
            CanKillYou = true;
         }
         Battleship.transform.localPosition = new Vector3(Mathf.Lerp(-171.4f, to, elapsed / dur), 1.15f, -29.1f);
         yield return null;
         elapsed += Time.deltaTime;
      }
      CanKillYou = false;
      Battleship.transform.localPosition = new Vector3(to, 1.15f, -29.1f);
      StartCoroutine(CheckForAttack());
   }

   IEnumerator CheckForAttack () {
      float Cooldown = 30f - 10 * Game.Paranoia / 100;
      if (Cooldown < MinCooldown) {
         Cooldown = MinCooldown;
      }
      yield return new WaitForSeconds(Cooldown);
      CanAttack = true;
   }

   IEnumerator MovementOpportunities () {
      yield return new WaitForSeconds(MinCooldown);
      while (true) {
         //Debug.Log(Mathf.Sin(Game.GameTime * Mathf.Log(Game.Paranoia + 1) / (21 - AILevel) / 10));
         if (Mathf.Sin(Game.GameTime * Mathf.Log(Game.Paranoia + 1) / (21 - AILevel) / 10) >= 0.75f && CanAttack) {
            CanAttack = false;
            Debug.Log("Time = " + Game.GameTime);
            AttackAnim = StartCoroutine(Attack());
         }
         yield return null;
      }
   }

   // Update is called once per frame
   void Update () {
      if (Input.GetKeyDown(KeyCode.A)) {
         CanAttack = false;
         AttackAnim = StartCoroutine(Attack());
      }
      if (CanKillYou && !Window.ClosedWindow) {
         Debug.Log("DEAD");
      }
   }
}
