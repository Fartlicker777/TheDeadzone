using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using rnd = UnityEngine.Random;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class AnswerInput : MonoBehaviour {

   public Collider InputDisplay;
   public MainGame Game;
   public MorseCodeFlasher MCF;
   public TextMeshPro InputTMP;

   public GameObject[] StageLEDs;
   public Material OffColor;
   public Material OnColor;

   string UserInput = "";

   bool CanToggleInputtingAnswer = true;

   string QWERTY = "QWERTYUIOPASDFGHJKLZXCVBNM";

   void OnEnable () {
      Keyboard.current.onTextInput += OnTextInput;
   }

   private void OnDisable () {
      Keyboard.current.onTextInput -= OnTextInput;
   }

   public void UpdateLEDColors (int LEDIndex) {
      StageLEDs[LEDIndex].GetComponent<MeshRenderer>().material = OnColor;
   }

   void OnTextInput (char ch) {
      if (Game.InputtingAnswer && UserInput.Length < 5) {
         ch = Char.ToUpper(ch);
         if (QWERTY.Contains(ch)) {
            UserInput += ch.ToString();
            if (UserInput.Length == 5) {
               MCF.CompareInput(UserInput);
               CanToggleInputtingAnswer = false;
               Game.InputtingAnswer = false;
            }
            InputTMP.text += ch.ToString() + " ";
         }
      }
   }

   public void ResetUserInput () {
      UserInput = "";
      CanToggleInputtingAnswer = true;
      InputTMP.text = "";
   }

   void Update () {
      if (Input.GetMouseButtonDown(0)) {
         Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));

         if (InputDisplay.Raycast(ray, out RaycastHit hit, 30f) && CanToggleInputtingAnswer) {
            Game.InputtingAnswer ^= true;
         }
      }
   }
}
