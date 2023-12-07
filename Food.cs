using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Making_a_Player_Class
{
    internal class Food
    {
        private Texture2D _texture;
        private Rectangle _location;
        private Vector2 _speed;
        private Color _color;
        Random generator = new Random();

        public Food(Texture2D texture, Color color)
        {
            _texture = texture;
            _color = color;
            do
            {
                _speed = new Vector2(generator.Next(-5, 5), generator.Next(-5, 5));
            }
            while (_speed.X == 0 || _speed.Y == 0);
            _location = new Rectangle(400, 240, 10, 10);
        }

        public void Draw(SpriteBatch sprite)
        {
            sprite.Draw(_texture, _location, _color);
        }

        public void Move(List<Rectangle> barriers, GraphicsDeviceManager graphics)
        {
            _location.X += (int)_speed.X;
            foreach (Rectangle barrier in barriers)
            {
                if (_location.Intersects(barrier))
                {
                    _speed.X *= -1;
                    _location.X += (int)_speed.Y;
                }
            }
            _location.Y += (int)_speed.Y;
            foreach (Rectangle barrier in barriers)
            {
                if (_location.Intersects(barrier))
                {
                    _speed.Y *= -1;
                    _location.Y += (int)_speed.Y;
                }
            }
            if (_location.X + _location.Width > graphics.PreferredBackBufferWidth || _location.X < 0)
            {
                _speed.X *= -1;
            }
            if (_location.Y + _location.Height > graphics.PreferredBackBufferHeight || _location.Y < 0)
            {
                _speed.Y *= -1;
            }
        }

        public Rectangle Location
        {
            get { return _location; }
        }
    }
}
