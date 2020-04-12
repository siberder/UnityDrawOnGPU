using System.Collections;
using UnityEngine;

public class PrizeIcons : MonoBehaviour
{
    public ScratchZone[] scratchZones;
    public Texture2D[] prizeIcons;

    private void Start()
    {
        SetRandomIcons();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            SetRandomIcons();
        }
    }

    public void SetRandomIcons()
    {
        foreach (var item in scratchZones)
        {
            item.IconTexture = prizeIcons[Random.Range(0, prizeIcons.Length)];
            item.CreateTexture();
        }        
    }
}
