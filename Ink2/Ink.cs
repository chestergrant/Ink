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
    public class Ink:Element
    {
        private Viewport view;
        private int momentum;
        private int maxMomentum;
        private Boolean verticalMobile;
        public Ink(Texture2D aSprite, Vector2 aPosition, Vector2 aSpeed, int numFrames, Viewport aView ):base(aSprite,aPosition,aSpeed, numFrames)
        {
            verticalMobile = true;
             view = aView;
             setActivated(true);
             momentum = 0;
            maxMomentum = 2;
            
        }
        public void setVerticalMobile(Boolean ans)
        {
            verticalMobile = ans;
        }
        public Boolean getVerticalMobile()
        {
            return verticalMobile;
        }
        public void setMaxMomentum(int max){
            maxMomentum = max;
        }
        public int getMaxMomentum(){
            return maxMomentum;
        }
        public void setMomentum(int value){
            momentum = value;
        }
        public void obtainMomentum()
        {
            if(momentum == 0){
                momentum = maxMomentum;
            }
           
        }
        public int getMomentum()
        {
            return momentum;
        }
        public void doEnd()
        {
        }
        public void moveDownward()
        {
            position.Y += speed.Y;
            //ourBox.moveDownward(speed.Y);
        }
        public void moveUpward()
        {
            position.Y -= speed.Y;
            momentum -= 1;
        }
        public Boolean moveForward(){
             if (position.X + speed.X < view.Width / 3)
                {
                    if (!endSequence)
                    {
                        position.X += speed.X;
                        ourBox.moveForward(speed.X);
                        se = SpriteEffects.None;
                    }
                     
                   return true;
                }
             if (!endSequence)
             {
                 return false;
             }
             else
             {
                 return true;
             }
        }
        
        public Boolean moveBackward(){
             if (position.X - speed.X >=0)
                {
                    if (!endSequence)
                    {
                        position.X -= speed.X;
                        ourBox.moveBackward(speed.X);
                        se = SpriteEffects.FlipHorizontally;
                    }
                 
                    
                    return true;
                }
             if (!endSequence)
             {
                 return false;
             }
             else
             {
                 return true;
             }
        }
    }
}
