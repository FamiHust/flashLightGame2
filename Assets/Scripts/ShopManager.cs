using UnityEngine;
using UnityEngine.EventSystems;

public class ShopManager : MonoBehaviour
{
    public int[,] shopItems = new int[5, 5];
    private CoinPickUp coins;

    void Start()
    {
        coins = FindObjectOfType<CoinPickUp>();
        coins.totalCoins = PlayerPrefs.GetInt("totalCoins", 0);
        coins.UpdateCoinsText();

        InitializeShopItems();
        LoadShopQuantities(); // Tải số lượng items đã mua
    }

    private void InitializeShopItems()
    {
        // ID = 1: maxBattery, ID = 2: maxHealth, ID = 3: moveSpeed
        shopItems[1, 1] = 1; // Item ID
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;

        shopItems[2, 1] = 50; // Giá tiền
        shopItems[2, 2] = 50;
        shopItems[2, 3] = 50;

        shopItems[3, 1] = 0; // Số lượng đã mua (ban đầu)
        shopItems[3, 2] = 0;
        shopItems[3, 3] = 0;
    }

    private void LoadShopQuantities()
    {
        // Tải số lượng đã mua từ PlayerPrefs
        for (int i = 1; i <= 3; i++) // Giả định có 3 items
        {
            shopItems[3, i] = PlayerPrefs.GetInt($"item_{i}_quantity", 0);
        }
    }

    private void SaveShopQuantities()
    {
        // Lưu số lượng đã mua vào PlayerPrefs
        for (int i = 1; i <= 3; i++) // Giả định có 3 items
        {
            PlayerPrefs.SetInt($"item_{i}_quantity", shopItems[3, i]);
        }
        PlayerPrefs.Save();
    }

    public void Buy()
    {
        GameObject buttonRef = EventSystem.current.currentSelectedGameObject;
        int itemID = buttonRef.GetComponent<ButtonInfo>().ItemID;

        if (coins.totalCoins >= shopItems[2, itemID])
        {
            coins.totalCoins -= shopItems[2, itemID];
            PlayerPrefs.SetInt("totalCoins", coins.totalCoins);
            PlayerPrefs.Save();

            shopItems[3, itemID]++; // Tăng số lượng item đã mua
            SaveShopQuantities(); // Lưu số lượng đã mua vào PlayerPrefs

            // Cập nhật các giá trị trong PlayerPrefs
            switch (itemID)
            {
                case 1: // maxBattery
                    PlayerPrefs.SetInt("maxBattery", PlayerPrefs.GetInt("maxBattery", 10) + 4);
                    break;
                case 2: // maxHealth
                    PlayerPrefs.SetInt("maxHealth", PlayerPrefs.GetInt("maxHealth", 10) + 4);
                    break;
                case 3: // moveSpeed
                    PlayerPrefs.SetFloat("moveSpeed", PlayerPrefs.GetFloat("moveSpeed", 3.5f) + 0.2f);
                    break;
            }

            PlayerPrefs.Save();
            coins.UpdateCoinsText();

            // Cập nhật số lượng hiển thị trên nút
            buttonRef.GetComponent<ButtonInfo>().QuantityTxt.text = GetItemQuantityText(itemID);
        }
        else
        {
            Debug.Log("Không đủ coin để mua item!");
        }
    }

    public string GetItemQuantityText(int itemID)
    {
        switch (itemID)
        {
            case 1: // maxBattery (BP)
                return $"BP + {shopItems[3, itemID] * 10}%";
            case 2: // maxHealth (HP)
                return $"HP + {shopItems[3, itemID] * 10}%";
            case 3: // moveSpeed (SP)
                return $"SP + {shopItems[3, itemID] * 10}%";
            default:
                return "Unknown";
        }
    }

}
