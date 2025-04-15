using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy_Movement : MonoBehaviour
{
    private float turnSpeed = 500f;
    [SerializeField] GameObject player;

    private void Update()
    {
        RotatePlayer();
    }

    private void MovePlayer()
    {
    }

    private void RotatePlayer()
    {
        Quaternion rotGoal;
        rotGoal = Quaternion.LookRotation(Vector3.forward,  (player.transform.position - transform.position).normalized);
        rotGoal = Quaternion.Euler(0, 0, rotGoal.eulerAngles.z + 90);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotGoal, turnSpeed * Time.deltaTime);
    }
}
