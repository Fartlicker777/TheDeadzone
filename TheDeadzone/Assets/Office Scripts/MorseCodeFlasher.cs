using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using rnd = UnityEngine.Random;

public class MorseCodeFlasher : MonoBehaviour {

   float UnitLength = .2f;

   public int MorseStage = 0;
   public int LetterIndex = 0;

   string[] MorseLetters = { ".-", "-...", "-.-.", "-..", ".", "..-.", "--.", "....", "..", ".---", "-.-", ".-..", "--", "-.", "---", ".--.", "--.-", ".-.", "...", "-", "..-", "...-", ".--", "-..-", "-.--", "--.." };
   string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
   public Light Flasher;
   public GameObject Hider;

   int[] ChosenLetters = new int[25];

   string[][] MorseSequences = new string[][] { new string[] { "", "", "", "", ""}, new string[] { "", "", "", "", "" }, new string[] { "", "", "", "", "" }, new string[] { "", "", "", "", "" }, new string[] { "", "", "", "", "" } };

   bool WaitForLetterReset;

   void Start () {
      for (int i = 0; i < 25; i++) {
         ChosenLetters[i] = rnd.Range(0, 26);
         MorseSequences[i / 5][i % 5] = MorseLetters[ChosenLetters[i]].ToString();
      }
      
      Debug.Log(Alphabet[ChosenLetters[0]].ToString() + Alphabet[ChosenLetters[1]].ToString() + Alphabet[ChosenLetters[2]].ToString() + Alphabet[ChosenLetters[3]].ToString() + Alphabet[ChosenLetters[4]].ToString());
      StartCoroutine(Sequence());
   }

   public void IncrementLetterIndex () {
      if (LetterIndex < 4) {
         LetterIndex++;
      }
   }

   public void DecrementLetterIndex () {
      if (LetterIndex > 0) {
         LetterIndex--;
      }
   }

   IEnumerator Sequence () {
      while (true) {
         int CurIndex = LetterIndex;

         for (int i = 0; i < MorseSequences[MorseStage][CurIndex].Length; i++) {
            if (MorseSequences[MorseStage][CurIndex][i] == '.') {
               Flasher.gameObject.SetActive(true);
               Hider.SetActive(false);
               yield return new WaitForSeconds(UnitLength);
               Hider.SetActive(true);
               Flasher.gameObject.SetActive(false);
               yield return new WaitForSeconds(UnitLength);
            }
            if (MorseSequences[MorseStage][CurIndex][i] == '-') {
               Flasher.gameObject.SetActive(true);
               Hider.SetActive(false);
               yield return new WaitForSeconds(3 * UnitLength);
               Hider.SetActive(true);
               Flasher.gameObject.SetActive(false);
               yield return new WaitForSeconds(UnitLength);
            }
         }
         yield return new WaitForSeconds(3 * UnitLength);
      }
   }

   // Update is called once per frame
   void Update () {

   }
}
