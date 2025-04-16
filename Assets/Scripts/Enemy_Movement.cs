using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy_Movement : MonoBehaviour
{
    private float turnSpeed = 400f;
    private float speed = 5f;
    [SerializeField] Transform gun1;
    [SerializeField] LaserBolt bolt;
    [SerializeField] float fireRate = 500f;
    [SerializeField] GameObject player;

    private float gunCooldown = 0f;

    private void Update()
    {
        MoveEnemy();
        RotateEnemy();
        Shoot();
    }

    private void MoveEnemy()
    {
        if ((player.transform.position - transform.position).magnitude > 5)
        {
            transform.position = transform.position + transform.right * speed * Time.deltaTime;
        }
    }

    private void RotateEnemy()
    {
        Quaternion rotGoal;
        rotGoal = Quaternion.LookRotation(Vector3.forward,  (player.transform.position - transform.position).normalized);
        rotGoal = Quaternion.Euler(0, 0, rotGoal.eulerAngles.z + 90);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotGoal, turnSpeed * Time.deltaTime);
    }

    private void Shoot() 
    {
        if (Vector2.Dot(transform.up, player.transform.position - transform.position) < 2 && (player.transform.position - transform.position).magnitude < 15) 
        {
            if (gunCooldown <= 0)
            {
                    LaserBolt laserBolt = LaserBolt.Instantiate(bolt, gun1.position, transform.rotation);
                    laserBolt.direction = transform.right;
                    laserBolt.GetComponent<SpriteRenderer>().enabled = true;
                    gunCooldown = 60 / fireRate;
            }
            else
            {
                gunCooldown -= Time.deltaTime;
            }
        }
    }
}
