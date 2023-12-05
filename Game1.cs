using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Making_a_Player_Class
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        KeyboardState keyboardState;
        Texture2D amoebaTexture;
        Texture2D wallTexture;
        Texture2D foodTexture;
        List<Rectangle> barriers;
        List<Rectangle> food;
        Player amoeba;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.ApplyChanges();
            this.Window.Title = "Player Class";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            amoeba = new Player(amoebaTexture, 10, 10);
            barriers = new List<Rectangle>();
            food = new List<Rectangle>();
            barriers.Add(new Rectangle(100, 100, 10, 200));
            barriers.Add(new Rectangle(400, 400, 100, 10));
            food.Add(new Rectangle(50, 50, 10, 10));
            food.Add(new Rectangle(600, 100, 10, 10));
            food.Add(new Rectangle(50, 200, 10, 10));
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            amoebaTexture = Content.Load<Texture2D>("amoeba");
            wallTexture = Content.Load<Texture2D>("rectangle");
            foodTexture = Content.Load<Texture2D>("circle");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            keyboardState = Keyboard.GetState();
            amoeba.HSpeed = 0;
            amoeba.VSpeed = 0;
            if (keyboardState.IsKeyDown(Keys.D))
            {
                amoeba.HSpeed += 3;
               
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                amoeba.HSpeed += -3;
                
            }
            if (keyboardState.IsKeyDown(Keys.W))
            {
                amoeba.VSpeed += -3;
               
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                amoeba.VSpeed += 3;
                
            }

            amoeba.Update(barriers);

            for (int i = 0; i < food.Count; i++)
            {
                if (amoeba.Collide(food[i]))
                {
                    food.RemoveAt(i);
                    i--;
                    amoeba.Grow();
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            amoeba.Draw(_spriteBatch);
            foreach (Rectangle barrier in barriers)
            {
                _spriteBatch.Draw(wallTexture, barrier, Color.White);
            }
            foreach (Rectangle bit in food)
            {
                _spriteBatch.Draw(foodTexture, bit, Color.Green);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}