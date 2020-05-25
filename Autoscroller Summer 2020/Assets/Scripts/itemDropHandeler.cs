﻿using UnityEngine;
using UnityEngine.EventSystems;

public class itemDropHandeler : MonoBehaviour, IDropHandler
{
    public GameObject player;
    private float pgAvalible;
    private float wgAvalible;
    public float wgCost;
    public float pgCost;
    public GameObject spriteLoc;
    private fittingScript parent;
    public Sprite[] swapsprites;
    public Sprite otherSprite;
    public Sprite cSprite;
    public int itemId;
    public ItemDragHandler.Slot typeOfItem = ItemDragHandler.Slot.HIGH;
    public int inputLoc;
    public bool slotNull = false;
    public bool slotNeedUpdate;
    public void OnDrop(PointerEventData eventData)
    {
        ItemDragHandler d = eventData.pointerDrag.GetComponent<ItemDragHandler>();
        pgCost = eventData.pointerDrag.GetComponent<ItemDragHandler>().pgCost;
        wgCost = eventData.pointerDrag.GetComponent<ItemDragHandler>().wgCost;
        pgAvalible = player.GetComponent<fittingScript>().power;
        wgAvalible = player.GetComponent<fittingScript>().Weight;

        if (d != null)
        {
            if (typeOfItem == d.typeOfItem)
            {
                if (pgAvalible >= pgCost && wgAvalible >= wgCost)
                {
                    itemId = eventData.pointerDrag.GetComponent<ItemDragHandler>().itemID;
                    otherSprite = eventData.pointerDrag.GetComponent<SpriteRenderer>().sprite;
                    spriteLoc.GetComponent<SpriteRenderer>().sprite = otherSprite;
                    parent = player.GetComponent<fittingScript>();
                    parent.inputLoc = inputLoc;
                    parent.input();
                }
            }
        }


    }
    public void reset()
    {
        itemId = 0;
        spriteLoc.GetComponent<SpriteRenderer>().sprite = swapsprites[itemId];
        if (slotNull == true)
        {
            //dissable sprite renderers
            this.GetComponent<SpriteRenderer>().enabled = false;
            spriteLoc.GetComponent<SpriteRenderer>().enabled = false;
            //dissable collider
            this.GetComponent<PolygonCollider2D>().enabled = false;
            slotNeedUpdate = false;
        }
        if (slotNull == false)
        {
            //dissable sprite renderers
            this.GetComponent<SpriteRenderer>().enabled = true;
            spriteLoc.GetComponent<SpriteRenderer>().enabled = true;
            //dissable collider
            this.GetComponent<PolygonCollider2D>().enabled = true;
            slotNeedUpdate = false;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (slotNeedUpdate == true)
        {
            reset();
        }
    }
}
