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
    public class Element: Microsoft.Xna.Framework.Game
    {

        protected Texture2D sprite;
        protected Texture2D collisionSprite;
        protected Vector2 position;
        protected Vector2 speed;
        protected int frameCounter;
        protected Boolean drawable;
        protected int frameSize;
        protected int hasBeenHit;
        protected SpriteEffects se = SpriteEffects.None;
        protected CollisionBox ourBox;
        protected Boolean endSequence;
        protected Boolean hasEndingSequence;
        protected int collisionNumFrames;
        protected Boolean activated;
        public Element(Texture2D aSprite, Vector2 aPosition, Vector2 aSpeed, int numFrames,Texture2D cSprite, int cNumFrames)
        {
            
            activated = false;
            collisionSprite = cSprite;
            collisionNumFrames = cNumFrames;
            hasBeenHit = -1;
            endSequence = false;
            sprite = aSprite;
            position = aPosition;
            speed = aSpeed;
            frameCounter = 0;
            frameSize = sprite.Width / numFrames;
            drawable = true;
            hasEndingSequence = true;
            ourBox = new CollisionBox(aPosition, new Vector2(aPosition.X + aSprite.Width/numFrames, aPosition.Y + aSprite.Height));
        }
        public Boolean getActivated()
        {
            return activated;
        }
        public void setActivated(Boolean act)
        {
            activated = act;
        }
        public Element(Texture2D aSprite, Vector2 aPosition,  Vector2 aSpeed, int numFrames){
            
            activated = false;
            hasBeenHit = -1;
            endSequence = false;
            sprite = aSprite;
            position = aPosition;
            speed = aSpeed;
            frameCounter = 0;
            frameSize = sprite.Width / numFrames;
            drawable = true;
            hasEndingSequence = false;
            ourBox = new CollisionBox(aPosition, new Vector2(aPosition.X + aSprite.Width/numFrames, aPosition.Y + aSprite.Height));
        }

        public CollisionBox getCollisionBox()
        {
            return ourBox;
        }
        private void doEnd()
        {
        }
        public int isHit()
        {
            return hasBeenHit;
        }
        public int checkCollision(Element e)
        {
            int ans = ourBox.Intersect(e.getCollisionBox());
            hasBeenHit = ans;
            return ans;
        }
        public void setSpeed(Vector2 aSpeed)
        {
            speed = aSpeed;
        }
        public Vector2 getSpeed()            
        {
            return speed;
        }
        public void moveForward()
        {
           if(!endSequence){
            position.X += speed.X;
            ourBox.moveForward(speed.X);
            se = SpriteEffects.None;
           }
           
        }

        public void advanceFrame()
        {
            if (!endSequence)
            {
                frameCounter += frameSize;
                if (frameCounter >= sprite.Width) frameCounter = 0;
            }
            else
            {
                doEnd();
            }
        }
        public void setEnd()
        {
            endSequence = true;
            frameCounter = 0;
            Texture2D temp = sprite;
            sprite = collisionSprite;
            collisionSprite = temp;
            frameSize = sprite.Width / collisionNumFrames;
        }
        public void moveBackward()
        {
            if(!endSequence){
                position.X -= speed.X;
                ourBox.moveBackward(speed.X);
                se = SpriteEffects.FlipHorizontally;
            }
            
        }
        public void setDrawable(Boolean aBool)
        {
            drawable = aBool;
        }
        public Boolean getDrawable()
        {
            return drawable;
        }
        public void setSprite(Texture2D aSprite)
        {
            sprite = aSprite;
        }
        public Texture2D getSprite()
        {
            return sprite;
        }
        public Vector2 getPosition()
        {
            return position;
        }
        public void setPosition(Vector2 pos)
        {
            position = pos;
        }
        public void draw(SpriteBatch spriteBatch)
        {
            if (drawable)
            {
               
                    spriteBatch.Draw(sprite, position, new Rectangle(frameCounter, 0, frameSize, sprite.Height), Color.White, 0, Vector2.Zero, 1, se, 0);
                
            }
            
        }




    }
}
