using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public CardBase card;

    public string cardImagePath;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        cardImagePath = card.imagePath;
    }
}
