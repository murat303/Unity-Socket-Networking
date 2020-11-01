using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public static PlayerSpawner ins;
    public Transform[] points;
        
    void Awake()
    {
        ins = this;
    }

    public Transform GetRandomPoint()
    {
        return points[Random.Range(0, points.Length)];
    }
}
