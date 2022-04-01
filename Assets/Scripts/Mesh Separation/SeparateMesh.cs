using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SeparateMesh : MonoBehaviour
{
    public Mesh BaseMesh;
    public int IndiceToExtract;
    public bool ExtractOnce = false;
    public bool ExtractAll = false;
    void Update()
    {
        if (ExtractOnce)
        {
            ExtractMesh(IndiceToExtract);
            ExtractOnce = false;
        }
        if (ExtractAll)
        {
            for (int i=0; i < BaseMesh.subMeshCount; i++)
            {
                ExtractMesh(i);
            }
            ExtractAll = false;
        }
    }

    private void ExtractMesh(int i)
    {
        Mesh ExtractedMesh = BaseMesh.GetSubmesh(i);
        GameObject go = new GameObject();
        go.AddComponent<MeshFilter>();
        go.AddComponent<MeshRenderer>();

        string pth = "LivingRoom/Materials/" + i.ToString();
        Material Mat = Resources.Load<Material>(pth);
        if (Mat == null)
        {
            Debug.Log("Failed to load.");
        }

        go.GetComponent<MeshFilter>().mesh = ExtractedMesh;
        go.GetComponent<MeshRenderer>().material = Mat;
        go.name = "Extracted Mesh (indice = " + i.ToString() + ")";
    }
}
