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
            float oneRad = MathHelper.Max(one.GetWidth(), one.GetHeight())/2;
            float twoRad = MathHelper.Max(two.GetWidth(), two.GetHeight())/2;

            Vector2 oneCPos;
            oneCPos.X = one.GetPosition().X - one.GetWidth()/2;
            oneCPos.Y = one.GetPosition().Y - one.GetHeight()/2;

            Vector2 twoCPos;
            twoCPos.X = one.GetPosition().X - one.GetWidth()/2;
            twoCPos.Y = one.GetPosition().Y - one.GetHeight()/2;

            float distance = (float)Math.Pow((oneCPos.X - twoCPos.X), 2) + (float)Math.Pow((oneCPos.Y - twoCPos.Y), 2);

            if (distance > oneRad * twoRad)
                return true;

            return false;
        }
    }
}
