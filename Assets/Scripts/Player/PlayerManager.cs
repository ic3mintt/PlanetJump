using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    private Player player;
    [SerializeField] private PlanetManager planetManager;

    #region PlayerOffset

    /*
        var xOffsetFromCenter = player.localScale.x / 2;
        var yOffsetFromCenter = player.localScale.y;
    */

    #endregion

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        planetManager.AttachToPlanetAtStart += AttachPlayerToPlanetAtStart;
        player.CollisionEnter += PlayerCollisionEnter;
    }

    private void AttachPlayerToPlanetAtStart()
    {
        var planet = planetManager.GetPlanetFromList().transform;
        var playerTransform = player.transform;
        playerTransform.SetParent(planet);
        playerTransform.localPosition = new Vector3(0, 0.5f  + playerTransform.localScale.y , 0);
        playerTransform.SetParent(null);
    }
    
    private void PlayerCollisionEnter(Collision2D other)
    {
        if (other.gameObject.CompareTag("Planet"))
        {
            AttachPlayerToPlanet(other);
            player.StartMoveAround(other.transform.position);
        }
    }

    private void AttachPlayerToPlanet(Collision2D planet)
    {
        var playerRigidbody = player.GetComponent<Rigidbody2D>();
        playerRigidbody.velocity = Vector2.zero;
        playerRigidbody.angularVelocity = 0;
    }
}