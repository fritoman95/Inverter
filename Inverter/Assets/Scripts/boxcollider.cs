using UnityEngine;

public class boxcollider : MonoBehaviour
{
    public Transform respawn;
    public GameObject box;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("box"))
        {
            collision.gameObject.transform.position = respawn.position;
        }
    }
}