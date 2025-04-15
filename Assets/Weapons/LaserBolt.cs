using UnityEngine;

public class LaserBolt : MonoBehaviour
{
    private float maxLifeTime = 2f;
    private float timeAlive = 0f;
    private float speed = 20f;
    public Vector2 direction = Vector2.zero;
    [SerializeField] float damage;

    void Update()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y) + direction * speed * Time.deltaTime;

        if (timeAlive > maxLifeTime)
        {
            Destroy(gameObject);
        } else
        {
            timeAlive += Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.collider.SendMessage("ReceiveDamage");
        }
    }
}
