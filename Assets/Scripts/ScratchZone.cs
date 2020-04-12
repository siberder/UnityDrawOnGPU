using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Collider))]
public class ScratchZone : MonoBehaviour
{
    private const string MASK_TEX_NAME = "_MaskTex";
    
    public MeshRenderer ScratchMeshRenderer { get; private set; }
    public Texture ScratchMaskMaterialTexture
    {
        get => ScratchMeshRenderer.material.GetTexture(MASK_TEX_NAME);
        set => ScratchMeshRenderer.material.SetTexture(MASK_TEX_NAME, value);
    }

    private void Awake()
    {
        ScratchMeshRenderer = GetComponent<MeshRenderer>();

        var initialTexture = ScratchMaskMaterialTexture;

        RenderTexture rt = new RenderTexture(initialTexture.width, initialTexture.height, 0);
        RenderTexture.active = rt;
        Graphics.Blit(initialTexture, rt);

        ScratchMaskMaterialTexture = rt;
    }
}
