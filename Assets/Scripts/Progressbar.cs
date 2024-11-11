using UnityEngine;

public class Progressbar : MonoBehaviour
{
    private GameManager manager;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    public void MarkCompleted() => GetComponent<SpriteRenderer>().sprite = manager.progressbarCompleted;
    public void MarkUncompleted() => GetComponent<SpriteRenderer>().sprite = manager.progressbarUncompleted;
}
