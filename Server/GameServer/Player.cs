﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GameServer
{
    class Player
    {
        public int id;
        public string username;

        public Vector3 position;
        public Quaternion rotation;

        float moveSpeed = 5f / Constants.TICKS_PER_SEC;
        bool[] inputs;

        public Player(int _id, string _username, Vector3 _spawnPosition)
        {
            id = _id;
            username = _username;
            position = _spawnPosition;
            rotation = Quaternion.Identity;

            inputs = new bool[4];
        }

        public void Update()
        {
            var inputDirection = Vector2.Zero;
            if (inputs[0]) inputDirection.Y += 1;
            if (inputs[1]) inputDirection.Y -= 1;
            if (inputs[2]) inputDirection.X += 1;
            if (inputs[3]) inputDirection.X -= 1;

            Move(inputDirection);
        }

        private void Move(Vector2 inputDirection)
        {
            var forward = Vector3.Transform(new Vector3(0, 0, 1), rotation);
            var right = Vector3.Normalize(Vector3.Cross(forward, new Vector3(0, 1, 0)));

            var moveDirection = right * inputDirection.X + forward * inputDirection.Y;
            position += moveDirection * moveSpeed;

            ServerSend.PlayerPosition(this);
            ServerSend.PlayerRotation(this);
        }

        internal void SetInput(bool[] _inputs, Quaternion _rotation)
        {
            inputs = _inputs;
            rotation = _rotation;
        }
    }
}
