using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool isFrozen = false; // Trạng thái đóng băng
    private bool isShrunk = false; // Trạng thái co lại
    private Renderer enemyRenderer; // Tham chiếu đến Renderer

    private void Start()
    {
        enemyRenderer = GetComponent<Renderer>(); // Lấy tham chiếu đến Renderer
    }

    private void Update()
    {
        if (isFrozen)
        {
            return;
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

    public void ChangeColor(Color color)
    {
        enemyRenderer.material.color = color; // Thay đổi màu sắc
    }

    public void Freeze()
    {
        isFrozen = true;
        GetComponent<Enemy_Movement>().StopMovement(); // Dừng chuyển động
    }

    public void Shrink()
    {
        if (!isShrunk) // Kiểm tra nếu chưa co lại
        {
            isShrunk = true; // Đánh dấu là đã co lại
        }
    }
}