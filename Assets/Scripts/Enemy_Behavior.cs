using UnityEngine;

public class Enemy_Behavior : MonoBehaviour
{
    [SerializeField] int health = 100;

    private void ReceiveDamage()
    {
        health -= 5;
        if (health <0)
        {
            Destroy(gameObject);
        }
    }
}
