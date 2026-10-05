using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using rnd = UnityEngine.Random;

public class MorseCodeFlasher : MonoBehaviour {

   public AnswerInput AnsInp;
   public WindowBlind Window;
   public MainGame Game;

   float UnitLength = .2f;

   public int MorseStage = 0;
   public int LetterIndex = 0;

   string[] MorseLetters = { ".-", "-...", "-.-.", "-..", ".", "..-.", "--.", "....", "..", ".---", "-.-", ".-..", "--", "-.", "---", ".--.", "--.-", ".-.", "...", "-", "..-", "...-", ".--", "-..-", "-.--", "--.." };
   string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
   public Light Flasher;
   public GameObject Hider;

   Coroutine FlashMorseCor;
   Coroutine InputComparisonCor;
   Coroutine FlashMenu;
   public float WaitTime = 1f;

   int[] ChosenLetters = new int[25];

   string[][] MorseSequences = new string[][] { new string[] { "", "", "", "", "" }, new string[] { "", "", "", "", "" }, new string[] { "", "", "", "", "" }, new string[] { "", "", "", "", "" }, new string[] { "", "", "", "", "" } };
   public string[] StageAnswers = new string[] { "", "", "", "", "" };

   bool WaitForLetterReset;

   void Start () {
      Flasher.gameObject.SetActive(false);
      Hider.SetActive(true);
      StartCoroutine(MainMenuFlash("Memento Mori"));
   }

   public void InitializeMorse () {
      for (int i = 0; i < 25; i++) {
         ChosenLetters[i] = rnd.Range(0, 26);
         MorseSequences[i / 5][i % 5] = MorseLetters[ChosenLetters[i]].ToString();
         StageAnswers[i / 5] += Alphabet[ChosenLetters[i]].ToString();
      }

      for (int i = 0; i < 5; i++) {
         Debug.Log(StageAnswers[i]);
      }
      FlashMorseCor = StartCoroutine(Sequence());
   }

   public void CompareInput (string q) {
      InputComparisonCor = StartCoroutine(Comp(q));
   }

   public void Reset () {
      for (int i = 0; i < 5; i++) {
         StageAnswers[i] = "";
      }
      if (InputComparisonCor != null) {
         StopCoroutine(InputComparisonCor);
      }
      if (FlashMorseCor != null) {
         StopCoroutine(FlashMorseCor);
      }
      MorseStage = 0;
      LetterIndex = 0;
   }

   IEnumerator Comp (string q) {
      yield return new WaitForSeconds(WaitTime);
      if (q == "RESET") {
         Game.HandleDeath("The Fool");
      }
      if (q == StageAnswers[MorseStage]) {
         AnsInp.UpdateLEDColors(MorseStage);
         if (MorseStage == 4) {
            StopCoroutine(FlashMorseCor);
         }
         else {
            MorseStage++;
            Game.ProcessStageAdvance(MorseStage);
            LetterIndex = 0;
         }
      }
      AnsInp.ResetUserInput();
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
         int CurStage = MorseStage;

         for (int i = 0; i < MorseSequences[CurStage][CurIndex].Length; i++) {
            if (MorseSequences[CurStage][CurIndex][i] == '.') {
               if (!Window.ClosedWindow) {
                  Flasher.gameObject.SetActive(true);
               }
               Hider.SetActive(false);
               yield return new WaitForSeconds(UnitLength);
               Hider.SetActive(true);
               Flasher.gameObject.SetActive(false);
               yield return new WaitForSeconds(UnitLength);
            }
            if (MorseSequences[CurStage][CurIndex][i] == '-') {
               if (!Window.ClosedWindow) {
                  Flasher.gameObject.SetActive(true);
               }
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

   public void StopMainMenuFlash () {
      Hider.SetActive(true);
      StopAllCoroutines();
      StopCoroutine(FlashMenu);
   }

   IEnumerator MainMenuFlash (string input) {
      input = input.Replace(" ", "^").ToUpper();
      string BeginningSequence = "";
      for (int i = 0; i < input.Length; i++) {
         if (input[i] == '^') {
            BeginningSequence += "|||||||";
         }
         for (int j = 0; j < 26; j++) {
            if (input[i] == Alphabet[j]) {
               BeginningSequence += MorseLetters[j];
               BeginningSequence += "|||";
            }
         }
         BeginningSequence += "||";
      }

      while (!Game.Ingame) {
         for (int i = 0; i < BeginningSequence.Length; i++) {
            if (BeginningSequence[i] == '.') {
               Hider.SetActive(false);
               yield return new WaitForSeconds(UnitLength);
               Hider.SetActive(true);
               Flasher.gameObject.SetActive(false);
               yield return new WaitForSeconds(UnitLength);
            }
            if (BeginningSequence[i] == '-') {
               Hider.SetActive(false);
               yield return new WaitForSeconds(3 * UnitLength);
               Hider.SetActive(true);
               Flasher.gameObject.SetActive(false);
               yield return new WaitForSeconds(UnitLength);
            }
            if (BeginningSequence[i] == '|') {
               yield return new WaitForSeconds(UnitLength);
            }
         }

         yield return new WaitForSeconds(7 * UnitLength);
      }
   }

   // Update is called once per frame
   void Update () {
      if (!Game.Ingame && FlashMenu == null) {
         FlashMenu = StartCoroutine(MainMenuFlash("Memento Mori"));
      }
   }
}
