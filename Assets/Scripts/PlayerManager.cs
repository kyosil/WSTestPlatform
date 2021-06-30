using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class PlayerManager : NetworkBehaviour
{
    public GameObject Card1;
    public GameObject Card2;
    public GameObject CardBase;
    public GameObject MyHand;
    public GameObject OpponentHand;
    public GameObject DropZone;
    
    private Sprite sprt;

    List<GameObject> cards = new List<GameObject>();
    List<Card> deck = GameManager.LoadDeckFromURL("https://www.encoredecks.com/api/deck/3MaHWilwW");

    public override void OnStartClient()
    {
        base.OnStartClient();

        MyHand = GameObject.Find("MyHand");
        OpponentHand = GameObject.Find("OpponentHand");
        DropZone = GameObject.Find("DropZone (1)");
    }

    [Server]
    public override void OnStartServer()
    {
        foreach(var card in deck)
        {
            GameObject newCard = CardBase;
            newCard.GetComponent<CardDescription>().Name = card.Name;
            newCard.GetComponent<CardDescription>().ImagePath = card.ImagePath;
            cards.Add(newCard);
        }
    }

    [Command] //As a client, if you try to do any network related but asking from a client perspective. A command is used. e.g. change my number, update my position
    // Make sure to add "Cmd" as ppart of your function name for it to run properly
    public void CmdDealCards()
    {
        for (var i = 0; i < 5; i++)
        {
            Card newCard = deck[Random.Range(0, deck.Count)]; 
            GameObject cardObject = CardBase;            
            cardObject.GetComponent<CardDescription>().Name = newCard.Name;
            cardObject.GetComponent<CardDescription>().ImagePath = newCard.ImagePath;

            GameObject card = Instantiate(cardObject, new Vector2(0, 0), Quaternion.identity);
            NetworkServer.Spawn(card, connectionToClient); //the second part determines who has authority, in this case "connectionToClient" determines that the authority comes from the client itself
            RpcShowCard(card, "Dealt");
            /*
            GameObject card = Instantiate(cards[Random.Range(0, 50)], new Vector2(0, 0), Quaternion.identity);
            Debug.Log(card.GetComponent<CardDescription>().ImagePath);
            NetworkServer.Spawn(card, connectionToClient); //the second part determines who has authority, in this case "connectionToClient" determines that the authority comes from the client itself
            RpcShowCard(card, "Dealt");*/
        }
    }

    public void PlayCard(GameObject card)
    {
        CmdPlayCard(card);
    }

    void CmdPlayCard(GameObject card)
    {
        RpcShowCard(card, "Played");
    }

    [ClientRpc] //RPC - Remove Procedure Call. Here you determine how the client handles commands it receives
    //Similar to Command with Cmd as prefix to function name, Rpc is added
    void RpcShowCard(GameObject card, string type)
    {
        if (type == "Dealt")
        {
            if (hasAuthority)
            {
                card.transform.SetParent(MyHand.transform, false);
            }
            else
            {
                card.transform.SetParent(OpponentHand.transform, false);
            }
        }
        else if (type == "Played")
        {
            card.transform.SetParent(DropZone.transform, false);
        }
    }
}
