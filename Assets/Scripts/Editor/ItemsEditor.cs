using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Items), true)]
public class ItemsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Items items = (Items)target;

        items.Id = EditorGUILayout.IntField("Id", items.Id);
        items.Name = EditorGUILayout.TextField("Name", items.Name);
        items.dropType = (DropType)EditorGUILayout.EnumPopup("Drop Type", items.dropType);
        items.UIDisplay = (Sprite)EditorGUILayout.ObjectField("Display on UI", items.UIDisplay, typeof(Sprite), false);

        EditorGUILayout.LabelField("Description");
        items.description = EditorGUILayout.TextArea(items.description, GUILayout.Height(100));

        if (items.dropType == DropType.Epic)
        {
            EditorGUILayout.LabelField("Epic Item Attributes", EditorStyles.boldLabel);
            items.buffs.buff = (Attributes)EditorGUILayout.EnumPopup("Buff", items.buffs.buff);
            items.buffs.value = EditorGUILayout.FloatField("Buff Value", items.buffs.value);
            items.ItemXP = EditorGUILayout.FloatField("Item XP", items.ItemXP);
            items.ItemLevel = EditorGUILayout.IntField("Item Level", items.ItemLevel);
        }
        if (items.dropType == DropType.Legendary)
        {
            items.LinkOnPrefab = EditorGUILayout.TextField("Link on prefab", items.LinkOnPrefab);
            items.ItemXP = EditorGUILayout.FloatField("Item XP", items.ItemXP);
            items.ItemLevel = EditorGUILayout.IntField("Item Level", items.ItemLevel);
        }
    }
}
