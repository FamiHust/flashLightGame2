using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum LightType { Freeze, Spawn, Shrink, Default }

public class LightController : MonoBehaviour
{
    public GameSkillSelectionMenu skillSelectionMenu;
    private Controller playerController;
    private UnityEngine.Rendering.Universal.Light2D light2D;
    private Collider2D lightCollider; // Collider cho ánh sáng

    [SerializeField] private float lightFadeSpeed = 1f; // Tốc độ giảm độ sáng
    [SerializeField] private float holdDuration = 5f; // Thời gian giữ phím để tiêu diệt kẻ thù
    [SerializeField] private int currentLevel = 1; // Biến để theo dõi level hiện tại
    private bool isFlashlightSoundPlaying = false;
    private bool isFlashlightOn = false;
    public LightType currentLightType;
    public GameObject flashLight;
    private bool isOpenFlash = false;

    private float holdTime = 0f; // Thời gian đã giữ phím

    private void Start()
    {
        playerController = FindObjectOfType<Controller>();

        light2D = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        light2D.enabled = false;

        lightCollider = GetComponent<Collider2D>();
        lightCollider.enabled = false;

        // Kiểm tra level hiện tại và thiết lập loại đèn
        if (currentLevel == 1)
        {
            currentLightType = LightType.Default; // Đặt loại đèn là Default cho level 1
            ChangeLightColor(GetColorByLightType(currentLightType)); // Đặt màu cho loại đèn Default
        }
        else
        {
            // Lấy loại ánh sáng đã chọn từ PlayerPrefs cho các level khác
            int lightTypeIndex = PlayerPrefs.GetInt("SelectedLightType", (int)LightType.Default); // Mặc định là Default
            currentLightType = (LightType)lightTypeIndex;

            // Lấy màu sắc từ PlayerPrefs
            float r = PlayerPrefs.GetFloat("LightColorR", 1f); // Mặc định là trắng
            float g = PlayerPrefs.GetFloat("LightColorG", 1f);
            float b = PlayerPrefs.GetFloat("LightColorB", 1f);
            Color lightColor = new Color(r, g, b);

            ChangeLightColor(lightColor); // Đặt màu cho đèn
        }
    }
    private void Update()
    {
        // Kiểm tra nếu người chơi đã chọn loại đèn
        if (!light2D.enabled && playerController.currentBattery > 0)
        {
            light2D.enabled = true;
            lightCollider.enabled = true;
        }

        if (isOpenFlash && playerController.currentBattery > 0)
        {
            holdTime += Time.deltaTime;
            if (!light2D.enabled)
            {
                light2D.enabled = true;
                lightCollider.enabled = true;
            }
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

    public void TurnFlash()
    {
        isOpenFlash = !isOpenFlash;
    }

    public LightType CurrentLightType
    {
        get { return currentLightType; }
    }

    public void SelectLightType(LightType lightType)
    {
        currentLightType = lightType;
        ChangeLightColor(GetColorByLightType(lightType));
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
                    case LightType.Default:
                        // Không làm gì khi chiếu ánh sáng Default
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
        enemy.transform.position = new Vector2(Random.Range(-13f, -13f), Random.Range(-7f, -7f));
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
            light2D.color = color; // Thay đổi màu của đèn
            ChangeFlashLightColor(color); // Thay đổi màu của FlashLight
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

    private Color GetColorByLightType(LightType lightType)
    {
        switch (lightType)
        {
            case LightType.Freeze:
                return Color.blue;
            case LightType.Shrink:
                return Color.red;
            case LightType.Spawn:
                return Color.green;
            case LightType.Default:
            default:
                return Color.white; // Màu cho loại đèn Default
        }
    }


}