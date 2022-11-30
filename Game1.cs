using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace summative
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        List<Texture2D> myCats;
        Texture2D binkaT, beanT, saraT, shebT,introCatT;
        Rectangle myCatR,catsR;
        Random r;
        List<int> x,y;
        int gPH, gPW;
        Screen screen;
        MouseState mouseState;
        SpriteFont font;
        Vector2 v;
        enum Screen
        {
            Intro,
            Middle,
            End
        }
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            gPH =(_graphics.PreferredBackBufferHeight);
            gPW =(_graphics.PreferredBackBufferWidth);
            y =new List<int> {gPH};
            x=new List<int> {gPW};            
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            binkaT = Content.Load<Texture2D>("Binka");
            beanT = Content.Load<Texture2D>("Bean");
            saraT = Content.Load<Texture2D>("Sara");
            shebT = Content.Load<Texture2D>("Sheb");
            myCats = new List<Texture2D> { binkaT, beanT, saraT, shebT };
            introCatT = Content.Load<Texture2D>("MyCats");
            myCatR = new Rectangle(0, 0, 50, 50);
            catsR = new Rectangle(0, 0, gPW, gPH);
            // TODO: use this.Content to load your game content here
            font = Content.Load<SpriteFont>("File");
            v=new Vector2(gPW/4, gPH / 2);
            screen = Screen.Intro;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (screen == Screen.Intro)
                if (mouseState.LeftButton == ButtonState.Pressed)
                    screen = Screen.Middle;
                else;
            else if (screen == Screen.Middle)
                if (mouseState.RightButton == ButtonState.Pressed)
                    screen = Screen.End;
                else;
            else if (screen == Screen.End)
                if (mouseState.LeftButton == ButtonState.Pressed)
                    this.Exit();
                else;
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(introCatT, catsR, Color.White);
                _spriteBatch.DrawString(font, "Left Click Right Left Click",v,Color.White);
            }
            else if (screen == Screen.Middle)
            {
                foreach (var cat in myCats)
                    _spriteBatch.Draw(cat, myCatR, Color.White);
            }
            else if (screen == Screen.End)
            {
                _spriteBatch.Draw(introCatT, catsR, Color.White);
                _spriteBatch.DrawString(font, "The End", v, Color.Blue);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}