using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OpenChestUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] costText;
    [SerializeField] private Button[] buttons;

    [SerializeField] private GameObject _received;
    [SerializeField] private GameObject _itemCard; 
    [SerializeField] private Transform _itemContainer;

    [SerializeField] private GameObject _errorMessage;
    [SerializeField] private TextMeshProUGUI _errorText;

    private Image _itemImage;
    private TextMeshProUGUI _itemText;
    private List<GameObject> _itemsCard;
    private OpenChest _openChest;
    private int _chestCost;

    public int X_START; // -4
    public int Y_START; // 1
    public int X_SPACE_BETWEEN_ITEMS; // 2
    public int Y_SPACE_BETWEEN_ITEMS; // 2
    public int NUMBER_OF_COLUMNS; // 5
    
    private void Start()
    {
        _openChest = GetComponent<OpenChest>();
        _chestCost = 100;
        costText[0].text = _chestCost.ToString();
        costText[1].text = (_chestCost * 10).ToString();
        _itemsCard = new List<GameObject>();
        _openChest.ChestOpen += IsItemReceived;
        _openChest.CantOpen += IsError;
    }

    private void OnDestroy()
    {
        _openChest.ChestOpen -= IsItemReceived;
        _openChest.CantOpen -= IsError;
    }

    private void IsError(string message) 
    {
        _errorMessage.SetActive(true);
        _errorText.text = message;
    }
    private void IsItemReceived(Items item)
    {
        var prefab = Instantiate(_itemCard, _itemContainer);
        
        _itemsCard.Add(prefab);
        
        GetPrefabComponents(prefab);
        SetRecievedItem(item, prefab);
        
        if (_itemsCard.Count == 10)
        {
            for (int i = 0; i < _itemsCard.Count; i++)
            {
                _itemsCard[i].GetComponent<RectTransform>().localPosition = GetPosition(i);
            }
        }
        if (buttons.Length == 2)
        {
            buttons[0].interactable = false;
            buttons[1].interactable = false;
        }
        _received.SetActive(true);
    }

    private void GetPrefabComponents(GameObject itemPrefab)
    {
        _itemImage = itemPrefab.transform.GetChild(0).GetComponentInChildren<Image>();
        _itemText = itemPrefab.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
    }

    private void SetRecievedItem(Items item, GameObject prefab)
    {
        if (_itemImage != null && _itemText != null)
        {
            _itemImage.sprite = item.UIDisplay;
            _itemText.text = item.Name;
            
            var ItemColor = prefab.GetComponent<Image>();

            switch (item.dropType)
            {
                case DropType.Common:
                    ItemColor.color = new Color(0.2f, 0.2f, 0.2f, 1f);
                    break;

                case DropType.Rare:
                    ItemColor.color = new Color(0, 0, 0.5f, 1f);
                    break;

                case DropType.Epic:
                    ItemColor.color = new Color(0.5f, 0, 0.5f, 1f);
                    break;

                case DropType.Legendary:
                    ItemColor.color = new Color(0.7f, 0.7f, 0, 1f);
                    break;

                default:
                    break;
            }
        }
    }
    private Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEMS * (i % NUMBER_OF_COLUMNS)), 
            Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMNS)), 
            0f);
    }
    public void CloseReceived()
    {
        foreach (Transform child in _itemContainer)
        {
            Destroy(child.gameObject);
        }
        if(buttons.Length == 2) 
        {
            buttons[0].interactable = true;
            buttons[1].interactable = true;
        }
        _itemsCard = new List<GameObject>();
        _received.SetActive(false);
    }
    public void CloseError() 
    {
        _errorMessage.SetActive(false);
        _errorText.text = "";
    }
}