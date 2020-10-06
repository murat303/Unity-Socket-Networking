using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(this);
    }
}
