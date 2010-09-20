using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Ink2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //Global Variable Declarations
        Texture2D sprite; //This holds our sprite image
        int frameCounter = 0; //This value will hold the x coordinate of the current frame while animating 
        Vector2 position = Vector2.Zero; //The X,Y position of our main sprite
        Vector2 speed = Vector2.Zero; // X,Y values to move the sprite around
        //SpriteEffects se = SpriteEffects.FlipHorizontally; //This value is used in the spritebatch.draw call and will flip the sprite
        double frameDelay = 0.10; //The amount of time in miliseconds to delay updating the next frame
        //Level level; //xml level content will be serialized into this object
        SpriteFont spriteFont;
        double frameDelay2 = 0.50;
        Background ourBackground;
        Texture2D background1;
        Foreground aItem;
        //Texture2D background2;
        //Texture2D background3;
        //Vector2 bgPos1;
        //Vector2 bgPos2;
        //Vector2 bgPos3;
        //Rectangle viewportRec;
        Ink ourInk;
        Foreground[] ourForeground;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFont = Content.Load<SpriteFont>("Arial");
            sprite = Content.Load<Texture2D>("ink2");
            Vector2 speed = new Vector2(6,12);
            Vector2 position = new Vector2(0,0);
            background1 = Content.Load<Texture2D>("arc");
            graphics.PreferredBackBufferWidth = background1.Width;
            graphics.PreferredBackBufferHeight = background1.Height;
            graphics.ApplyChanges();
            ourInk = new Ink(sprite, position, speed, 6, graphics.GraphicsDevice.Viewport);
            sprite = Content.Load<Texture2D>("44");
            position.X = 100;
            position.Y = 20;
            // Specify file, instructions, and privelegdes
            FileStream file = new FileStream("H:\\myLevel.txt", FileMode.OpenOrCreate, FileAccess.Read);

            // Create a new stream to read from a file
            StreamReader sr = new StreamReader(file);
            
            // Read contents of file into a string
            
            string s = sr.ReadToEnd();
            s = s.Replace('\t', ' ');
            s = s.Replace("\r\n", "");
            
            
            
            string[] sArray = s.Split(' ');
            int rowSize = sArray.Length;
            int count = 0;
            ourForeground = new Foreground[rowSize];
            position = new Vector2(0, 0);
            int x= 0;
            for (int i = 2; i < rowSize; i++ )
            {

                if (count < 150)
                {
                    if(count < 10){
                        if(sArray[i] == "" ){
                            i++;
                        }
                        try
                        {
                            int aType = Int32.Parse(sArray[i]);
                        
                        int numFrames = 1;
                        if(i == 2733){
                            i = 2733;
                        }
                        if(aType ==130){
                            numFrames = 5;
                        }else if(aType == 3){
                            numFrames = 3;
                        }
                        int value = 0;
                        if(aType == 44){
                            value = 25;
                        }
                        if(aType != 0){
                            try
                            {
                                sprite = Content.Load<Texture2D>(sArray[i]);
                                aItem = new Foreground(sprite, position, speed, numFrames, value, aType);
                                ourForeground[x] = aItem;
                                x++;
                            }
                            catch (Exception ex)
                            {
                                i=i;
                            }
                        }
                        position = new Vector2(position.X+32,position.Y);
                        }
                        catch (Exception ex)
                        {
                            i=i;
                        }

                    } 
                }
                else
                {
                    count = 0;
                    position = new Vector2(0,position.Y+32);

                }
                count++;
            }

            // Close StreamReader
            sr.Close();

            // Close file
            file.Close();
            try
            {
                ourBackground = new Background(background1, speed);
            }catch(Exception ex){
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState kb = new KeyboardState();
            kb = Keyboard.GetState();
            Boolean moving = false;
            ourInk.setVerticalMobile(true);
            // Allows the game to exit
            /*if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            */
            // TODO: Add your update logic here
           /* CollisionBox box1 = new CollisionBox(new Vector2(0, 0), new Vector2(46, 66));
            CollisionBox box2 = new CollisionBox(new Vector2(0, 384), new Vector2(32, 416));

            for (int i = 0; i < 300; i++ )
            {

                int ans = box2.Intersect(box1);
                
                if (ans != -1)
                {
                    ans = ans;
                }
                box1.moveDownward(2.0f);
            }*/

            int action = -1;
            if(ourInk.getPosition().Y >400 ){
                action = 1;
            }
            if (kb.IsKeyDown(Keys.Right))
            {
                if (!ourInk.moveForward())
                {
                    action = 2;

                    //move more items onto the screen
                }
                else
                {
                    action = 1;
                }
                moving = true;
            }
            if (kb.IsKeyDown(Keys.Left))
            {
                ourInk.moveBackward();
                action = 3;
                moving = true;
            }
            if (kb.IsKeyDown(Keys.Space))
            {
                ourInk.obtainMomentum();
                ourInk.setVerticalMobile(true);
            }
           
            if (moving == true)
            {
                //check for collision
                

                for (int i = 0; i < ourForeground.Length; i++)
                {
                    if (ourForeground[i] != null)
                    {
                        if (ourForeground[i].getDrawable())
                        {
                            int ans = ourForeground[i].checkCollision(ourInk);
                            if (ans != -1)
                            {
                                Boolean im = ourForeground[i].getImmutable();
                                if (im)
                                {
                                    if (action == 1)
                                    {
                                        ourInk.setPosition(new Vector2(ourInk.getPosition().X - ourInk.getSpeed().X, ourInk.getPosition().Y));
                                    }
                                    else
                                    {
                                        ourInk.setPosition(new Vector2(ourInk.getPosition().X + ourInk.getSpeed().X, ourInk.getPosition().Y));
                                    }
                                    moving = false;
                                }
                                else
                                {
                                    //add score

                                }
                            }
                        }
                    }

                }
                if (moving)
                {
                    if (action == 2)
                    {
                        if (ourBackground != null)
                        {
                            ourBackground.moveForward();
                        }
                        //increase all foreground on screen
                        for (int j = 0; j < ourForeground.Length; j++)
                        {
                            if(ourForeground[j] != null){
                               ourForeground[j].moveBackward();
                            }
                        }
                    }
                }
                
                frameDelay -= gameTime.ElapsedGameTime.TotalSeconds;
                if (frameDelay < 0)
                {
                    //advance to the next frame
                    frameDelay = 0.10;
                    ourInk.advanceFrame();
                   
                }
            }
            frameDelay2 -= gameTime.ElapsedGameTime.TotalSeconds;
            if (frameDelay2 < 0)
            {
                //advance to the next frame
                frameDelay2 = .12;
                for (int i = 0; i < ourForeground.Length; i++)
                {

                    if (ourForeground[i] != null)
                    {
                        ourForeground[i].advanceFrame();
                    }
                }
                Boolean canMoveDown = true;
                CollisionBox c2 = ourInk.getCollisionBox();
                c2.moveDownward(ourInk.getSpeed().Y);
                for (int i = 0; i < ourForeground.Length; i++)
                {
                    if (ourForeground[i] != null)
                    {
                        if (ourForeground[i].getDrawable())
                        {
                            CollisionBox c1 = ourForeground[i].getCollisionBox();
                           
                            /*Vector2 s = c2.getStart();
                            Vector2 e = c2.getEnd();
                            CollisionBox c3 = new CollisionBox(s, e);
                            c3.moveDownward(ourInk.getSpeed().Y);*/
                            Boolean ans1 = c1.IntersectTop(c2);
                            if (ans1 && (ourInk.getVerticalMobile() != false))
                            {

                                ourInk.setVerticalMobile(false);
                                ourInk.setPosition(new Vector2(ourInk.getPosition().X, c1.getStart().Y - ourInk.getSprite().Height));
                                c2.setStart(ourInk.getPosition());
                                c2.setEnd(new Vector2(ourInk.getPosition().X + ourInk.getSprite().Width, ourInk.getPosition().Y + ourInk.getSprite().Height));
                               
                            }
                            if (ourInk.getMomentum() > 0)
                            {
                                c2 = ourInk.getCollisionBox();
                                c2.moveUpward(ourInk.getSpeed().Y);
                                int ans2 = c1.Intersect(c2);
                                if (ans2 == 2)
                                {
                                    ourInk.setMomentum(0);
                                }
                                else
                                {
                                    ourInk.moveUpward();
                                }
                            }

                        }
                    }
                }
                if (ourInk.getVerticalMobile())
                {
                    if (ourInk.getMomentum() <= 0)
                    {
                        ourInk.moveDownward();
                    }
                }
                
                

            }
            
           
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            if (ourBackground != null)
            {
                ourBackground.draw(spriteBatch);
            }
               for (int i = 0; i < ourForeground.Length; i++ )
               {
                  
                   if(ourForeground[i] != null){
                       ourForeground[i].draw(spriteBatch);
                   }
               }
               ourInk.draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
