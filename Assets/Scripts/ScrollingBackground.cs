using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float speed;

    public Renderer bgRenderer;

    void Update()
    {
        bgRenderer.material.mainTextureOffset -= new Vector2(0, speed * Time.deltaTime);
    }
}
