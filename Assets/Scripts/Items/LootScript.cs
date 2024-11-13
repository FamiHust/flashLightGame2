using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LootScript : MonoBehaviour
{
    [System.Serializable]
    public class DropCurrency
    {
        public string name;
        public GameObject item;
        public int dropRarity;
    }

    public List<DropCurrency> LootTable = new List<DropCurrency>();
    private bool hasDropped = false; // Biến kiểm tra xem vật phẩm đã được rơi hay chưa

    public void OnLootButtonClicked()
    {
        calculateLoot();
    }

    public void calculateLoot()
    {
        if (hasDropped)
        {
            return;
        }

        // Tính toán trọng số cho vật phẩm
        int itemWeight = 0;
        for (int i = 0; i < LootTable.Count; i++)
        {
            itemWeight += LootTable[i].dropRarity;
        }

        // Chọn một vật phẩm dựa trên trọng số
        int randomValue = Random.Range(0, itemWeight);
        for (int j = 0; j < LootTable.Count; j++)
        {
            if (randomValue < LootTable[j].dropRarity)
            {
                Instantiate(LootTable[j].item, transform.position, Quaternion.identity);
                hasDropped = true; // Đánh dấu là đã rơi vật phẩm
                return;
            }
            randomValue -= LootTable[j].dropRarity;
        }
    }
}