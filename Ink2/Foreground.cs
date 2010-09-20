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
    public class Foreground : Element
    {
        int pointValue = 0;
        int type = 0;
        Boolean immutable;
        public Foreground(Texture2D aSprite, Vector2 aPosition, Vector2 aSpeed, int numFrames, int value, int aType, Texture2D cSprite, int cNumFrames)
            : base(aSprite, aPosition, aSpeed, numFrames,cSprite,cNumFrames)
        {
            setActivated(true);
            pointValue = value;
            type = aType;
            immutable = true;

            if ((aType == 44) || (aType == 83) || (aType == 128) || (type == 104))
            {
                immutable = false;
            }
           
        }
        public Foreground(Texture2D aSprite, Vector2 aPosition, Vector2 aSpeed, int numFrames, int value, int aType): base(aSprite, aPosition, aSpeed, numFrames)
        {
            setActivated(true);
            pointValue = value;
            type = aType;
            immutable = true;

            if((aType ==44) || (aType ==83) ||(aType == 128)||(type == 104)){
                immutable = false;
            }
            if (aType == 8)
            {
                ourBox.setStart(new Vector2(aPosition.X, aPosition.Y + 7 * aSprite.Height / 8));
            }
        }
        private void doEnd()
        {
            if (frameCounter < collisionNumFrames)
            {
                frameCounter += frameSize;
            }
            else
            {
                if (type == 104)
                {
                    setDrawable(false);
                }

            }
        }
        public int checkCollision(Element e)
        {
            int ans = base.checkCollision(e);
            if(ans != -1){
                if ((type == 44) || (type == 128))
                {
                    setDrawable(false);
                }
                if ((type == 83)||(type==104))
                {
                    if(ans == 4){
                    
                        if(type == 83){
                            type = 84;
                            immutable = true;
                        }
                      setEnd();
                    }

                }
            }
            return ans;
        }
        public Boolean getImmutable(){
            return immutable;
        }
        public int getType()
        {
            return type;
        }
        public void setType(int aType)
        {
            type = aType;
        }
        public void moveForward()
        {
        }
        public int getValue()
        {
            return pointValue;
        }
        public void setValue(int value)
        {
            pointValue = value;
        }
        public void moveBackward()
        {
            if (!endSequence)
            {
                position.X -= speed.X;
                ourBox.moveBackward(speed.X);
                
            }

        }
        
    }
}
