using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerParams))]
public class CharacterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PlayerParams playerParams = (PlayerParams)target;

        playerParams.CharType = (CharType)EditorGUILayout.EnumPopup("Character Type", playerParams.CharType);

        switch (playerParams.CharType)
        {
            case CharType.player:
                DrawPlayerFields(playerParams);
                break;
            case CharType.enemy:
                DrawEnemyFields(playerParams);
                break;
        }
    }

    private void DrawPlayerFields(PlayerParams playerParams)
    {
        playerParams.CharRoles = (CharRole)EditorGUILayout.EnumPopup("Character Role", playerParams.CharRoles);
        playerParams.Health = EditorGUILayout.FloatField("Health", playerParams.Health);
        playerParams.ATK = EditorGUILayout.FloatField("Attack", playerParams.ATK);
        playerParams.DEF = EditorGUILayout.FloatField("Defense", playerParams.DEF);
        playerParams.Speed = EditorGUILayout.FloatField("Speed", playerParams.Speed);
        playerParams.CritChance = EditorGUILayout.IntField("Crit Chance", playerParams.CritChance);
        playerParams.EnergyRecovery = EditorGUILayout.IntField("Energy Recovery", playerParams.EnergyRecovery);
        playerParams.EnergyForUltimates = EditorGUILayout.FloatField("Energy for Ultimates", playerParams.EnergyForUltimates);
        playerParams.NeedEnergyToUseUltimates = EditorGUILayout.FloatField("Energy Needed for Ultimates", playerParams.NeedEnergyToUseUltimates);
        playerParams.CharXP = EditorGUILayout.FloatField("Character XP", playerParams.CharXP);
        playerParams.CharLevel = EditorGUILayout.IntField("Character Level", playerParams.CharLevel);
    }

    private void DrawEnemyFields(PlayerParams playerParams)
    {
        playerParams.CharRoles = (CharRole)EditorGUILayout.EnumPopup("Character Role", playerParams.CharRoles);
        playerParams.Health = EditorGUILayout.FloatField("Health", playerParams.Health);
        playerParams.ATK = EditorGUILayout.FloatField("Attack", playerParams.ATK);
        playerParams.DEF = EditorGUILayout.FloatField("Defense", playerParams.DEF);
        playerParams.Speed = EditorGUILayout.FloatField("Speed", playerParams.Speed);
        playerParams.CritChance = EditorGUILayout.IntField("Crit Chance", playerParams.CritChance);
        playerParams.EnergyRecovery = EditorGUILayout.IntField("Energy Recovery", playerParams.EnergyRecovery);
        playerParams.EnergyForUltimates = EditorGUILayout.FloatField("Energy for Ultimates", playerParams.EnergyForUltimates);
        playerParams.NeedEnergyToUseUltimates = EditorGUILayout.FloatField("Energy Needed for Ultimates", playerParams.NeedEnergyToUseUltimates);
        playerParams.CharLevel = EditorGUILayout.IntField("Character Level", playerParams.CharLevel);
    }
}
