using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Collider))]
public class ScratchZone : MonoBehaviour
{
    private const string MASK_TEX_NAME = "_MaskTex";
    
    public MeshRenderer ScratchMeshRenderer { get; private set; }
    public Texture2D ScratchMaskTexture { get; private set; }

    private void Awake()
    {
        ScratchMeshRenderer = GetComponent<MeshRenderer>();
        var texture = ((Texture2D)ScratchMeshRenderer.material.GetTexture(MASK_TEX_NAME));
        ScratchMaskTexture = new Texture2D(texture.width, texture.height);
        ScratchMaskTexture.SetPixels(texture.GetPixels());
        ScratchMaskTexture.Apply();
        ScratchMeshRenderer.material.SetTexture(MASK_TEX_NAME, ScratchMaskTexture);
    }
}
