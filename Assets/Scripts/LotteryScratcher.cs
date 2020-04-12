using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotteryScratcher : MonoBehaviour
{
    public int brushSize = 10;
    public Material gpuDrawerMaterial;
    public Texture2D brushTexture;
    [Range(0, 1)]
    public float brushHardness = 1f;

    Camera cam;

    void Start()
    {
        cam = Camera.main;        
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);        

        if (Physics.Raycast(ray, out var hit))
        {
            if (hit.collider.TryGetComponent<ScratchZone>(out var scratchZone))
            {
                var leftBot = scratchZone.ScratchMeshRenderer.bounds.center - scratchZone.ScratchMeshRenderer.bounds.extents;
                var rightTop = scratchZone.ScratchMeshRenderer.bounds.center + scratchZone.ScratchMeshRenderer.bounds.extents;

                var p1 = hit.point;

                var size = (Input.GetKey(KeyCode.LeftShift)) ? brushSize * 3 : brushSize;

                if (Input.GetMouseButton(0))
                {
                    var pointOnTextureNormalized = new Vector3
                    {
                        x = Mathf.InverseLerp(leftBot.x, rightTop.x, p1.x),
                        y = Mathf.InverseLerp(leftBot.y, rightTop.y, p1.y),
                        z = Mathf.InverseLerp(leftBot.z, rightTop.z, p1.z),
                    };

                    scratchZone.ScratchMaskMaterialTexture = DrawOnTextureGPU(scratchZone.ScratchMaskMaterialTexture, pointOnTextureNormalized);
                }
            }
        }
    }

    RenderTexture DrawOnTextureGPU(Texture src, Vector2 nrmPos)
    {
        gpuDrawerMaterial.SetVector("_BrushPosition", nrmPos);
        RenderTexture copiedTexture = new RenderTexture(src.width, src.height, 32);
        Graphics.Blit(src, copiedTexture, gpuDrawerMaterial);

        return copiedTexture;
    }
}
