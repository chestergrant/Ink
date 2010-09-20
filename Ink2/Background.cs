using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Ink2
{
    public class Background : Microsoft.Xna.Framework.Game
    {
        protected Texture2D bg1;
        protected Texture2D bg2;
        protected Texture2D bg3;
        protected Vector2 bgPos1;
        protected Vector2 bgPos2;
        protected Vector2 bgPos3;
        protected Vector2 speed;
        public Background(Texture2D background1, Vector2 aSpeed)
        {
            bg1 = background1;
            bg2 = background1;
            bg3 = background1;
            bgPos1.Y = 0;
            bgPos2.Y = 0;
            bgPos3.Y = 0;
            bgPos1.X = 0;
            bgPos2.X = bgPos1.X + bg1.Width;
            bgPos3.X = bgPos2.X + bg2.Width;
            speed = aSpeed;
        }
        public void moveForward()
        {
            if (bgPos1.X < -bg1.Width)
            {
                bgPos1.X = bgPos3.X + bg3.Width;
            }
            if (bgPos2.X < -bg2.Width)
            {
                bgPos2.X = bgPos1.X + bg1.Width;
            }
            if (bgPos3.X < -bg3.Width)
            {
                bgPos3.X = bgPos2.X + bg2.Width;
            }

            bgPos1.X -= speed.X;
            bgPos2.X -= speed.X;
            bgPos3.X -= speed.X;
        }
       
        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bg1, bgPos1, new Rectangle(0, 0, bg1.Width, bg1.Height), Color.White);
            spriteBatch.Draw(bg2, bgPos2, new Rectangle(0, 0, bg2.Width, bg2.Height), Color.White);
            spriteBatch.Draw(bg3, bgPos3, new Rectangle(0, 0, bg3.Width, bg3.Height), Color.White);
        }
    }
}
