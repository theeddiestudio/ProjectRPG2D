using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager manager; // access from anywhere
    public Player player;

    private void Awake()
    {
        if (manager != null) 
            Destroy(manager.gameObject);
        else
            manager = this;
    }
}
