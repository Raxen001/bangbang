using UnityEngine;

public class ShootButton : MonoBehaviour
{
    private GameObject player;
    private GunHandeler bulletHandler;

    public void AssignValues()
    {
        player = FindObjectOfType<MyNetworkManager>().FindLocalPlayer();
        bulletHandler = player.GetComponent<GunHandeler>();
    }
   public void ShootCall()
    {
        if (bulletHandler == null) return;
        bulletHandler.Shoot();  
    }
}
