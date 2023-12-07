using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Making_a_Player_Class
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        KeyboardState keyboardState;
        KeyboardState previousState;
        MouseState mouseState;
        MouseState previousMouseState;
        SpriteFont winnerFont;
        Texture2D amoebaTexture;
        Texture2D wallTexture;
        Texture2D foodTexture;
        Texture2D intro1texture;
        Texture2D intro2texture;
        Texture2D intro3texture;
        List<Rectangle> barriers;
        List<Player> amoebas;
        List<Food> feed;
        List<Food> posion;
        int playersReady;
        enum Screen
        {
            Intro,
            Game,
            End
        }
        Screen screen;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.ApplyChanges();
            this.Window.Title = "Size Matters";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            screen = Screen.Intro;
            amoebas = new List<Player>();
            feed = new List<Food>();
            posion = new List<Food>();
            for (int i = 0; i < 10; i++)
            {
                feed.Add(new Food(foodTexture, Color.Green));
            }
            for (int i = 0; i < 5; i++)
            {
                posion.Add(new Food(foodTexture, Color.Red));
            }
            amoebas.Add(new Player(amoebaTexture, 10, 10));
            amoebas.Add(new Player(amoebaTexture, 10, 400));
            barriers = new List<Rectangle>();
            barriers.Add(new Rectangle(100, 100, 10, 200));
            barriers.Add(new Rectangle(400, 400, 100, 10));
            playersReady = 0;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            amoebaTexture = Content.Load<Texture2D>("amoeba");
            wallTexture = Content.Load<Texture2D>("rectangle");
            foodTexture = Content.Load<Texture2D>("circle");
            intro1texture = Content.Load<Texture2D>("Intro 1");
            intro2texture = Content.Load<Texture2D>("Intro 2");
            intro3texture = Content.Load<Texture2D>("Intro 3");
            winnerFont = Content.Load<SpriteFont>("Winner");
            _graphics.PreferredBackBufferWidth=intro1texture.Width;
            _graphics.PreferredBackBufferHeight=intro1texture.Height;
            _graphics.ApplyChanges();
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            previousState = keyboardState;
            keyboardState = Keyboard.GetState();
            previousMouseState = mouseState;
            mouseState = Mouse.GetState();
            if (screen == Screen.Intro)
            {
                if (keyboardState.IsKeyDown(Keys.Space) && previousState.IsKeyUp(Keys.Space))
                {
                    if (playersReady == 0)
                    {
                        playersReady = 1;
                    }
                    else
                    {
                        _graphics.PreferredBackBufferWidth = 800;
                        _graphics.PreferredBackBufferHeight = 480;
                        _graphics.ApplyChanges();
                        screen = Screen.Game;
                    }
                }
                if (mouseState.LeftButton==ButtonState.Pressed && previousMouseState.LeftButton==ButtonState.Released)
                {
                    if (playersReady == 0)
                    {
                        playersReady = 2;
                    }
                    else
                    {
                        _graphics.PreferredBackBufferWidth = 800;
                        _graphics.PreferredBackBufferHeight = 480;
                        _graphics.ApplyChanges();
                        screen = Screen.Game;
                    }
                }
            }
            else if (screen == Screen.Game)
            {
                foreach (Player amoeba in amoebas)
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
                if (keyboardState.IsKeyDown(Keys.R) && previousState.IsKeyUp(Keys.R))
                {
                    amoebas[0].Reset();
                }
                if (keyboardState.IsKeyDown(Keys.RightShift) && previousState.IsKeyUp(Keys.RightShift))
                {
                    amoebas[1].Reset();
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
                            if (feed.Count == 0)
                            {
                                screen = Screen.End;
                            }
                        }
                    }
                    for (int i = 0; i < posion.Count; i++)
                    {
                        if (amoeba.Collide(posion[i].Location))
                        {
                            posion.RemoveAt(i);
                            i--;
                            amoeba.Shrink();
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
                foreach (Food posion in posion)
                {
                    posion.Move(barriers, _graphics);
                }
            }
            else if (screen == Screen.End)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    Exit();
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            if (screen == Screen.Intro)
            {
                if (playersReady == 0)
                {
                    _spriteBatch.Draw(intro1texture, new Vector2(0, 0), Color.White);
                }
                else if (playersReady == 1)
                {
                    _spriteBatch.Draw(intro2texture, new Vector2(0, 0), Color.White);
                }
                else if (playersReady == 2)
                {
                    _spriteBatch.Draw(intro3texture, new Vector2(0, 0), Color.White);
                }
            }
            else if (screen == Screen.Game)
            {
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
                foreach (Food posion in posion)
                {
                    posion.Draw(_spriteBatch);
                }
            }
            else if (screen == Screen.End)
            {
                if (amoebas[0].Location.Width> amoebas[1].Location.Width)
                {
                    _spriteBatch.DrawString(winnerFont, "Player 1 Wins", new Vector2(20, 170), Color.White);
                }
                else if (amoebas[1].Location.Width > amoebas[0].Location.Width)
                {
                    _spriteBatch.DrawString(winnerFont, "Player 2 Wins", new Vector2(20, 170), Color.White);
                }
                else
                {
                    _spriteBatch.DrawString(winnerFont, "Everyone Wins!", new Vector2(20, 170), Color.White);
                }
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}