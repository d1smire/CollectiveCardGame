using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _heroSide; 
    [SerializeField] private GameObject[] _enemiesSide; 

    public List<Fighter> SpawnEnemies(Fighter[] enemies)
    {
        CheckInputCorrect(enemies, _enemiesSide);
        return SpawnFighters(enemies,_enemiesSide);
    }

    public List<Fighter> SpawnHeroes(Fighter[] heroes)
    {
        CheckInputCorrect(heroes, _heroSide);
        return SpawnFighters(heroes, _heroSide);
    }
    
    private void CheckInputCorrect(Fighter[] fighter, GameObject[] side) 
    {
        if (fighter.Length > side.Length || fighter.Length == 0) 
        {
            throw new System.Exception("Incorrect Fighter Count");
        }
    }

    private List<Fighter> SpawnFighters(Fighter[] fighterTemplate, GameObject[] spawnPoints) 
    {
        List<Fighter> fighter = new List<Fighter>();
    
        for(int i = 0; i < fighterTemplate.Length; i++) 
        {
            Fighter newFighter = Instantiate(fighterTemplate[i], spawnPoints[i].transform);
            newFighter.CurrentStayPoint(spawnPoints[i]);
            fighter.Add(newFighter);
        }
        return fighter;
    }
}
