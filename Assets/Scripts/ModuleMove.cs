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
        
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint);
        Vector3 objectPosition = cursorPosition;

        if (objectPosition.x < gameManager.leftBorder + offset) objectPosition.x = gameManager.leftBorder + offset;
        if (objectPosition.x > gameManager.rightBorder - offset) objectPosition.x = gameManager.rightBorder - offset;
        if (objectPosition.y < gameManager.downBorder + offset) objectPosition.y = gameManager.downBorder + offset;
        if (objectPosition.y > gameManager.topBorder - offset) objectPosition.y = gameManager.topBorder - offset;

        float newX = CanMoveX ? objectPosition.x : transform.position.x;
        float newY = CanMoveY ? objectPosition.y : transform.position.y;
        Vector3 newPos = new Vector3(newX, newY, 0);
        
        transform.position = newPos;
        connectionManager.UpdateLines(gameObject);
    }

}
