using UnityEngine;

public class LightGameObjectController : MonoBehaviour
{
    private UnityEngine.Rendering.Universal.Light2D light2D;
    private Collider2D lightCollider; // Collider cho ánh sáng
    public GameObject flashLight; // Tham chiếu đến GameObject FlashLight

    private void Start()
    {
        light2D = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        light2D.enabled = false;

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.O))
        {
            if (!light2D.enabled) // Kiểm tra nếu đèn chưa bật
            {
                light2D.enabled = true;
            }
        }
        else
        {
            if (light2D.enabled)
            {
                light2D.enabled = false;
            }
        }
    }

    private void ChangeLightColor(Color color)
    {
        if (light2D != null)
        {
            light2D.color = color; // Change the light color
            ChangeFlashLightColor(color);
        }
    }

    private void ChangeFlashLightColor(Color color)
    {
        if (flashLight != null)
        {
            Renderer flashLightRenderer = flashLight.GetComponent<Renderer>();

            if (flashLightRenderer != null)
            {
                flashLightRenderer.material.color = color;
            }
        }
    }
}