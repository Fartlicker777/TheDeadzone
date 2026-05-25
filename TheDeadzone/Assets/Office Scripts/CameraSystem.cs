using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraSystem : MonoBehaviour {

   public Collider ComputerSystem;
   public GameObject CameraButtons;
   public bool InCameras;
   public int SelectedCam = 0;
   public Camera RenderCam;
   public Camera PlayerCam;
   public GameObject Reticle;

   public Vector3[] CameraPositions = new Vector3[6];
   public Vector3[] CameraRotations = new Vector3[6];

   public Vector3 InitialCamPosition = new Vector3();
   Vector3 InitialCamRotation = new Vector3();

   void Start () {
      CameraButtons.SetActive(false);
   }

   void EnterCams () {
      CameraButtons.SetActive(true);
      RenderCam.gameObject.transform.localPosition = CameraPositions[SelectedCam];
      RenderCam.gameObject.transform.localEulerAngles = CameraRotations[SelectedCam];
      PlayerCam.gameObject.transform.localPosition = CameraPositions[SelectedCam];
      PlayerCam.gameObject.transform.localEulerAngles = CameraRotations[SelectedCam];
   }

   public void UpdateSelectedCam (int i) {
      SelectedCam = i;
      CameraButtons.SetActive(true);
      RenderCam.gameObject.transform.localPosition = CameraPositions[SelectedCam];
      RenderCam.gameObject.transform.localEulerAngles = CameraRotations[SelectedCam];
      PlayerCam.gameObject.transform.localPosition = CameraPositions[SelectedCam];
      PlayerCam.gameObject.transform.localEulerAngles = CameraRotations[SelectedCam];
   }

   public void ExitCams () {
      CameraButtons.SetActive(false);
      RenderCam.gameObject.transform.localPosition = InitialCamPosition;
      RenderCam.gameObject.transform.localEulerAngles = InitialCamRotation;
      PlayerCam.gameObject.transform.localPosition = InitialCamPosition;
      PlayerCam.gameObject.transform.localEulerAngles = InitialCamRotation;
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
      Reticle.SetActive(true);
   }

   void Update () {
      if (Input.GetKeyDown(KeyCode.Alpha1)) {
         UpdateSelectedCam(0);
      }
      if (Input.GetKeyDown(KeyCode.Alpha2)) {
         UpdateSelectedCam(1);
      }
      if (Input.GetKeyDown(KeyCode.Alpha3)) {
         UpdateSelectedCam(2);
      }
      if (Input.GetKeyDown(KeyCode.Alpha4)) {
         UpdateSelectedCam(3);
      }
      if (Input.GetKeyDown(KeyCode.Alpha5)) {
         UpdateSelectedCam(4);
      }
      if (Input.GetKeyDown(KeyCode.Alpha6)) {
         UpdateSelectedCam(5);
      }
      if (Input.GetMouseButtonDown(0)) {
         Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
         if (ComputerSystem.Raycast(ray, out RaycastHit hit, 30f) && !InCameras) {
            Reticle.SetActive(false);
            InCameras = true;
            InitialCamRotation = new Vector3(RenderCam.transform.localEulerAngles.x, RenderCam.transform.localEulerAngles.y, RenderCam.transform.localEulerAngles.z);
            EnterCams();
         }
      }
      if (Input.GetMouseButtonDown(1)) {
         InCameras = false;
         ExitCams();
      }
   }
}
