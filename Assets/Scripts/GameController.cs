using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour
{
    public InventoryObjects Inventory;

    public Camera cameras;

    [SerializeField] private GameObject escapeMenu;

    [SerializeField] private GameObject openingCases;
    [SerializeField] private GameObject ChosingLvl;
    [SerializeField] private GameObject craft;
    [SerializeField] private GameObject upgrade;

    [SerializeField] private Transform prefabPosition;


    [Inject] private DiContainer Container;

    private GameObject _createdPrefab;
    private ChooseLevelUI LevelUI;
    [SerializeField] private List<Fighter> heroes;

    private bool IsCreate = false;
    private bool IsOpen = true;

    //private void Start()
    //{
    //    fighters = new List<Fighter>();
    //}

    private void Update()
    {
        if (!IsCreate)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                escapeMenu.SetActive(IsOpen);
                IsOpen = !IsOpen;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMainWindow();
        }
    }

    private void OnDestroy()
    {
        if(LevelUI != null)
            LevelUI.IsArenaChoose -= CreateArena;
    }

    private void ReturnToMainWindow() 
    {
        cameras.transform.position = new Vector3(0, 0, -10);
        cameras.orthographicSize = 5;
        Destroy(_createdPrefab);
        IsCreate = false;
    }

    public void Reverse() 
    {
        escapeMenu.SetActive(IsOpen);
        IsOpen = !IsOpen;
    }

    public void Create(int number) 
    {
        switch (number) 
        {
            case 0:
                CreatePrefab(openingCases);
                break;
            case 1:
                CreatePrefab(ChosingLvl);
                if (LevelUI != null)
                {
                    LevelUI.IsArenaChoose += CreateArena;
                }
                break;
            case 2:
                CreatePrefab(craft);
                break;
            case 3:
                CreatePrefab(upgrade);
                break;
        } 
    }

    private void CreatePrefab(GameObject prefab)
    {
        cameras.transform.position = new Vector3(34, 0, -10);
        _createdPrefab = Container.InstantiatePrefab(prefab, prefabPosition.position, Quaternion.identity, prefabPosition);
        if(ChosingLvl.name + "(Clone)" == _createdPrefab.name) 
        {
            LevelUI = _createdPrefab.GetComponent<ChooseLevelUI>();
        }
        IsCreate = true;
    }

    private void CreateArena(GameObject prefab) 
    {
        var arena = prefab.GetComponent<Arena>();
        if (arena != null) 
        {
            arena.SetHeroes(heroes);
        }
        if (IsCreate) 
        {
            Destroy(_createdPrefab);
            CreatePrefab(prefab);
        }
        else 
        {
            CreatePrefab(prefab);
        }
        cameras.orthographicSize = 10;
    }

    public void Save()
    {
        Inventory.Save();
    }
    public void Load()
    {
        Inventory.Load();
    }
    public void ExitFromGame()
    {
        Application.Quit();
    }
    private void OnApplicationQuit()
    {
        Inventory.Container.Item = new InventorySlot[96];
    }
}
