using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "HGPTE/Item data")]
public class Item : ScriptableObject
{
    public string itemName;
    public string type;
    public string description;
    public Sprite image;
}
