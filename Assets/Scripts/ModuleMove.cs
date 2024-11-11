using Classes;
using UnityEngine;

public class ModuleMove : MonoBehaviour
{
    public bool isBeingDragged;
    
    private float offset;
    private GameManager gameManager;
    
    // add canMove field

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        offset = gameManager.borderOffset;
    }

    private void OnMouseDown()
    {
        isBeingDragged = true;
    }

    private void OnMouseUp()
    {
        isBeingDragged = false;
    }

    private void OnMouseDrag()
    {
        // Object drag
        Vector2 cursorPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint);
        
        float cursorX = cursorPosition.x;
        float cursorY = cursorPosition.y;
        Vector2 objectPosition = cursorPosition;

        // Boundaries for object dragging
        if (cursorX < gameManager.leftBorder + offset) objectPosition.x = gameManager.leftBorder + offset;
        if (cursorX > gameManager.rightBorder - offset) objectPosition.x = gameManager.rightBorder - offset;
        if (cursorY < gameManager.downBorder + offset) objectPosition.y = gameManager.downBorder + offset;
        if (cursorY > gameManager.topBorder - offset) objectPosition.y = gameManager.topBorder - offset;

        transform.position = objectPosition;
    }
}
