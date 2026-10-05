using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
/*
public static class JSONUtility {

   static int Night = 0;
   static int[] Enemies = { 0, 0, 0, 0, 0, 0, 0 };
   static float time = 1;
   static bool[] ChalComplete = { false, false, false, false, false, false, false, false, false };
   //                             com    ed     a      b      art    off    emb    mod    QM

   static bool[] BonusComplete = { false, false, false, false, false, false, false, false, false, false, false, false };

   public static void AddBonusCompletion (int index) {
      BinaryFormatter formatter = new BinaryFormatter();
      string path = Application.persistentDataPath + "/FNWTC_Save.boner";
      FileStream stream = new FileStream(path, FileMode.Create);

      BonusComplete[index] = true;

      PlayerData data = new PlayerData(Night, Enemies, time, ChalComplete, BonusComplete);
      //Debug.Log(4);

      //Debug.Log(data.BonusModesComplete()[index]);

      formatter.Serialize(stream, data);
      stream.Close();
   }

   public static void AddChallengeCompletion (int index) {
      BinaryFormatter formatter = new BinaryFormatter();
      string path = Application.persistentDataPath + "/FNWTC_Save.boner";
      FileStream stream = new FileStream(path, FileMode.Create);
      //Debug.Log(2);
      ChalComplete[index] = true; // Have to do this for it to be set fsr.
      if (ChalComplete[8])
         ChalComplete[7] = true;
      if (ChalComplete[7])
         ChalComplete[6] = true;
      //Debug.Log(3);
      PlayerData data = new PlayerData(Night, Enemies, time, ChalComplete, BonusComplete);
      //Debug.Log(4);
      formatter.Serialize(stream, data);
      stream.Close();
   }

   public static void NewBestTime (float Time) {
      BinaryFormatter formatter = new BinaryFormatter();
      string path = Application.persistentDataPath + "/FNWTC_Save.boner";
      FileStream stream = new FileStream(path, FileMode.Create);

      time = Time; // Have to do this for it to be set fsr.

      PlayerData data = new PlayerData(Night, Enemies, time, ChalComplete, BonusComplete);

      formatter.Serialize(stream, data);
      stream.Close();
   }

   public static void IncrementDeath (string Enemy) {
      BinaryFormatter formatter = new BinaryFormatter();
      string path = Application.persistentDataPath + "/FNWTC_Save.boner";
      FileStream stream = new FileStream(path, FileMode.Create);

      //  Debug.Log(-2);
      string[] x = { "Ba", "Br", "Bu", "Co", "Gh", "Hy", "Ma" };

      // Debug.Log(-1);
      if (Array.IndexOf(x, Enemy) == -1) {
         return;
      }
      // Debug.Log(0);
      Enemies[Array.IndexOf(x, Enemy)] += 1;

      //Debug.Log(1);
      PlayerData data = new PlayerData(Night, Enemies, time, ChalComplete, BonusComplete);
      //Debug.Log(2);
      formatter.Serialize(stream, data);
      // Debug.Log(3);
      stream.Close();
   }

   public static void EraseData () {
      BinaryFormatter formatter = new BinaryFormatter();
      string path = Application.persistentDataPath + "/FNWTC_Save.boner";
      FileStream stream = new FileStream(path, FileMode.Create);

      Night = 1;
      time = 0;
      Enemies = new int[] { 0, 0, 0, 0, 0, 0, 0 };
      ChalComplete = new bool[] { false, false, false, false, false, false, false, false, false };
      BonusComplete = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false };

      PlayerData data = new PlayerData(Night, Enemies, time, ChalComplete, BonusComplete);

      formatter.Serialize(stream, data);
      stream.Close();
   }

   public static void SaveData (int NightNumber) {
      BinaryFormatter formatter = new BinaryFormatter();
      string path = Application.persistentDataPath + "/FNWTC_Save.boner";
      FileStream stream = new FileStream(path, FileMode.Create);

      Night = NightNumber;

      PlayerData data = new PlayerData(Night, Enemies, time, ChalComplete, BonusComplete);

      formatter.Serialize(stream, data);
      stream.Close();
   }

   public static PlayerData LoadData () {
      string path = Application.persistentDataPath + "/FNWTC_Save.boner";
      if (File.Exists(path)) {
         BinaryFormatter formatter = new BinaryFormatter();
         FileStream stream = new FileStream(path, FileMode.Open);

         PlayerData data = formatter.Deserialize(stream) as PlayerData;


         stream.Close();

         return data;
      }
      else {
         Debug.LogError("File not found in " + path);
         return new PlayerData(1, new int[] { 0, 0, 0, 0, 0, 0, 0 }, 0f, new bool[] { false, false, false, false, false, false, false, false, false }, new bool[] { false, false, false, false, false, false, false, false, false, false, false, false });
      }
   }

   public static void LoadDataStart () {
      string path = Application.persistentDataPath + "/FNWTC_Save.boner";
      if (File.Exists(path)) {
         BinaryFormatter formatter = new BinaryFormatter();
         FileStream stream = new FileStream(path, FileMode.Open);

         //Debug.Log(-1);
         //Debug.Log(stream.Length);

         PlayerData data;

         if (stream.Length == 0) {
            data = new PlayerData(1, new int[] { 0, 0, 0, 0, 0, 0, 0 }, 0f, new bool[] { false, false, false, false, false, false, false, false, false }, new bool[] { false, false, false, false, false, false, false, false, false, false, false, false });
         }
         else {
            data = formatter.Deserialize(stream) as PlayerData;
         }


         Night = data.Night();
         Enemies = data.EnemyDeath();
         time = data.SurvTime();
         ChalComplete = data.ChallengesComplete();
         BonusComplete = data.BonusModesComplete();
         stream.Close();
      }
      else {
         Debug.LogError("File not found in " + path);
         //SaveData(1);
      }
   }

}
*/