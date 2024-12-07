using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonInfo : MonoBehaviour
{
    public int ItemID;
    public TextMeshProUGUI PriceTxt;
    public TextMeshProUGUI QuantityTxt;
    public GameObject ShopManager;
    // Update is called once per frame
    void Update()
    {
        ShopManager manager = ShopManager.GetComponent<ShopManager>();
        PriceTxt.text = "Price: " + manager.shopItems[2, ItemID].ToString();
        QuantityTxt.text = manager.GetItemQuantityText(ItemID);
    }
}
