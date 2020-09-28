using UnityEngine;

public class Crosshairs : MonoBehaviour
{

    [SerializeField]
    private Sprite crosshairs;
    [SerializeField]
    private Sprite crosshairsTargert;
    private Sprite currentsprite;
    public SpriteRenderer spriteRenderer;

    [SerializeField]
    private LayerMask HitableLayers;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Vector2 mousePos;

    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        gameObject.transform.position = mousePos;

        if (Physics2D.Raycast(mousePos, Vector3.forward, 100, HitableLayers))
        {
            currentsprite = crosshairsTargert;
        }

        else
        {
            currentsprite = crosshairs;
        }

        //if gun is child of player show crosshairs
        if(player.transform.Find("Gun").gameObject)
        {
            spriteRenderer.enabled = true;
            spriteRenderer.sprite = currentsprite;

            Debug.Log("Gun is child of player");

            spriteRenderer.size = new Vector2(3, 3);
        }


        //if gun isnt child of player dont show crosshairs
        else
        {
            spriteRenderer.enabled = false;
        }
    }

    //things to fix
    //crosshair sizes, crosshair jumping when the player moves, and have a raycast shoot from the camera towards the cursor and check if it
    //hits an enemy
}
