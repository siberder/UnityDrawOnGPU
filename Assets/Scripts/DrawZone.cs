using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(BoxCollider))]
public class DrawZone : MonoBehaviour
{
    public MeshRenderer MeshRenderer { get; private set; }

    public virtual Texture DrawTexture
    {
        get => MeshRenderer.material.mainTexture;
        set => MeshRenderer.material.mainTexture = value;
    }

    Texture initialTexture;

    protected virtual void Awake()
    {
        MeshRenderer = GetComponent<MeshRenderer>();

        initialTexture = DrawTexture;

        CreateTexture();
    }

    public void CreateTexture()
    {
        if(!initialTexture)
        {
            initialTexture = new Texture2D(1024, 1024);
            var tex2D = (Texture2D)initialTexture;
            var fillColorArray = tex2D.GetPixels();

            for (var i = 0; i < fillColorArray.Length; ++i)
            {
                fillColorArray[i] = Color.white;
            }

            tex2D.SetPixels(fillColorArray);
            tex2D.Apply();
        }

        RenderTexture rt = new RenderTexture(initialTexture.width, initialTexture.height, 0);
        RenderTexture.active = rt;
        Graphics.Blit(initialTexture, rt);

        DrawTexture = rt;
    }
}
