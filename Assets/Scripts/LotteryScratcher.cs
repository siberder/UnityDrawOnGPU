using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotteryScratcher : MonoBehaviour
{
    public int brushSize = 10;
    public Color color = Color.black;
    public AnimationCurve brushHardness = AnimationCurve.Linear(0, 0, 1, 1);

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

                    DrawOnTexture(pointOnTextureNormalized, size, scratchZone.ScratchMaskTexture, brushHardness);
                }
            }
        }
    }

    void DrawOnTexture(Vector2 nrmPos, float size, Texture2D texture, AnimationCurve brushHardness)
    {
        var width = texture.width;
        var height = texture.height;

        var px = Mathf.RoundToInt(nrmPos.x * width);
        var py = Mathf.RoundToInt(nrmPos.y * height);

        var cols = texture.GetPixels();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var distance = Vector2.Distance(new Vector2(x, y), new Vector2(px, py));
                if (distance <= size)
                {
                    float hardnessT = 1f - (distance / size);
                    float hardness = brushHardness.Evaluate(hardnessT);
                    cols[width * y + x] *= Color.white * hardness;
                }
            }
        }

        texture.SetPixels(cols);
        texture.Apply();
    }
}
