using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ChooseLevelUI : MonoBehaviour
{
    [SerializeField] private GameObject CardOfArena;
    [SerializeField] private Transform CardPosition;

    private ArenaInfoObjects[] _battleArena;

    public event Action<GameObject> IsArenaChoose;  

    private void Start()
    {
        _battleArena = Resources.LoadAll<ArenaInfoObjects>("ScriptableObjects/Arenas");
        SetArenas();
    }

    private void SetArenas() 
    {
        if (_battleArena != null) 
        {
            for(int i = 0; i < _battleArena.Length; i++)
            {
                var arenaCard = Instantiate(CardOfArena, CardPosition);
                SetCards(arenaCard, i);
            }
        }
    }

    private void SetCards(GameObject arenaCard, int arenaID)
    {
        var arenaName = arenaCard.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
        var arenaImage = arenaCard.transform.GetChild(1).GetComponentInChildren<Image>();
        var arenaReward = arenaCard.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>();
        var arenaDescription = arenaCard.transform.GetChild(3).GetComponentInChildren<TextMeshProUGUI>();
        var arenaEnemy = arenaCard.transform.GetChild(4).GetComponentInChildren<TextMeshProUGUI>();
        var button = arenaCard.transform.GetChild(5).GetComponentInChildren<Button>();

        SetButton(button, arenaID);

        arenaName.text = _battleArena[arenaID].ArenaName;
        arenaImage.sprite = _battleArena[arenaID].UIDisplay;
        arenaReward.text += _battleArena[arenaID].CoinReward.ToString();
        arenaDescription.text = _battleArena[arenaID].ArenaDescription;
        foreach (var enemy in _battleArena[arenaID].Enemy)
        {
            arenaEnemy.text += "\n" + enemy.name;
        }
    }

    private void SetButton(Button button, int arenaID)
    {
        var loadedArena = ArenaPicked(arenaID);
        var arena = loadedArena.GetComponent<Arena>();
        arena.SetEnemies(_battleArena[arenaID].Enemy);
        button.onClick.AddListener(() => IsArenaChoose?.Invoke(loadedArena));
    }

    private GameObject ArenaPicked(int arenaID) 
    {
        var arena = Resources.Load<GameObject>(_battleArena[arenaID].LinkOnArenaPrefab);
        return arena;
    }
}
