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

    Texture initialTexture;

    private void Awake()
    {
        ScratchMeshRenderer = GetComponent<MeshRenderer>();

        initialTexture = ScratchMaskMaterialTexture;

        CreateTexture();
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.R))
        {
            CreateTexture();
        }
    }

    void CreateTexture()
    {
        RenderTexture rt = new RenderTexture(initialTexture.width, initialTexture.height, 0);
        RenderTexture.active = rt;
        Graphics.Blit(initialTexture, rt);

        ScratchMaskMaterialTexture = rt;
    }
}
