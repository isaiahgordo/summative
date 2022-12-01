using Microsoft.VisualBasic;
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
        Rectangle catsR,binkaR,beanR,saraR,shebR,my;
        List<Rectangle> myCatR;
        Random r;
        List<int> x,y;
        int gPH, gPW;
        Screen screen;
        MouseState mouseState;
        SpriteFont font;
        Vector2 v,catMovement;
        float seconds, startTime;        
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
            my = new Rectangle(0, 0, 0, 0);
            // TODO: Add your initialization logic here
            gPH =(_graphics.PreferredBackBufferHeight);
            gPW =(_graphics.PreferredBackBufferWidth);
            r=new Random();
            y =new List<int> {gPH};
            x=new List<int> {gPW};
            screen = Screen.Intro;
            catMovement=new Vector2(r.Next(1,3), r.Next(1, 3));
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
            binkaR=beanR=saraR=shebR= new Rectangle(0, 0, 100, 100);
            myCatR=new List<Rectangle> { binkaR,beanR,saraR,shebR };    
            catsR = new Rectangle(0, 0, gPW, gPH);
            // TODO: use this.Content to load your game content here
            font = Content.Load<SpriteFont>("File");
            v=new Vector2(gPW/4, gPH / 2);            
        }
        protected override void Update(GameTime gameTime)
        {
            seconds = (float)gameTime.TotalGameTime.TotalSeconds-startTime;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            mouseState = Mouse.GetState();
            if (screen == Screen.Intro)
                if (mouseState.LeftButton == ButtonState.Pressed)
                    screen = Screen.Middle;
                else;
            else if (screen == Screen.Middle)
                if (mouseState.RightButton == ButtonState.Pressed)
                    screen = Screen.End;
                else
                {                                       
                    foreach (var cat in myCatR)
                    {
                        my = cat;
                        my.X += (int)catMovement.X;
                        if (my.X < 0 || my.X > gPW)
                            catMovement.X *= -1;
                        my.Y += (int)catMovement.Y;
                        if (my.Y < 0 || my.Y > gPH)
                            catMovement.Y *= -1;
                    }
                    if(seconds>10)
                        startTime = (float)gameTime.TotalGameTime.TotalSeconds;                    
                }
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
            if (screen == Screen.Middle)
            {                                            
                foreach (var cat in myCats)
                    foreach(var my in myCatR)
                            _spriteBatch.Draw(cat, my, Color.White);                                                                                                                       
            }
            if (screen == Screen.End)
            {
                _spriteBatch.Draw(introCatT, catsR, Color.White);
                _spriteBatch.DrawString(font, "The End by Isaiah", v, Color.Blue);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}