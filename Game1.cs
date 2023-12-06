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
        List<Player> amoebas;
        List<Food> feed;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.ApplyChanges();
            this.Window.Title = "Amoeba Game";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            amoebas = new List<Player>();
            feed = new List<Food>();
            for (int i = 0; i < 10; i++)
            {
                feed.Add(new Food(foodTexture));
            }
            amoebas.Add(new Player(amoebaTexture, 10, 10));
            amoebas.Add(new Player(amoebaTexture, 10, 40));
            barriers = new List<Rectangle>();
            barriers.Add(new Rectangle(100, 100, 10, 200));
            barriers.Add(new Rectangle(400, 400, 100, 10));
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
            foreach(Player amoeba in amoebas)
            {
                amoeba.HSpeed = 0;
                amoeba.VSpeed = 0;
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                amoebas[0].HSpeed += 3;
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                amoebas[0].HSpeed += -3;
            }
            if (keyboardState.IsKeyDown(Keys.W))
            {
                amoebas[0].VSpeed += -3;
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                amoebas[0].VSpeed += 3;
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                amoebas[1].HSpeed += 3;
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                amoebas[1].HSpeed += -3;
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                amoebas[1].VSpeed += -3;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                amoebas[1].VSpeed += 3;
            }

            foreach (Player amoeba in amoebas)
            {
                amoeba.Update(barriers, _graphics);

                for (int i = 0; i < feed.Count; i++)
                {
                    if (amoeba.Collide(feed[i].Location))
                    {
                        feed.RemoveAt(i);
                        i--;
                        amoeba.Grow();
                    }
                }
            }
            if (amoebas[0].Collide(amoebas[1].Location))
            {
                amoebas[0].UndoMove();
            }
            if (amoebas[1].Collide(amoebas[0].Location))
            {
                amoebas[1].UndoMove();
            }
            foreach (Food feed in feed)
            {
                feed.Move(barriers, _graphics);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            foreach (Player amoeba in amoebas)
            {
                amoeba.Draw(_spriteBatch);
            }
            foreach (Rectangle barrier in barriers)
            {
                _spriteBatch.Draw(wallTexture, barrier, Color.White);
            }
            foreach (Food feed in feed)
            {
                feed.Draw(_spriteBatch);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}