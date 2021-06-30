﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TestButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cardObject;
    private List<Card> list = new List<Card>();
    private string cardUrl = "https://www.encoredecks.com/images/EN/S50/E002.gif";


    void Start()
    {
        //StartCoroutine(GetTexture());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Test(List<Card> list)
    {
        Debug.Log("Test");
    }

    public void RandomizeList()
    {
        var rand = new System.Random();
        var randomList = list.OrderBy(x => rand.Next()).ToList();

        GameManager _gm = new GameManager();
        _gm.ShowList(randomList);

        //
    }

    IEnumerator DownloadImage(string MediaUrl, GameObject cardObject)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
        {
            Texture2D webTexture = ((DownloadHandlerTexture)request.downloadHandler).texture as Texture2D;
            Sprite webSprite = SpriteFromTexture2D(webTexture);
            cardObject.GetComponent<Image>().sprite = webSprite;
        }
    }

    Sprite SpriteFromTexture2D(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
    }

}
