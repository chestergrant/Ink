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

    public class CollisionBox : Microsoft.Xna.Framework.Game
    {
        private BoundingBox top;
        private BoundingBox bottom;
        private BoundingBox left;
        private BoundingBox right;
        private BoundingBox entireObject;
        private Vector2 start;
        private Vector2 finish;

        public CollisionBox(Vector2 s, Vector2 e)
        {
            setBoundaries(s, e);
            start = s;
            finish = e;
        }

        public void setBoundaries(Vector2 s, Vector2 e)
        {
            left = new BoundingBox(new Vector3(s, 0), new Vector3(s.X+1, e.Y, 0));
            right = new BoundingBox(new Vector3(e, 0), new Vector3(e.X-1, s.Y, 0));
            top =  new BoundingBox(new Vector3(s, 0), new Vector3(e.X, s.Y-1, 0));
            bottom = new BoundingBox(new Vector3(e, 0), new Vector3(s.X, e.Y+1, 0));
            entireObject = new BoundingBox(new Vector3(s, 0), new Vector3(e, 0));
        }
        public void setStart(Vector2 s)
        {
            start = s;
            setBoundaries(start, finish);
        }
        public void setEnd(Vector2 e)
        {
            finish = e;
            setBoundaries(start, finish);
        }
        public Vector2 getStart()
        {
            return start;
        }

        public Vector2 getEnd()
        {
            return finish;
        }
        public BoundingBox getEntireObject()
        {
            return entireObject;
        }
        public void moveForward(float points)
        {
            start.X += points;
            finish.X += points;
            setBoundaries(start, finish);
        }
        public void moveBackward(float points)
        {
            start.X -= points;
            finish.X -= points;
            setBoundaries(start, finish);
        }
        public void moveUpward(float points)
        {
            start.Y -= points;
            finish.Y -= points;
            setBoundaries(start, finish);
        }
        public void moveDownward(float points)
        {
            start.Y += points;
            finish.Y += points;
            setBoundaries(start, finish);
        }
        public Boolean IntersectLeft(CollisionBox box)
        {
            BoundingBox ex = box.getEntireObject();
            return ex.Intersects(left);
        }
        public Boolean IntersectRight(CollisionBox box)
        {
            BoundingBox ex = box.getEntireObject();
            return ex.Intersects(right);
        }
        public Boolean IntersectTop(CollisionBox box)
        {
            BoundingBox ex = box.getEntireObject();
            Boolean ans = ex.Intersects(top);
            if (ans)
            {
                ans = ans;
            }
            return ans;
        }
        public Boolean IntersectBottom(CollisionBox box)
        {
            BoundingBox ex = box.getEntireObject();
            return ex.Intersects(bottom);
        }
        public int Intersect(CollisionBox box)
        {
            int side = -1;
            BoundingBox ex = box.getEntireObject();
            if (ex.Intersects(left))
            {
                side = 1;
            }else if(ex.Intersects(top)){
                side = 2;
            }
            else if (ex.Intersects(right))
            {
                side = 3;
            }
            else if (ex.Intersects(bottom))
            {
                side = 4;
            }
             

            return side;
        }

    }
}
