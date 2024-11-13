using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    private bool isFrozen = false; // Trạng thái đóng băng
    private bool isShrunk = false; // Trạng thái co lại
    private Renderer enemyRenderer; // Tham chiếu đến Renderer
    private float freezeDuration = 5f; // Thời gian đóng băng
    private float freezeTimer = 0f; // Bộ đếm thời gian đóng băng
    private float shrinkDuration = 5f; // Thời gian co lại
    private float shrinkTimer = 0f; // Bộ đếm thời gian co lại
    private Vector3 originalScale;
    private Color defaultColor;


    private void Start()
    {
        enemyRenderer = GetComponent<Renderer>(); // Lấy tham chiếu đến Renderer
        defaultColor = enemyRenderer.material.color;
        originalScale = transform.localScale;
    }

    private void Update()
    {
        if (isFrozen)
        {
            freezeTimer += Time.deltaTime; // Cập nhật bộ đếm thời gian
            if (freezeTimer >= freezeDuration)
            {
                Unfreeze(); // Giải phóng khi hết thời gian
            }
            return;
        }

        if (isShrunk)
        {
            shrinkTimer += Time.deltaTime; // Cập nhật bộ đếm thời gian co lại
            if (shrinkTimer >= shrinkDuration)
            {
                Unshrink(); // Khôi phục kích thước khi hết thời gian co lại
            }
        }
    }

    public bool IsFrozen()
    {
        return isFrozen; // Trả về trạng thái đóng băng
    }

    public bool IsShrunk()
    {
        return isShrunk; // Trả về trạng thái co lại
    }

    public void Freeze(float duration)
    {
        isFrozen = true;
        freezeDuration = duration; // Cập nhật thời gian đóng băng
        freezeTimer = 0f; // Đặt lại bộ đếm thời gian
        GetComponent<Enemy_Movement>().StopMovement(); // Dừng chuyển động
    }

    public void Unfreeze()
    {
        isFrozen = false;
        freezeTimer = 0f; // Đặt lại bộ đếm thời gian
        ChangeColor(defaultColor);
        GetComponent<Enemy_Movement>().ResumeMovement(); // Khôi phục chuyển động
    }

    public void Shrink()
    {
        if (!isShrunk) // Kiểm tra nếu chưa co lại
        {
            isShrunk = true; // Đánh dấu là đã co lại
            shrinkTimer = 0f; // Đặt lại bộ đếm thời gian co lại
            transform.localScale *= 0.5f;
            ChangeColor(Color.red);
            GetComponent<Enemy_Movement>().SetSpeed(0.4f);

            // Bắt đầu coroutine để khôi phục tốc độ sau 5 giây
            StartCoroutine(RestoreSpeedAfterShrink());
        }
    }

    private IEnumerator RestoreSpeedAfterShrink()
    {
        yield return new WaitForSeconds(5f); // Chờ 5 giây
        Unshrink(); // Khôi phục kích thước
        GetComponent<Enemy_Movement>().ResetSpeed(); // Khôi phục tốc độ gốc
    }

    public void Unshrink()
    {
        isShrunk = false; // Đánh dấu là đã trở lại kích thước gốc
        transform.localScale = originalScale; // Khôi phục kích thước gốc
        ChangeColor(defaultColor);
    }

    public void ChangeColor(Color color)
    {
        enemyRenderer.material.color = color; // Thay đổi màu sắc
    }

}