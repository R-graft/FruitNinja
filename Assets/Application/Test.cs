using System;
using System.Linq;
using UnityEngine;

public class Test : MonoBehaviour
{

    public SpriteRenderer inn;

    public SpriteRenderer outt;

    public MeshFilter tempMesh;

    private void Start()
    {
        SpriteToMesh(inn, tempMesh);

        MeshToSprite(tempMesh, outt);
    }
    void SpriteToMesh(SpriteRenderer inRenderer, MeshFilter meshFilter)
    {
        Sprite sprite = inRenderer.sprite;

        Mesh mesh = new Mesh();
        mesh.SetVertices(Array.ConvertAll(sprite.vertices, i => (Vector3)i).ToList());
        mesh.SetUVs(0, sprite.uv.ToList());
        mesh.SetTriangles(Array.ConvertAll(sprite.triangles, i => (int)i), 0);

        tempMesh.mesh = mesh;

        meshFilter.mesh = mesh;
    }
    void MeshToSprite(MeshFilter meshFilter, SpriteRenderer Outt)
    {
        Mesh mesh = meshFilter.mesh;

        Sprite spr = Outt.GetComponent<SpriteRenderer>().sprite;

        var copiedVerticies = new Vector2[mesh.vertices.Length];

        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            copiedVerticies[i] = new Vector2(mesh.vertices[i].x, mesh.vertices[i].y);
        }

        for (int i = 0; i < copiedVerticies.Length; ++i)
        {
            copiedVerticies[i] = (copiedVerticies[i] * spr.pixelsPerUnit) + spr.pivot;

        }

        var copiedTriangels = new ushort[mesh.triangles.Length];

        for (int i = 0; i < mesh.triangles.Length; i++)
        {
            copiedTriangels[i] = (ushort)mesh.triangles[i];
        }

        spr.OverrideGeometry(copiedVerticies, copiedTriangels);

        Outt.sprite = spr;
    }

    void HalfMash(Mesh mesh, MeshFilter one, MeshFilter two)
    {

    }
    private void OnDrawGizmosSelected()
    {
        SpriteToMesh(inn, tempMesh);

        MeshToSprite(tempMesh, outt);

    }

}
//private Mesh SpriteToMesh2(Sprite sprite)
//{


//    return mesh;
//}


//Mesh mesh = new Mesh();
//mesh.vertices = Array.ConvertAll(sprite.vertices, i => (Vector3)i);
//mesh.uv = sprite.uv;
//mesh.triangles = Array.ConvertAll(sprite.triangles, i => (int)i);