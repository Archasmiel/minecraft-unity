using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossbarInvertEffect : MonoBehaviour {
    public float thickness = 4f;
    public float size = 50f;
    public Material crossbarMaterial;

    private void Start() {
        crossbarMaterial.SetFloat("_Thickness", thickness);
        crossbarMaterial.SetFloat("_Size", size);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination) {
        crossbarMaterial.SetFloat("_Width", Screen.width);
        crossbarMaterial.SetFloat("_Height", Screen.height);

        Graphics.Blit(source, destination, crossbarMaterial);
    }
}
