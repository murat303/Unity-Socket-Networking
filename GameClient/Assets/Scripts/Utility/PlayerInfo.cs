using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] Text playerName;
    [SerializeField] Text playerHealth;
    Transform target;

    void Start()
    {
        target = Camera.main.transform;
        var player = GetComponentInParent<PlayerManager>();
        if (player)
        {
            playerName.text = player.username;
            player.onHealthChanged += OnHealthChanged;
        }
    }

    void OnHealthChanged(float health)
    {
        playerHealth.text = health.ToString();
    }

    void Update()
    {
        transform.LookAt(target);
    }
}
