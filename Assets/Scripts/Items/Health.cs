using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    [HideInInspector] public int currentHealth;
    private bool isDie = false;
    private SpriteRenderer spriteRenderer; // Thêm biến để lưu trữ SpriteRenderer
    private Color originalColor; // Màu sắc gốc
    public GameObject gameObject;

    HealthBar healthBar;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color; // Lưu màu sắc gốc
    }

    private void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
            healthBar.UpdateBar(currentHealth, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth == 0)
        {
            currentHealth = 0;
            SoundManager.PlaySound(SoundType.DIE);
            gameObject.SetActive(false);
        }

        StartCoroutine(FlashGrey());
    }

    private IEnumerator FlashGrey()
    {
        spriteRenderer.color = Color.grey;
        yield return new WaitForSeconds(0.1f); // Thời gian nhấp nháy
        spriteRenderer.color = originalColor; // Khôi phục lại màu sắc gốc
    }
}