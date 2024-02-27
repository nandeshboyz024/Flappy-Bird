using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public float animationSpeed = 1.0f;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    // Update is called once per frame
    void Update()
    {   
        if (meshRenderer != null)
        {
            meshRenderer.material.mainTextureOffset += new Vector2(animationSpeed*Time.deltaTime, 0);
        }
        
    }
}
