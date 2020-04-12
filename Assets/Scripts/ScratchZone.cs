using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Collider))]
public class ScratchZone : MonoBehaviour
{
    private const string MASK_TEX_NAME = "_MaskTex";
    private const string ICON_TEX_NAME = "_IconTex";

    public MeshRenderer ScratchMeshRenderer { get; private set; }
    public Texture ScratchMaskTexture
    {
        get => ScratchMeshRenderer.material.GetTexture(MASK_TEX_NAME);
        set => ScratchMeshRenderer.material.SetTexture(MASK_TEX_NAME, value);
    }

    public Texture IconTexture
    {
        get => ScratchMeshRenderer.material.GetTexture(ICON_TEX_NAME);
        set => ScratchMeshRenderer.material.SetTexture(ICON_TEX_NAME, value);
    }

    Texture initialTexture;

    private void Awake()
    {
        ScratchMeshRenderer = GetComponent<MeshRenderer>();

        initialTexture = ScratchMaskTexture;

        CreateTexture();
    }    

    public void CreateTexture()
    {
        RenderTexture rt = new RenderTexture(initialTexture.width, initialTexture.height, 0);
        RenderTexture.active = rt;
        Graphics.Blit(initialTexture, rt);

        ScratchMaskTexture = rt;
    }
}
