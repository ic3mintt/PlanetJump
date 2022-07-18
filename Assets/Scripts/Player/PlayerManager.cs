using UnityEngine;

public class PlayerManager
{
    private readonly Player player;

    public PlayerManager(Player player)
    {
        this.player = player;
        player.CollisionEnter += PlayerCollisionEnter;
    }
    
    public void AttachPlayerToPlanet(Transform planet)
    {
        var playerTransform = player.transform;
        playerTransform.SetParent(planet);
        playerTransform.localPosition = new Vector3(0, 0.5f  + playerTransform.localScale.y , 0);
        player.StartMoveAround(planet);
    }
    
    private void PlayerCollisionEnter(Collision2D other)
    {
        if (other.gameObject.GetComponent<Planet>() != null)
        {
            player.transform.SetParent(other.transform);
            StopPlayer();
            player.StartMoveAround(other.transform);
        }
    }
    
    private void StopPlayer()
    {
        player.Rigidbody.velocity = Vector2.zero;
        player.Rigidbody.angularVelocity = 0;
    }
}