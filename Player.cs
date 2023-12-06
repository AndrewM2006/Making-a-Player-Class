using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Making_a_Player_Class
{
    internal class Player
    {
        private Texture2D _texture;
        private Rectangle _location;
        private Vector2 _speed;

        public Player(Texture2D texture, int x, int y)
        {
            _texture = texture;
            _location = new Rectangle(x, y, 30, 30);
            _speed = new Vector2();
        }

        public float HSpeed
        {
            get { return _speed.X; }
            set { _speed.X = value; }
        }

        public float VSpeed
        {
            get { return _speed.Y; }
            set { _speed.Y = value; }
        }

        public Rectangle Location
        {
            get { return _location; }
        }

        private void Move(List<Rectangle> barriers, GraphicsDeviceManager Graphics)
        {
            _location.X += (int)_speed.X;
            if (_location.X + _location.Width > Graphics.PreferredBackBufferWidth || _location.X < 0)
            {
                _location.X -= (int)_speed.X;
            }
            foreach (Rectangle barrier in barriers)
            {
                if (Collide(barrier) == true)
                {
                    _location.X -= (int)_speed.X;
                }
            }
            _location.Y += (int)_speed.Y;
            if (_location.Y + _location.Height > Graphics.PreferredBackBufferHeight || _location.Y < 0)
            {
                _location.Y -= (int)_speed.Y;
            }
            foreach (Rectangle barrier in barriers)
            {
                if (Collide(barrier) == true)
                {
                    _location.Y -= (int)_speed.Y;
                }
            }

        }

        public void Update(List<Rectangle> barriers, GraphicsDeviceManager Graphics)
        {
            Move(barriers, Graphics);
        }

        public bool Collide(Rectangle item)
        {
            return _location.Intersects(item);
        }

        public void Grow()
        {
            _location.Width += 5;
            _location.Height += 5;
        }

        public void UndoMove()
        {
            _location.X -= (int)_speed.X;
            _location.Y -= (int)_speed.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _location, Color.White);
        }
    }
}
