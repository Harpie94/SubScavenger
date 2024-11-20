using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName ="Scriptable Object/Item")]
public class ItemSO : ScriptableObject
{
    public float mass;
    public itemType item_type;
    public Sprite item_sprite;
}

public enum itemType { Cube, Sphere };