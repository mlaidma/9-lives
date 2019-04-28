using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Store : MonoBehaviour
{

    [SerializeField] GameObject storeItemTitle;
    [SerializeField] GameObject storeItemDescription;
    [SerializeField] GameObject alreadyPurchased;

    [SerializeField] StoreObject[] availableProducts;

    [SerializeField] Player player;

    [SerializeField] float jumpBoost;
    [SerializeField] float speedBoost;
    [SerializeField] int powerUpPrice = 2;

    TextMeshProUGUI itemTitleText;
    TextMeshProUGUI itemDescriptionText;
    TextMeshProUGUI alreadyPurchasedText;

    string purchased = ""; 

    // Start is called before the first frame update
    void Start()
    {
        itemTitleText = storeItemTitle.GetComponent<TextMeshProUGUI>();
        itemDescriptionText = storeItemDescription.GetComponent<TextMeshProUGUI>();
        alreadyPurchasedText = alreadyPurchased.GetComponent<TextMeshProUGUI>();
        SetTextActive(false);
        HideAlreadyPurchased();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Buy(StoreObject storeObject)
    {
        string product = storeObject.GetStoreObjectText().GetObjectName();
        string productName = storeObject.name;

        if(purchased == "")
        {
            switch (product)
            {
                case "Plush Mouse":
                    BuyAbilityToSmack();
                    break;
                case "Climbing Tree":
                    BuyAbilityToClimb();
                    break;
                case "Ball of Yarn":
                    BuyEnchancedSpeed();
                    break;
                case "Feather on a Stick":
                    BuyEnchanedJumping();
                    break;
                default:
                    break;
            }

            purchased = product;
            HideOtherProducts(productName);
            Debug.Log(purchased);
        }
        else
        {
            alreadyPurchasedText.text = "You already purchased " + purchased + " this level!";
            alreadyPurchased.SetActive(true);
            Invoke("HideAlreadyPurchased", 5f);

            return;
        }

    }

    private void BuyEnchanedJumping()
    {
        player.IncreaseJumpForce(jumpBoost);
        player.LoseLives(powerUpPrice);
    }

    private void BuyEnchancedSpeed()
    {
        player.IncreaseSpeed(speedBoost);
        player.LoseLives(powerUpPrice);
    }

    private void BuyAbilityToClimb()
    {
        player.EnableClimbing();
        player.LoseLives(powerUpPrice);
    }

    private void BuyAbilityToSmack()
    {
        player.EnableSmacking();
        player.LoseLives(powerUpPrice);
    }

    public void SetTextActive(bool state)
    {
        storeItemTitle.SetActive(state);
        storeItemDescription.SetActive(state);
    }

    private void HideAlreadyPurchased()
    {
        alreadyPurchased.SetActive(false);
    }

    private void HideOtherProducts(string name)
    {
        foreach (StoreObject obj in availableProducts)
        {
            if(obj.name != name)
            {
                obj.SetVisibility(false);
            }
        }
    }

    public void DisplayItemText(string title, string description)
    {
        SetTextActive(true);
        itemTitleText.text = title;
        itemDescriptionText.text = description;
    }



}
