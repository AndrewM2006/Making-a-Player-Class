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

        private void Move(List<Rectangle> barriers)
        {
            _location.Offset(_speed);
            
            foreach (Rectangle barrier in barriers) 
            { 
               if () 
            }
          
        }

        public void Update(List<Rectangle> barriers)
        {
            _speed.Normalize();
            Move(barriers);
        }

        public bool Collide(Rectangle item)
        {
            return _location.Intersects(item);
        }

        public void UndoMoveLeft(Rectangle collide)
        {
            _location.X = collide.Right;
        }

        public void UndoMoveRight(Rectangle collide)
        {
            _location.X = collide.Left - _location.Width;
        }

        public void UndoMoveDown(Rectangle collide)
        {
            _location.Y = collide.Top - _location.Height;
        }

        public void UndoMoveUp(Rectangle collide)
        {
            _location.Y = collide.Bottom;
        }

        public void Grow()
        {
            _location.Width += 5;
            _location.Height += 5;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _location, Color.White);
        }
    }
}
