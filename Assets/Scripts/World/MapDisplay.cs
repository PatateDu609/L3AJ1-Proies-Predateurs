using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Méthode de génération procédurale inspirée par la série de tutoriels de Sebastian Lague :
 * https://www.youtube.com/playlist?list=PLFt_AvWsXl0eBW2EiBtl_sxmDtSgZBxB3
 */

public class MapDisplay : MonoBehaviour
{
    public Renderer textureRenderer;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    
    /// <summary>
    /// Application des textures dans les cas de drawMode égal à NoiseMap ou ColourMap
    /// </summary>
    /// <param name="texture"></param>
    public void DrawTexture(Texture2D texture)
    {
        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }

    /// <summary>
    /// Application des textures dans les cas de drawMode égal à Mesh
    /// </summary>
    /// <param name="texture"></param>
    public void DrawMesh(MeshData meshData, Texture2D texture)
    {
        meshFilter.sharedMesh = meshData.CreateMesh();
        meshRenderer.sharedMaterial.mainTexture = texture;
    }
}
