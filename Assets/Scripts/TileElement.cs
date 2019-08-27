using UnityEngine;
using System.Collections;

public class TileElement : MonoBehaviour
{

    bool MoveWithMouse = false;
    bool CollidedWithOther = false;

    TileMap TM;
    TileSpriteManager TSM;
    Stack Sprites;
    GameObject triggered;
    
    // Use this for initialization
    void Awake()
    {
        TM = FindObjectOfType<TileMap>();
        TSM = FindObjectOfType<TileSpriteManager>();
    }
    void Update()
    {
        if (MoveWithMouse && GetComponent<SpriteRenderer>().enabled)
        {
            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        }
    }
    void OnMouseDown()
    {
        MoveWithMouse = true;
    }
    void OnMouseUp()
    {
        MoveWithMouse = false;
        if (CollidedWithOther)
        {
            OntoOtherOne();
        }
        else
        {
            BackToParent();
        }       
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<TileElement>())
        {
            triggered = col.gameObject;
            CollidedWithOther = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<TileElement>() )
        {
            CollidedWithOther = false;
        }
    }
    void OntoOtherOne()
    {
        if(triggered.GetComponent<TileElement>().Sprites.Count == Sprites.Count && Sprites.Count !=0 )
        {
            Merge();
        }
        else
        {
            Swap();
        }
    }
    void Merge()
    {
        triggered.GetComponent<TileElement>().UpdateTileElementSprite();
        Hide();
    }
    void Hide()
    {
        BackToParent();
        ResetStack();
        Appear(false);
        TM.EmptyTile(this);
    }    
    void Swap()
    {
        Transform parent = triggered.transform.parent;
        triggered.transform.parent = this.transform.parent;
        this.transform.parent = parent;
        triggered.GetComponent<TileElement>().BackToParent();
        BackToParent();
    }
    public void Appear(bool enable)
    {
        GetComponent<SpriteRenderer>().enabled = enable;
        if (enable)
        {
            UpdateTileElementSprite();
        }
    }
     void UpdateTileElementSprite()
    {
        GetComponent<SpriteRenderer>().sprite = (Sprite)Sprites.Pop();
    }
    public void InitTileElement()
    {
        ResetStack();
        BackToParent();
        Appear(false);
        GetComponent<BoxCollider2D>().size = ((Sprite)Sprites.Peek()).bounds.size;
    }
    void ResetStack()
    {
        Sprites = TSM.GetTileSpritesStack();
    }
    void BackToParent()
    {
        this.transform.position = transform.parent.position;
    }
}
