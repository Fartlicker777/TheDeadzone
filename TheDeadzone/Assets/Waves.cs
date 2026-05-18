using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Waves : MonoBehaviour {
   Mesh mesh;
   Vector3[] baseVertices;
   Vector3[] vertices;

   public float amplitude = 1f;
   public float wavelength = 2f;
   public float speed = 1f;

   void Start () {
      mesh = GetComponent<MeshFilter>().mesh;
      baseVertices = mesh.vertices;
   }

   void Update () {
      vertices = new Vector3[baseVertices.Length];

      for (int i = 0; i < vertices.Length; i++) {
         Vector3 v = baseVertices[i];

         float wave = Mathf.Sin((v.x / wavelength) + Time.time * speed) * amplitude;

         v.z = wave;

         vertices[i] = v;
      }

      mesh.vertices = vertices;
      mesh.RecalculateNormals();

   }
}