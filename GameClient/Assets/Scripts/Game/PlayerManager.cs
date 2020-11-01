using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;
    public float health;
    public float maxHealth = 100f;
    public MeshRenderer model;

    public Action<float> onHealthChanged;
    public Action<int> onDied;
    public Action<int> onItemCountChanged;

    int itemCount = 0;
    int died = 0;

    public void Initialize(int _id, string _username)
    {
        id = _id;
        username = _username;
        SetHealth(maxHealth);
    }

    public void SetHealth(float _health)
    {
        health = _health;

        onHealthChanged?.Invoke(health);
        if (IsMine()) UIManager.ins.txtHealth.text = health.ToString();

        if (health <= 0f)
        {
            Die();
        }
    }

    bool IsMine()
    {
        return id == Client.instance.myId;
    }

    public void Die()
    {
        died++;
        model.enabled = false;
        onDied?.Invoke(died);
        if (IsMine()) UIManager.ins.txtKilled.text = died.ToString();
    }

    public void ItemAdd(int count)
    {
        itemCount += count;
        onItemCountChanged?.Invoke(itemCount);
        if (IsMine()) UIManager.ins.txtBomb.text = itemCount.ToString();
    }

    public void Respawn()
    {
        model.enabled = true;
        SetHealth(maxHealth);
    }
}
