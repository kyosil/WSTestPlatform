using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Net;
using System;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;
using static System.Net.Mime.MediaTypeNames;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //List<Card> myDeck = LoadDeckFromURL("https://www.encoredecks.com/api/deck/3MaHWilwW");
        //List<Card> opponentDeck = LoadDeckFromURL("https://www.encoredecks.com/api/deck/Y1mRBaryO");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public static List<Card> LoadDeckFromURL(string url)
    {
        string json = new WebClient().DownloadString(url);
        var jsonData = JObject.Parse(json);

        JObject encoreDecks = JObject.Parse(json);

        IList<JToken> results = encoreDecks["cards"].Children().ToList();
        List<string> cardNames = new List<string>();
        List<string> cardAbilities = new List<string>();
        List<string> cardAttributes = new List<string>();
        List<string> cardRarity = new List<string>();
        List<string> cardSide = new List<string>();
        List<int> cardLevel = new List<int>();
        List<int> cardCost = new List<int>();
        List<int> cardPower = new List<int>();
        List<int> cardSoul = new List<int>();
        List<string> cardSet = new List<string>();
        List<string> cardRelease = new List<string>();
        List<string> cardSID = new List<string>();
        List<string> cardType = new List<string>();
        List<string> cardColour = new List<string>();
        List<string> imagePath = new List<string>();

        foreach (var item in results)
        {
            cardNames.Add(item["locale"]["EN"]["name"].ToString());
            cardAbilities.Add(item["locale"]["EN"]["ability"].ToString());
            cardAttributes.Add(item["locale"]["EN"]["attributes"].ToString());
            cardRarity.Add(item["rarity"].ToString());
            cardSide.Add(item["side"].ToString());
            cardLevel.Add((int)item["level"]);
            cardCost.Add((int)item["cost"]);
            cardPower.Add((int)item["power"]);
            cardSoul.Add((int)item["soul"]);
            cardSet.Add(item["set"].ToString());
            cardRelease.Add(item["release"].ToString());
            cardSID.Add(item["sid"].ToString());
            cardType.Add(item["cardtype"].ToString());
            cardColour.Add(item["colour"].ToString());
            imagePath.Add(item["imagepath"].ToString());
        }

        List<Card> deck1 = new List<Card>();

        for (int i = 0; i < 50; i++)
        {
            deck1.Add(new Card(cardAbilities[i], cardNames[i], cardRarity[i], cardSide[i], cardLevel[i], cardCost[i], cardPower[i], cardSoul[i], cardSet[i], cardRelease[i], cardSID[i], cardType[i], cardColour[i], imagePath[i]));
            //Debug.Log(deck1[i].ImagePath);
        }

        return deck1;
    }
        
    public List<Card> LoadDecklist(string url)
    {
        string json = new WebClient().DownloadString(url);
        var jsonData = JObject.Parse(json);

        JObject encoreDecks = JObject.Parse(json);

        //IList <JToken> results = encoreDecks["cards"].Children().ToList();
        IList<JToken> results = encoreDecks["cards"].ToList();

        IList<Card> deckList = new List<Card>();
        foreach (JToken result in results)
        {
            // JToken.ToObject is a helper method that uses JsonSerializer internally
            Card searchResult = result.ToObject<Card>();

            deckList.Add(searchResult);
        }

        List<Card> deck1 = new List<Card>();
        foreach (var item in deckList)
        {
            deck1.Add(new Card(item.Ability, item.Name, item.Rarity, item.Side, item.Level, item.Cost, item.Power, item.Soul, item.Set, item.Release, item.SID, item.CardType, item.Colour, item.ImagePath));
            //Console.WriteLine(item.Name + " " + item.Level + " " + item.Side + " " + item.Cost + " " + item.Power + " " + item.Rarity + " " + item.Soul + " " + item.Set + " " + item.SID + " " + item.Release + " " + item.CardType + " " + item.Colour);
        }

        return deck1;
    }

    public void RandomizeList(List<Card> list)
    {
        var rand = new System.Random();
        var randomList = list.OrderBy(x => rand.Next()).ToList();

        ShowList(randomList);

        //return randomList;
    }

    public void MoveOneTo(List<Card> origin, List<Card> destination)
    {
        List<Card> cardMovedTo = destination;
        cardMovedTo.Add(origin[0]);

        List<Card> deck = origin;
        deck.RemoveAt(0);
    }

    public void MoveOneToTop(List<Card> origin, List<Card> destination)
    {
        List<Card> cardMovedTo = destination;
        //cardMovedTo.Add(origin[0]);
        cardMovedTo.Insert(0, origin[0]);

        List<Card> deck = origin;

        deck.RemoveAt(0);
    }

    public void ShowList(List<Card> list)
    {
        Debug.Log(list.Count);
        foreach (var item in list)
        {
            Debug.Log(item.Ability + "_" + item.Name + " " + item.Level + " " + item.Side + " " + item.Cost + " " + item.Power + " " + item.Rarity + " " + item.Soul + " " + item.Set + " " + item.SID + " " + item.Release + " " + item.CardType + " " + item.Colour);
        }
    }

    public void CombineLists(List<Card> list1, List<Card> list2)
    {
        list2.AddRange(list1);
        list1.Clear();
    }

    IEnumerator DownloadImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
        {
            Texture2D webTexture = ((DownloadHandlerTexture)request.downloadHandler).texture as Texture2D;
            Sprite webSprite = SpriteFromTexture2D(webTexture);
            //cardImage.GetComponent<Image>().sprite = webSprite;
        }
    }

    Sprite SpriteFromTexture2D(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
    }
}
