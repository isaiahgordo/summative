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
        Rectangle catsR,binkaR,beanR,saraR,shebR;
        List<Rectangle> myCatR;
        Random r;         
        int gPH, gPW, x, y;
        Screen screen;
        MouseState mouseState;
        SpriteFont font;
        Vector2 v;
        List<Vector2> catMovement;
        float seconds, startTime;
        string nulle;
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
            r=new Random();
            y = r.Next(gPH);
            x=r.Next(gPW);
            screen = Screen.Intro;
            catMovement=new List<Vector2> { new Vector2(r.Next(1,4), r.Next(1, 4)), new Vector2(r.Next(1, 4), r.Next(1, 4)) , new Vector2(r.Next(1, 4), r.Next(1, 4)) , new Vector2(r.Next(1, 4), r.Next(1, 4)) };
            base.Initialize();
        }
        protected override void LoadContent()
        {
            nulle = "";
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            binkaT = Content.Load<Texture2D>("Binka");
            beanT = Content.Load<Texture2D>("Bean");
            saraT = Content.Load<Texture2D>("Sara");
            shebT = Content.Load<Texture2D>("Sheb");            
            myCats = new List<Texture2D> { binkaT, beanT, saraT, shebT };
            introCatT = Content.Load<Texture2D>("MyCats");
            binkaR = new Rectangle(r.Next(gPW), r.Next(gPH), 100, 100);
            beanR= new Rectangle(r.Next(gPW), r.Next(gPH), 100, 100);
            saraR= new Rectangle(r.Next(gPW), r.Next(gPH), 100, 100);
            shebR= new Rectangle(r.Next(gPW), r.Next(gPH), 100, 100);
            myCatR =new List<Rectangle> { binkaR,beanR,saraR,shebR };             
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
            var my = binkaR;
            mouseState = Mouse.GetState();
            if (catsR.Contains(mouseState.Position))
            {
                if (screen == Screen.Intro)
                    if (mouseState.LeftButton == ButtonState.Pressed)
                        screen = Screen.Middle;
                    else nulle = "";
                else if (screen == Screen.Middle)
                    if (mouseState.RightButton == ButtonState.Pressed)
                        screen = Screen.End;
                    else
                    {
                        Rectangle temp;
                        Vector2 tempV;
                        for(int i=0;i<myCatR.Count;i++)
                        {
                            tempV = catMovement[i];
                            temp = myCatR[i];
                            temp.X += (int)tempV.X;
                            if (myCatR[i].Left < 0 || myCatR[i].Right > gPW)
                                tempV.X *= -1;                            
                            catMovement[i] = tempV;
                            myCatR[i] = temp;
                            temp.Y += (int)tempV.Y;
                            if(myCatR[i].Top < 0 || myCatR[i].Bottom > gPH)
                                tempV.Y *= -1;                            
                            catMovement[i] = tempV;
                            myCatR[i] = temp;
                        }
                    }
                else if (screen == Screen.End)
                    if (mouseState.LeftButton == ButtonState.Pressed)
                        this.Exit();
                    else nulle = "";
            }        
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
                _spriteBatch.DrawString(font, "Left Click Right Left Click"+nulle,v,Color.White);
            }
            if (screen == Screen.Middle)
            {             
                for(int i=0,t=0; i<myCats.Count&&t<myCatR.Count; i++,t++)                    
                    _spriteBatch.Draw(myCats[i], myCatR[t], Color.White);                                                                                                                                                                                     
            }
            if (screen == Screen.End)
            {
                _spriteBatch.Draw(introCatT, catsR, Color.White);
                _spriteBatch.DrawString(font, "The End by Isaiah"+nulle, v, Color.Blue);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}