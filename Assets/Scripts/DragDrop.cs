using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class DragDrop : NetworkBehaviour
{
    public GameObject Canvas;
    public GameObject DropZone;
    public PlayerManager PlayerManager;
    private bool isDragging = false;
    private bool isOverDropZone = false;
    private bool isDraggable = true;
    private GameObject dropZone;
    private GameObject startParent;
    private Vector2 startPosition;

    private GameObject text;
    Ray ray;
    RaycastHit hit;

    private void Start()
    {
        Canvas = GameObject.Find("Main Canvas");
        text = GameObject.Find("Text");
        if (!hasAuthority)
        {
            isDraggable = false;
        }
    }
    void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    Debug.Log("Hello");
                }
            }
        }*/

        if (isDragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            transform.SetParent(Canvas.transform, true);
            
        }
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOverDropZone = true;
        dropZone = collision.gameObject;
        //Debug.Log(dropZone.name);
        text.GetComponent<Text>().text = dropZone.name;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isOverDropZone = false;
        dropZone = null;
        Debug.Log("Left");
        text.GetComponent<Text>().text = "";
    }

    public void StartDrag()
    {
        if (!isDraggable) return;
        startParent = transform.parent.gameObject;
        startPosition = transform.position;
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(5,5);
        isDragging = true;
    }

    public void EndDrag()
    {
        if (!isDraggable) return;
        isDragging = false;
        if (isOverDropZone)
        {
            transform.SetParent(dropZone.transform, false);
            NetworkIdentity networkIdentity = NetworkClient.connection.identity;
            PlayerManager = networkIdentity.GetComponent<PlayerManager>();
            PlayerManager.PlayCard(gameObject);
        }
        else
        {
            transform.position = startPosition;
            transform.SetParent(startParent.transform, false);
        }
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(110, 180);
    }
}
