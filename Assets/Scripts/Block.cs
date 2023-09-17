using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Block : MonoBehaviour
{
    public GameObject go;
    public Material material0;
    public Material material1;
    public Material material2;
    public Material material3;
    public Material material4;
    public Material material5;
    private List<Material> Materials;
    private int materialCount;
    // Start is called before the first frame update
    void Start()
    {
        Materials = new List<Material>() { material0, material1, material2, material3, material4, material5 };

        go = gameObject;
        go.GetComponent<MeshRenderer>().material = Materials[0];
        materialCount = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void MaterialAdd(Vector3 MaterialPosition)
    {
        if( materialCount < 5 && transform.position == MaterialPosition )
        {
            materialCount++;
            go.GetComponent<MeshRenderer>().material = Materials[materialCount];
        }
    }

    void MaterialMinus(Vector3 MaterialPosition)
    {
        if( materialCount > 0 && transform.position == MaterialPosition )
        {
            materialCount--;
            go.GetComponent<MeshRenderer>().material = Materials[materialCount];
        }
    }
}
