using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Store : MonoBehaviour
{

    [SerializeField] GameObject storeItemTitle;
    [SerializeField] GameObject storeItemDescription;

    [SerializeField] Player player;

    [SerializeField] float jumpBoost;
    [SerializeField] float speedBoost;
    [SerializeField] int powerUpPrice = 2;

    TextMeshProUGUI itemTitleText;
    TextMeshProUGUI itemDescriptionText;

    // Start is called before the first frame update
    void Start()
    {
        itemTitleText = storeItemTitle.GetComponent<TextMeshProUGUI>();
        itemDescriptionText = storeItemDescription.GetComponent<TextMeshProUGUI>();
        SetTextActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Buy(string product)
    {
        switch (product)
        {
            case "PlushMouse":
                BuyAbilityToSmack();
                break;
            case "ClimbingTree":
                BuyAbilityToClimb();
                break;
            case "BallOfYarn":
                BuyEnchancedSpeed();
                break;
            case "FeatherOnAStick":
                BuyEnchanedJumping();
                break;
            default:
                break;
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

    public void DisplayItemText(string title, string description)
    {
        SetTextActive(true);
        itemTitleText.text = title;
        itemDescriptionText.text = description;
    }



}
