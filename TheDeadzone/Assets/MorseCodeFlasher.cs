using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using rnd = UnityEngine.Random;

public class MorseCodeFlasher : MonoBehaviour {

   float UnitLength = .2f;

   string[] MorseLetters = { ".-", "-...", "-.-.", "-..", ".", "..-.", "--.", "....", "..", ".---", "-.-", ".-..", "--", "-.", "---", ".--.", "--.-", ".-.", "...", "-", "..-", "...-", ".--", "-..-", "-.--", "--.." };
   string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
   public Light Flasher;
   public GameObject Hider;

   int[] ChosenLetters = { 0, 0, 0, 0, 0};

   string MorseBuilder = "";

   void Start () {
      for (int i = 0; i < 5; i++) {
         ChosenLetters[i] = rnd.Range(0, 26);
         MorseBuilder += MorseLetters[ChosenLetters[i]];
         MorseBuilder += "|||";
      }
      MorseBuilder += "||||";
      Debug.Log(Alphabet[ChosenLetters[0]].ToString() + Alphabet[ChosenLetters[1]].ToString() + Alphabet[ChosenLetters[2]].ToString() + Alphabet[ChosenLetters[3]].ToString() + Alphabet[ChosenLetters[4]].ToString());
      StartCoroutine(Sequence());
   }

   IEnumerator Sequence () {
      while (true) {
         for (int i = 0; i < MorseBuilder.Length; i++) {
            if (MorseBuilder[i] == '.') {
               Flasher.gameObject.SetActive(true);
               Hider.SetActive(false);
               yield return new WaitForSeconds(UnitLength);
               Hider.SetActive(true);
               Flasher.gameObject.SetActive(false);
               yield return new WaitForSeconds(UnitLength);
            }
            if (MorseBuilder[i] == '-') {
               Flasher.gameObject.SetActive(true);
               Hider.SetActive(false);
               yield return new WaitForSeconds(3 * UnitLength);
               Hider.SetActive(true);
               Flasher.gameObject.SetActive(false);
               yield return new WaitForSeconds(UnitLength);
            }
            if (MorseBuilder[i] == '|') {
               yield return new WaitForSeconds(UnitLength);
            }
         }
      }
   }

   // Update is called once per frame
   void Update () {

   }
}
