using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StoreObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    Store store;
    Image image;

    [SerializeField] StoreObjectText itemText;
 

    bool available = true;

    // Start is called before the first frame update
    void Start()
    {
        store = FindObjectOfType<Store>();
        image = GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Buy()
    {
        if(available)
        {
            SetAvailability(false);
            store.Buy(gameObject.name);
        }
    }

    private void SetAvailability(bool state)
    {
        if (state)
        {
            image.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            image.color = new Color(1f, 1f, 1f, 0.2f);
        }

        available = state;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        store.DisplayItemText(itemText.GetObjectName(), itemText.GetObjectDescription());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        store.SetTextActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Buy();
    }
}
