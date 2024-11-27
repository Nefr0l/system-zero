using UnityEngine;

public class ModuleMove : MonoBehaviour
{
    public GameObject Checkbox;
    public bool CanMoveX = true;
    public bool CanMoveY = true;
    
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
        if (GameManager.IsWin) return;
        connectionManager.UpdateLines(gameObject);
        
        Vector2 cursorPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint);
        Vector2 objectPosition = cursorPosition;
                
        float cursorX = cursorPosition.x;
        float cursorY = cursorPosition.y;

        if (cursorX < gameManager.leftBorder + offset) objectPosition.x = gameManager.leftBorder + offset;
        if (cursorX > gameManager.rightBorder - offset) objectPosition.x = gameManager.rightBorder - offset;
        if (cursorY < gameManager.downBorder + offset) objectPosition.y = gameManager.downBorder + offset;
        if (cursorY > gameManager.topBorder - offset) objectPosition.y = gameManager.topBorder - offset;

        Vector2 newPos = new Vector2(CanMoveX ? objectPosition.x : transform.position.x,
            CanMoveY ? objectPosition.y : transform.position.y);
        
        transform.position = newPos;
    }

}
