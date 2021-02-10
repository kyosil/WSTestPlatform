using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public string Ability { get; set; }
    public string Name { get; set; }
    public string Rarity { get; set; }
    public string Side { get; set; }
    public int Level { get; set; }
    public int Cost { get; set; }
    public int Power { get; set; }
    public int Soul { get; set; }
    public string Set { get; set; }
    public string Release { get; set; }
    public string SID { get; set; }
    public string CardType { get; set; }
    public string Colour { get; set; }

    public Card(string ability, string name, string rarity, string side, int level, int cost, int power, int soul, string set, string release, string sid, string cardType, string colour)
    {
        Ability = ability;
        Name = name;
        Rarity = rarity;
        Side = side;
        Level = level;
        Cost = cost;
        Power = power;
        Soul = soul;
        Set = set;
        Release = release;
        SID = sid;
        CardType = cardType;
        Colour = colour;
    }
}
