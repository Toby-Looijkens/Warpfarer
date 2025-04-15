using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInputManager playerInputManager;
    [Header("Player movement")]
    [SerializeField] float acceleration = 5f;
    [SerializeField] float maxSpeed = 20f;
    [SerializeField] float turnSpeed = 5f;

    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] Transform gun1;
    [SerializeField] Transform gun2;
    [SerializeField] LaserBolt bolt;
    [SerializeField] float fireRate;

    private bool gun1Fired = false;
    private float gunCooldown = 0;

    private Vector2 target;
    private Quaternion rotGoal;
    private Vector2 direction;


    private void Awake()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
    }

    void Update()
    {
        rigidbody.angularVelocity = 0f;

        if (playerInputManager.movementVector != Vector2.zero)
        {
            MovePlayer();
        } 
        else
        {
            SlowDownPlayer();
        }

        if (rigidbody.linearVelocity.magnitude > maxSpeed)
        {
            rigidbody.linearVelocity = rigidbody.linearVelocity.normalized * maxSpeed;
        }

        if (playerInputManager.isTriggerPulled > 0) 
        {
            Shoot();
        }

        RotatePlayer();
    }

    private void MovePlayer()
    {        
        rigidbody.linearVelocity += playerInputManager.movementVector * acceleration * Time.deltaTime;
    }

    private void RotatePlayer()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        rotGoal = Quaternion.LookRotation(Vector3.forward, (new Vector3(mousePosition.x, mousePosition.y, 0) - transform.position));
        rotGoal = Quaternion.Euler(0, 0, rotGoal.eulerAngles.z + 90);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotGoal, turnSpeed * Time.deltaTime);
    }

    private void SlowDownPlayer()
    {
        Vector2 speedVector = rigidbody.linearVelocity;
        Vector2 invertedSpeedVector = speedVector * -1 * acceleration / 5 * Time.deltaTime;

        if (Mathf.Abs(speedVector.x) >= 0 && Mathf.Abs(speedVector.x) <= Mathf.Abs(invertedSpeedVector.x))
        {
            speedVector.x = 0;
        }
        else
        {
            speedVector.x += invertedSpeedVector.x;
        }

        if (Mathf.Abs(speedVector.y) >= 0 && Mathf.Abs(speedVector.y) <= Mathf.Abs(invertedSpeedVector.y))
        {
            speedVector.y = 0;
        }
        else
        {
            speedVector.y += invertedSpeedVector.y;
        }

        rigidbody.linearVelocity = speedVector;
    }

    private void Shoot()
    {
        if (gunCooldown <= 0)
        {
            if (gun1Fired)
            {
                LaserBolt laserBolt = LaserBolt.Instantiate(bolt, gun2.position, transform.rotation);
                laserBolt.direction = transform.right;
                Debug.Log(laserBolt.direction);
                laserBolt.GetComponent<SpriteRenderer>().enabled = true;
                gun1Fired = false;
                gunCooldown = 60 / fireRate;
            }
            else
            {
                LaserBolt laserBolt = LaserBolt.Instantiate(bolt, gun1.position, transform.rotation);
                laserBolt.direction = transform.right;
                laserBolt.GetComponent<SpriteRenderer>().enabled = true;
                gun1Fired = true;
                gunCooldown = 60 / fireRate;
            }
        }
        else
        { 
            gunCooldown -= Time.deltaTime;
        }
    }
}
