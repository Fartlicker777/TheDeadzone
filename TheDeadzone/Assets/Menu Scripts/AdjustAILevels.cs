using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class AdjustAILevels : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {

   public TextMeshProUGUI AILevelDisplay;
   public TextMeshProUGUI Description;

   public string Instruction;

   public int AILevel;
   bool Focused;


   void Start () {

   }

   public void OnPointerDown (PointerEventData eventData) {

   }

   public void OnPointerEnter (PointerEventData eventData) {
      Focused = true;
      Description.text = Instruction;
   }

   public void OnPointerExit (PointerEventData eventData) {
      Focused = false;
      Description.text = "Scroll to adjust AI values/hazard level. All future hazards retain previous hazard buffs.";
   }

   void Update () {
      if (Input.GetAxis("Mouse ScrollWheel") > 0f && Focused && AILevel < 20) {
         AILevel++;
      }
      else if (Input.GetAxis("Mouse ScrollWheel") < 0f && Focused && AILevel > 0) {
         AILevel--;
      }
      AILevelDisplay.text = AILevel.ToString();
   }
}
