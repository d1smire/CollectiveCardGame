using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class CraftingUI : MonoBehaviour
{
    private CraftingRecipe[] _recipes;
    private List<GameObject> TimeList; // придумать норм назву
    private Crafting _crafting;

    [SerializeField] private Transform CraftingCardPosition;
    [SerializeField] private GameObject CraftingCard;
    [SerializeField] private Image ImagePrefab;
    [SerializeField] private Transform InformationPanelPosition;
    [SerializeField] private GameObject InformationPanel;

    public event Action<int> ButtonClicked;

    private void Awake()
    {
        _crafting = GetComponent<Crafting>();
        _crafting.GettingRecipes += SetRecipe;
        _crafting.InfoAbt += InfoAbout;
    }

    private void Start()
    {
        TimeList = new List<GameObject>();
        if (_recipes != null)
        {
            SetRecipeToCards();
        }
    }

    private void OnDestroy()
    {
        _crafting.GettingRecipes -= SetRecipe;
        _crafting.InfoAbt -= InfoAbout;
    }

    private void SetRecipe(CraftingRecipe[] recipes) 
    {
        _recipes = new CraftingRecipe[recipes.Length];
        for (int i = 0; i < _recipes.Length; i++) 
        {
            _recipes[i] = recipes[i];
        }
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

    private void SetRecipeToCards() 
    {
        for (int i = 0; i < _recipes.Length; i++)
        {
            var card = Instantiate(CraftingCard, CraftingCardPosition);
            var text = card.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
            var imagePrefab = card.transform.GetChild(1).GetComponentInChildren<Image>();
            var imageCardPosition = card.transform.GetChild(2).GetComponentInChildren<RectTransform>();
            var button = card.transform.GetChild(4).GetComponentInChildren<Button>();

            var image = imagePrefab.transform.GetChild(0).GetComponentInChildren<Image>();

            SetButton(button, i);

            text.text = _recipes[i].CraftedItem.Name;
            image.sprite = _recipes[i].CraftedItem.UIDisplay;

            CreateImages(i, imageCardPosition);
        }
    }
    private void SetButton(Button button,int recipeNumber) 
    {
        button.onClick.AddListener(() => ButtonClicked?.Invoke(recipeNumber));
    }

    private void CreateImages(int CurrentCard, Transform imageCardPosition)
    {
        float currentXPosition = -230f;

        for (int i = 0; i < _recipes[CurrentCard].ItemsForCraft.Length; i++)
        {
            var cardImagePrefab = Instantiate(ImagePrefab, imageCardPosition);
            RectTransform imageRect = cardImagePrefab.GetComponent<RectTransform>();

            float newXPosition = currentXPosition;

            //позиція для новоствореного об'єкта
            imageRect.anchoredPosition = new Vector2(newXPosition, 0);
            
            float imageWidth = imageRect.rect.width;
            
            currentXPosition += imageWidth;
            currentXPosition += 10f;


            var image = cardImagePrefab.transform.GetChild(0).GetComponentInChildren<Image>();
            image.sprite = _recipes[CurrentCard].ItemsForCraft[i].UIDisplay;
        }
    }
}
