using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIUpgrade : MonoBehaviour
{
    [SerializeField] private InventoryObjects _inventory;

    [SerializeField] private List<GameObject> ItemCardsUI;

    [SerializeField] private Upgrade _upgrade;

    [SerializeField] private GameObject ItemCardUI;
    [SerializeField] private Transform ItemCardPosition;

    [SerializeField] private Transform InformationPanelPosition;
    [SerializeField] private GameObject InformationPanel;
    private List<GameObject> TimeList; // придумать норм назву

    public event Action<int> ItemID;

    private void Start()
    {
        TimeList = new List<GameObject>();
        _inventory = Resources.Load<InventoryObjects>("Inventories/Inventory");
        _upgrade = GetComponentInParent<Upgrade>();
        if (_upgrade != null)
        {
            _upgrade.InfoAbt += InfoAbout;
        }
    }

    private void OnDestroy()
    {
        _upgrade.InfoAbt -= InfoAbout;
    }

    private void InfoAbout(string message)
    {
        if (TimeList.Count < 4)
        {
            var panel = Instantiate(InformationPanel, InformationPanelPosition);
            var text = panel.GetComponentInChildren<TextMeshProUGUI>(true);
            text.text = message;
            TimeList.Add(panel);
            StartCoroutine(StartTimer());
        }
    }

    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(1f);
        Destroy(TimeList[0].gameObject);
        TimeList.Remove(TimeList[0]);
    }

    public void ItemOnDisplay(string dropType)
    {
        switch (dropType) 
        {
            case "Common": //0
                CreateCard(DropType.Common);
                break;
            case "Rare": //1
                CreateCard(DropType.Rare);
                break;
            case "Epic": //2
                CreateCard(DropType.Epic);
                break;
            case "Legendary": //3
                CreateCard(DropType.Legendary);
                break;
        }
    }

    private void CreateCard(DropType dropType) 
    {
        var cardID = 0;
        var count = FindAllItemsOfOneType(dropType);
        for (int i = 0; i < _inventory.Container.Item.Length; i++)
        {
            if (_inventory.Container.Item[i].ID != -1 && _inventory.Container.Item[i].item.dropType == dropType)
            {
                if (ItemCardsUI.Count < count) 
                {
                    var card = Instantiate(ItemCardUI, ItemCardPosition);
                    FillCard(card, _inventory.Container.Item[i].item, _inventory.Container.Item[i].amount.ToString()); 
                }
                else 
                {
                    ReFillCard(ItemCardsUI, _inventory.Container.Item[i].item, _inventory.Container.Item[i].amount.ToString(), cardID);
                    cardID++;
                }
            }
        }
    }

    private int FindAllItemsOfOneType(DropType dropType) 
    {
        var count = 0;
        for (int i = 0; i < _inventory.Container.Item.Length; i++)
        {
            if (_inventory.Container.Item[i].ID != -1 && _inventory.Container.Item[i].item.dropType == dropType)
            {
                count++;
            }
        }
        return count;
    }

    private void FillCard(GameObject card, Items item, string amount) 
    {
        var image = card.transform.GetChild(0).GetComponentInChildren<Image>();
        var textName = card.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
        var textAmount = card.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>();

        image.sprite = item.UIDisplay;
        textName.text = item.Name;
        textAmount.text = amount;

        if (item.dropType == DropType.Epic || item.dropType == DropType.Legendary)
        {
            var button = card.GetComponent<Button>();
            button.interactable = true;
            button.onClick.AddListener(() => ItemID?.Invoke(item.Id));
        }
        else
        {
            var button = card.GetComponent<Button>();
            button.interactable = false;
        }
        ItemCardsUI.Add(card);
    }

    private void ReFillCard(List<GameObject> card, Items item, string amount, int cardId)
    {
        var image = card[cardId].transform.GetChild(0).GetComponentInChildren<Image>();
        var textName = card[cardId].transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
        var textAmount = card[cardId].transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>();
        if (item != null)
        {
            image.sprite = item.UIDisplay;
            textName.text = item.Name;
            textAmount.text = amount;
            if (item.dropType == DropType.Epic || item.dropType == DropType.Legendary)
            {
                var button = card[cardId].GetComponent<Button>();
                button.interactable = true; 
                button.onClick.AddListener(() => ItemID?.Invoke(item.Id));
            }
            else
            {
                var button = card[cardId].GetComponent<Button>();
                button.interactable = false; 
            }

            card[cardId].gameObject.SetActive(true);
        }
        else { card[cardId].gameObject.SetActive(false); }

        for (int i = 0; i < card.Count; i++) // може додам новий метод для цієї перевірки
        {
            if (card.Count != cardId + 1)
            {
                card[cardId + 1].gameObject.SetActive(false);
            }
        }
    }
}
