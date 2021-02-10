using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestButton : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Card> list = new List<Card>();
    void Start()
    {

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
}
