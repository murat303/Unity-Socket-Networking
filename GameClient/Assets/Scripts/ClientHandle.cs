﻿using System;
using System.Net;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet packet)
    {
        string msg = packet.ReadString();
        int myId = packet.ReadInt();

        Debug.Log($"Message from Server: {msg}");
        Client.instance.myId = myId;
        ClientSend.WelcomeReceived();

        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void SpawnPlayer(Packet packet)
    {
        int id = packet.ReadInt();
        string username = packet.ReadString();
        Vector3 position = packet.ReadVector3();
        Quaternion rotation = packet.ReadQuaternion();

        GameManager.instance.SpawnPlayer(id, username, position, rotation);
    }

    public static void PlayerPosition(Packet packet)
    {
        int id = packet.ReadInt();
        Vector3 position = packet.ReadVector3();

        GameManager.players[id].transform.position = position;
    }

    public static void PlayerRotation(Packet packet)
    {
        int id = packet.ReadInt();
        Quaternion rotation = packet.ReadQuaternion();

        GameManager.players[id].transform.rotation = rotation;
    }

    public static void PlayerDisconnected(Packet packet)
    {
        int id = packet.ReadInt();

        Destroy(GameManager.players[id].gameObject);
        GameManager.players.Remove(id);
    }

    public static void PlayerHealth(Packet _packet)
    {
        int _id = _packet.ReadInt();
        float _health = _packet.ReadFloat();

        GameManager.players[_id].SetHealth(_health);
    }

    public static void PlayerRespawned(Packet _packet)
    {
        int _id = _packet.ReadInt();

        GameManager.players[_id].Respawn();
    }

    public static void CreateItemSpawner(Packet _packet)
    {
        int _spawnerId = _packet.ReadInt();
        Vector3 _spawnerPosition = _packet.ReadVector3();
        bool _hasItem = _packet.ReadBool();

        GameManager.instance.CreateItemSpawner(_spawnerId, _spawnerPosition, _hasItem);
    }

    public static void ItemSpawned(Packet _packet)
    {
        int _spawnerId = _packet.ReadInt();

        GameManager.itemSpawners[_spawnerId].ItemSpawned();
    }

    public static void ItemPickedUp(Packet _packet)
    {
        int _spawnerId = _packet.ReadInt();
        int _byPlayer = _packet.ReadInt();

        GameManager.itemSpawners[_spawnerId].ItemPickedUp();
        GameManager.players[_byPlayer].itemCount++;
    }
}
