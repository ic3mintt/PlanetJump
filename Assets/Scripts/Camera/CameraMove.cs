using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private ICollision2D player;
    
    private void Start()
    {
        player = FindObjectOfType<Player>();
        player.CollisionEnter += StartMove;
    }

    private void StartMove(Collision2D planet)
    {
        Debug.Log("You're not mistaked");
    }
}