using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DeckAction : NetworkBehaviour
{
    public PlayerManager PlayerManager;
    

    public void OnClickDrawCard()
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        PlayerManager.CmdDealCards();
    }

}
