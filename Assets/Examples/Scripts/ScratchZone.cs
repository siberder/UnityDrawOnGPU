using UnityEngine;


public class ScratchZone : DrawZone
{
    private const string MASK_TEX_NAME = "_MaskTex";
    private const string ICON_TEX_NAME = "_IconTex";

    public override Texture DrawTexture
    {
        get => MeshRenderer.material.GetTexture(MASK_TEX_NAME);
        set => MeshRenderer.material.SetTexture(MASK_TEX_NAME, value);
    }

    public Texture IconTexture
    {
        get => MeshRenderer.material.GetTexture(ICON_TEX_NAME);
        set => MeshRenderer.material.SetTexture(ICON_TEX_NAME, value);
    }
}
