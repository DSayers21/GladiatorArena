using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GladiatorArena
{
    class Collider
    {
        public static bool CheckColliding(Entity one, Entity two)
        {
            //Get Radius
            float oneRad = one.GetRadius();
            float twoRad = two.GetRadius();

            //Get Centres
            Vector2 oneCPos = one.GetCentre();
            Vector2 twoCPos = two.GetCentre();

            //Calc Distance
            float distance = (float)Math.Sqrt((float)Math.Pow((oneCPos.X - twoCPos.X), 2) + (float)Math.Pow((oneCPos.Y - twoCPos.Y), 2));

            //Check if collison has happened
            if (distance < (oneRad + twoRad))
                return true;

            return false;
        }
    }
}
