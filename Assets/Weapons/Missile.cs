using NUnit.Framework;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;

public class Missile : MonoBehaviour
{
    private float maxLifeTime = 5f;
    private float timeAlive = 0f;
    private float speed = 5f;
    private float turnSpeed = 200f;
    public Vector2 direction = Vector2.zero;
    [SerializeField] float damage;

    private Transform closestEnemy;

    private void Start()
    {
    }

    void Update()
    {
        Quaternion rotGoal = Quaternion.identity;
        //rotGoal = Quaternion.LookRotation(Vector3.forward, );
        rotGoal = Quaternion.Euler(0, 0, rotGoal.eulerAngles.z + 90);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotGoal, turnSpeed * Time.deltaTime);

        transform.position = new Vector2(transform.position.x, transform.position.y) + direction * speed * Time.deltaTime;

        if (timeAlive > maxLifeTime)
        {
            Destroy(gameObject);
        }
        else
        {
            timeAlive += Time.deltaTime;
        }
    }
}
