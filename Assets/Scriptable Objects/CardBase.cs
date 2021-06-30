using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "CardBase")]
public class CardBase : ScriptableObject
{
    public string ability;
    public string cardName;
    public string rarity;
    public string side;
    public int level;
    public int cost;
    public int power;
    public int soul;
    public string set;
    public string release;
    public string sID;
    public string cardType;
    public string colour;
    public string imagePath;
}
