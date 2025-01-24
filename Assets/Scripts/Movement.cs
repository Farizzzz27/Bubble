using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{   
    [SerializeField] private float speed;
    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * 5f, body.linearVelocity.y);

        if(Input.GetKey(KeyCode.Space) && Mathf.Abs(body.linearVelocity.y) < 0.01f)
            body.linearVelocity = new Vector2(body.linearVelocity.x, speed);
    }
}

