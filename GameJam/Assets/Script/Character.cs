using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewCharecter", menuName = "Character")]
public class Character : ScriptableObject
{
    public string characterName;
    public Sprite characterSprite;
    public Sprite characterIcon;
    public float zoom = 1;
}
