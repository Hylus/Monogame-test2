using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingPong
{
    public class AnimatedSprite : Sprite
    {
        public int Rows, Columns;
        int _currentFrame, _totalFrames;

        int _width, _height;

        int timeSinceLastFrame = 0;
        int milisecondsPerFrame;

        public AnimatedSprite(Texture2D texture, int rows, int columns,int missingFrames, int msecondsPerFrame) : base(texture)
        {
            milisecondsPerFrame = msecondsPerFrame;
            Rows = rows;
            Columns = columns;
            _currentFrame = 0;
            _totalFrames = Rows * Columns - missingFrames;

            _width = texture.Width / Columns;
            _height = texture.Height / Rows;

            base._textureWidth = _width;
            base._textureHeight = _height;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > milisecondsPerFrame)
            {
                timeSinceLastFrame -= milisecondsPerFrame;

                _currentFrame++;                
                if (_currentFrame == _totalFrames)
                    _currentFrame = 0;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {            
            int row = (int)((float)_currentFrame / (float)Columns);
            int column = _currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(_width * column, _height * row, _width, _height);
            Rectangle destinationRectangle = new Rectangle((int)base.Position.X, (int)base.Position.Y, _width, _height);

            spriteBatch.Draw(base._texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
