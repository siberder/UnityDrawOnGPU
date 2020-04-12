using UnityEngine;

public class MouseDrawController : MonoBehaviour
{
    public int brushSize = 10;
    public Color brushColor = Color.black;
    public Texture2D brushTexture;
    [Range(0, 1)]
    public float brushHardness = 1f;

    Camera cam;
    Material gpuDrawerMaterial;

    private void Awake()
    {
        gpuDrawerMaterial = new Material(Shader.Find("Hidden/DrawOnTexture"));
        gpuDrawerMaterial.SetTexture("_BrushTexture", brushTexture);
    }

    void Start()
    {
        cam = GetComponent<Camera>();        
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);        

        if (Physics.Raycast(ray, out var hit))
        {
            if (hit.collider.TryGetComponent<DrawZone>(out var drawZone))
            {
                var leftBot = drawZone.MeshRenderer.bounds.center - drawZone.MeshRenderer.bounds.extents;
                var rightTop = drawZone.MeshRenderer.bounds.center + drawZone.MeshRenderer.bounds.extents;

                var p1 = hit.point;

                if (Input.GetMouseButton(0))
                {
                    var pointOnTextureNormalized = new Vector3
                    {
                        x = Mathf.InverseLerp(leftBot.x, rightTop.x, p1.x),
                        y = Mathf.InverseLerp(leftBot.y, rightTop.y, p1.y),
                        z = Mathf.InverseLerp(leftBot.z, rightTop.z, p1.z),
                    };

                    drawZone.DrawTexture = DrawOnTextureGPU(drawZone.DrawTexture, pointOnTextureNormalized);
                }
            }
        }
    }

    RenderTexture DrawOnTextureGPU(Texture src, Vector2 nrmPos)
    {
        int srcWidth = src.width;
        // TODO: Optimize this
        gpuDrawerMaterial.SetVector("_BrushPosition", nrmPos);
        gpuDrawerMaterial.SetFloat("_BrushSize", brushSize / (float)srcWidth);
        gpuDrawerMaterial.SetColor("_BrushColor", brushColor);

        RenderTexture copiedTexture = new RenderTexture(srcWidth, src.height, 32);
        Graphics.Blit(src, copiedTexture, gpuDrawerMaterial);
        DestroyImmediate(src);

        return copiedTexture;
    }    
}
