using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider2D food;

    void Start()
    {
        food = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var random = new System.Random();
        int x = random.Next(0, 10);
        int y = random.Next(0, 10);
        food.transform.position = new Vector2((float)x, (float)y);
    }
}
