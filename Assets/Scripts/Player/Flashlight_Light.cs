using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum LightType
{
    Freeze,
    Spawn,
    Shrink
}

public class LightController : MonoBehaviour
{
    private Controller playerController;
    private UnityEngine.Rendering.Universal.Light2D light2D;
    private Collider2D lightCollider; // Collider cho ánh sáng

    [SerializeField] private float lightFadeSpeed = 1f; // Tốc độ giảm độ sáng
    [SerializeField] private float holdDuration = 5f; // Thời gian giữ phím để tiêu diệt kẻ thù
    [SerializeField] private LightType lightType; // Loại đèn
    private LightType currentLightType; // Loại đèn hiện tại
    public GameObject flashLight; // Tham chiếu đến GameObject FlashLight

    private float holdTime = 0f; // Thời gian đã giữ phím

    private void Start()
    {
        playerController = FindObjectOfType<Controller>();

        light2D = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        light2D.enabled = false;

        lightCollider = GetComponent<Collider2D>();
        lightCollider.enabled = false;

        currentLightType = LightType.Spawn;
        ChangeLightColor(Color.green);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentLightType = LightType.Freeze;
            ChangeLightColor(Color.blue);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentLightType = LightType.Spawn;
            ChangeLightColor(Color.green);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentLightType = LightType.Shrink;
            ChangeLightColor(Color.red);
        }

        if (Input.GetKey(KeyCode.O) && playerController.currentBattery > 0)
        {
            if (!light2D.enabled) // Kiểm tra nếu đèn chưa bật
            {
                light2D.enabled = true;
                lightCollider.enabled = true;
                SoundManager.PlaySound(SoundType.FLASHLIGHT); // Gọi âm thanh khi bật đèn
            }

            holdTime += Time.deltaTime;
            CheckForEnemies();
        }
        else
        {
            if (light2D.enabled)
            {
                light2D.enabled = false;
                lightCollider.enabled = false;
            }
            holdTime = 0f;
        }
    }

    public LightType CurrentLightType
    {
        get { return currentLightType; }
    }

    private void CheckForEnemies()
    {
        Collider2D[] hitColliders = new Collider2D[10];
        int hitCount = lightCollider.Overlap(new ContactFilter2D(), hitColliders);

        for (int i = 0; i < hitCount; i++)
        {
            if (hitColliders[i] != null && hitColliders[i].CompareTag("Enemy"))
            {
                // Kiểm tra xem GameObject có tag "FinishLine" không
                if (hitColliders[i].CompareTag("FinishLine"))
                {
                    continue; // Bỏ qua nếu là FinishLine
                }

                // Gọi phương thức tương ứng dựa trên loại đèn hiện tại
                switch (currentLightType)
                {
                    case LightType.Freeze:
                        FreezeEnemy(hitColliders[i]);
                        hitColliders[i].GetComponent<Enemy>().ChangeColor(Color.blue);
                        break;
                    case LightType.Shrink:
                        ShrinkEnemy(hitColliders[i]);
                        hitColliders[i].GetComponent<Enemy>().ChangeColor(Color.red);
                        break;
                    case LightType.Spawn:
                        SpawnEnemy(hitColliders[i]);
                        //hitColliders[i].GetComponent<Enemy>().ChangeColor(Color.green);
                        break;
                }
            }
        }
    }

    private void FreezeEnemy(Collider2D enemy)
    {
        float freezeDuration = 5f; // Thay đổi giá trị này để điều chỉnh thời gian đóng băng
        enemy.GetComponent<Enemy>().Freeze(freezeDuration);
    }

    private void SpawnEnemy(Collider2D enemy)
    {
        enemy.transform.position = new Vector2(Random.Range(-25f, -13f), Random.Range(-8f, 7f));
    }

    private void ShrinkEnemy(Collider2D enemy)
    {
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        if (enemyScript != null && !enemyScript.IsShrunk()) // Kiểm tra nếu enemy tồn tại và chưa co lại
        {
            enemyScript.Shrink(); // Gọi phương thức Shrink để co lại

        }
    }

    private void ChangeLightColor(Color color)
    {
        if (light2D != null)
        {
            light2D.color = color; // Change the light color
            ChangeFlashLightColor(color);
            //ChangeLightGameObjectColor(color);
            //ChangeCubeColor(color);
        }
    }

    private void ChangeFlashLightColor(Color color)
    {
        if (flashLight != null)
        {
            Renderer flashLightRenderer = flashLight.GetComponent<Renderer>();

            if (flashLightRenderer != null)
            {
                flashLightRenderer.material.color = color; // Thay đổi màu của FlashLight
            }
        }
    }

    //     private void ChangeCubeColor(Color color) // đổi màu GameObject con
    //     {
    //         if (flashLight != null)
    //         {
    //             Renderer[] childRenderers = flashLight.GetComponentsInChildren<Renderer>();
    //             foreach (Renderer childRenderer in childRenderers)
    //             {
    //                 childRenderer.material.color = color;
    //             }
    //         }
    //     }
    // }
}