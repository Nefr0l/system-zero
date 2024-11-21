using UnityEngine;

public class ModuleMove : MonoBehaviour
{
    public GameObject Checkbox;
    
    private float offset;
    private GameManager gameManager;
    private ConnectionManager connectionManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        connectionManager = Checkbox.GetComponent<ConnectionManager>();
        offset = gameManager.borderOffset;
    }

    private void OnMouseDown()
    {
        connectionManager.ShowLines();
    }

    private void OnMouseUp()
    {
        connectionManager.HideLines();
    }

    private void OnMouseDrag()
    {
        connectionManager.ShowLines();
        connectionManager.UpdateLines();
    
        // Object drag code remains the same
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

        if (!gameManager.IsWin) transform.position = objectPosition;
    }

}
