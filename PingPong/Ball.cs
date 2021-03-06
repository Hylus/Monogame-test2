﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace PingPong
{

    public class Ball : AnimatedSprite
    {        
        private Vector2? _startPosition = null;
        private float? _startSpeed;
        private bool _isPlaying;

        public Score Score;

        public Ball(Texture2D texture, int rows, int columns)
          : base(texture,rows,columns,9,16)
        {
            Speed = 3f;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {            
            if (_startPosition == null)
            {
                _startPosition = Position;
                _startSpeed = Speed;

                Restart();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                _isPlaying = true;

            if (!_isPlaying)
                return;

            foreach (var sprite in sprites)
            {
                if (sprite == this)
                    continue;

                if (this.Velocity.X > 0 && this.IsTouchingLeft(sprite))
                {
                    this.Velocity.X = -this.Velocity.X;
                    Speed++;

                    sprite.Shining = true;
                }
                if (this.Velocity.X < 0 && this.IsTouchingRight(sprite))
                {
                    this.Velocity.X = -this.Velocity.X;
                    Speed++;

                    sprite.Shining = true;
                }
                if (this.Velocity.Y > 0 && this.IsTouchingTop(sprite))
                    this.Velocity.Y = -this.Velocity.Y;
                if (this.Velocity.Y < 0 && this.IsTouchingBottom(sprite))
                    this.Velocity.Y = -this.Velocity.Y;
            }

            if (Position.Y <= 0 || Position.Y + base._textureHeight >= Game1.ScreenHeight)
                Velocity.Y = -Velocity.Y;

            if (Position.X <= 0)
            {
                Kaboom();
                Score.Score2++;
                Restart();
            }

            if (Position.X + base._textureWidth >= Game1.ScreenWidth)
            {
                Kaboom();
                Score.Score1++;
                Restart();
            }

            Position += Velocity * Speed;

            base.Update(gameTime, sprites);
        }

        void Kaboom()
        {
           
        }

        public void Restart()
        {
            var direction = Game1.Random.Next(0, 4);

            switch (direction)
            {
                case 0:
                    Velocity = new Vector2(1, 1);
                    break;
                case 1:
                    Velocity = new Vector2(1, -1);
                    break;
                case 2:
                    Velocity = new Vector2(-1, -1);
                    break;
                case 3:
                    Velocity = new Vector2(-1, 1);
                    break;
            }

            Position = (Vector2)_startPosition;
            Speed = (float)_startSpeed;
            
            _isPlaying = false;
        }
    }
    
}