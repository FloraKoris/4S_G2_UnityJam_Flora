using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticalBehavior : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float obstacleSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector2(-obstacleSpeed / 10, 0), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0f;
        }
    }
}
