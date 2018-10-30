using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace PingPong
{
    public class Bat : Sprite
    {
        int timeSinceLastFrame = 0;
        int shiningMiliseconds = 300;


        public Bat(Texture2D texture)
          : base(texture)
        {
            Speed = 5f;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            if (Keyboard.GetState().IsKeyDown(Input.Up))
                Velocity.Y = -Speed;
            else if (Keyboard.GetState().IsKeyDown(Input.Down))
                Velocity.Y = Speed;

            Position += Velocity;

            Position.Y = MathHelper.Clamp(Position.Y, 0, Game1.ScreenHeight - _texture.Height);

            Velocity = Vector2.Zero;

            if (Shining)
            {
                ShineBat(gameTime);                
            }
        }

        void ShineBat(GameTime gameTime)
        {
            base.Color = Color.Yellow;
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > shiningMiliseconds)
            {
                timeSinceLastFrame -= shiningMiliseconds;
                base.Color = Color.White;
                base.Shining = false;
            }
        }
    }
}