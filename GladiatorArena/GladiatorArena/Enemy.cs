using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GladiatorArena
{
    public class Enemy
    {
        public enum DIR { Up = 0, UpRight = 1, Right = 2, DownRight = 3, Down = 4, DownLeft = 5, Left = 6, TopLeft = 7 }
        public int m_id { get; private set; }
        public bool m_isHuman { get; private set; }
        public float m_lifePoints { get; private set; }
        public DIR m_dirFacing { get; private set; }
        public int[] m_weaponId { get; private set; }
        public int m_specialMoveId { get; private set; }

        public int m_pos { get; private set; }
        public int m_posNew { get; private set; }


        public float m_speed = 24.0f;
        private bool m_moving = false;
        private float m_timeSoFar = 0.0f;

        public Entity spr_enemy;

        public int m_rangeAwayFromPlr;
        public bool m_atRange = false;

        public Enemy(Texture2D enemySprite, TileMap tiles, Vector2 startPosition, float speed, int rangeAwayFromPlayer)
        {
            //General player stats
            m_id = -1;
            m_isHuman = true;
            m_lifePoints = 100.0f;

            //refers to surrounding cells starting from topLeft in clockwise direction
            m_dirFacing = DIR.Left;

            //Setup player weapons
            m_weaponId = new int[12];
            foreach (int weapon in m_weaponId) { m_weaponId[weapon] = 0; }
            m_specialMoveId = -1;

            //Setup Player position
            int pos = tiles.ConvertTo1D(Convert.ToInt32(startPosition.X), Convert.ToInt32(startPosition.Y));
            m_pos = pos;
            m_posNew = pos;


            //Player speeds
            m_speed = speed;

            m_rangeAwayFromPlr = rangeAwayFromPlayer;

            //Setup player sprite
            spr_enemy = new Entity(enemySprite, new Vector2(startPosition.X * 64, startPosition.Y * 64));
        }

        public void Input(TileMap tiles, Vector2 plrPos)
        {
            if (!m_moving)
            {
                Vector2 position = tiles.ConvertTo2D(m_pos);


                Vector2 PosLeft = Move(DIR.Left, position);
                Vector2 PosRight = Move(DIR.Right, position);
                Vector2 PosTop = Move(DIR.Up, position);
                Vector2 PosBottom = Move(DIR.Down, position);

                if (PosLeft == position)
                    PosLeft = new Vector2(9999, 9999);
                if (PosRight == position)
                    PosRight = new Vector2(9999, 9999);
                if (PosTop == position)
                    PosTop = new Vector2(9999, 9999);
                if (PosBottom == position)
                    PosBottom = new Vector2(9999, 9999);

                double[] Directions = new double[4] { CalcDistance(plrPos, PosLeft) , CalcDistance(plrPos, PosRight), CalcDistance(plrPos, PosTop), CalcDistance(plrPos, PosBottom) };




                int LowestI = 0;
                for(int i = 0; i < 4; i++)
                {
                    if (Directions[LowestI] > Directions[i])
                        LowestI = i;
                }
                Vector2 MoveDirection = position;
                if (CalcDistance(position, plrPos) > m_rangeAwayFromPlr)
                {
                    m_atRange = false;

                    switch (LowestI)
                    {
                        case 0:
                            MoveDirection = PosLeft;
                            break;
                        case 1:
                            MoveDirection = PosRight;
                            break;
                        case 2:
                            MoveDirection = PosTop;
                            break;
                        case 3:
                            MoveDirection = PosBottom;
                            break;
                    }



                    //Check if movement it allowed before moving
                    if (tiles.CheckMap(MoveDirection))
                    {
                        m_timeSoFar = 0.0f;
                        m_moving = true;
                        m_posNew = tiles.ConvertTo1D(Convert.ToInt32(MoveDirection.X), Convert.ToInt32(MoveDirection.Y));
                        Console.WriteLine("Enemy: " + LowestI);
                    }
                    else
                        Console.WriteLine("Enemy: " + tiles.CheckMap(MoveDirection));
                }
                else
                {
                    m_atRange = true;
                }
            }
        }

        private Vector2 Move(DIR direction, Vector2 position)
        {
            if (direction == DIR.Down)
                if (position.Y < 10)
                    position.Y += 1;
            if (direction == DIR.Up)
                if (position.Y > 0)
                    position.Y -= 1;
            if (direction == DIR.Left)
                if (position.X > 0)
                    position.X -= 1;
            if (direction == DIR.Right)
                if (position.X < 16)
                    position.X += 1;
            return position;
        }

        private double CalcDistance(Vector2 posA, Vector2 posB)
        {
            float X = posB.X - posA.X;
            float Y = posB.Y - posA.Y;
            X = X * X;
            Y = Y * Y;
            double Distance = Math.Sqrt(Convert.ToDouble(X) + Convert.ToDouble(Y));

            return Math.Abs(Distance);
        }

        public void Update(TileMap tiles, GameTime gameTime, Player plr)
        { 
            float spd = m_speed;

            if (m_timeSoFar >= spd)
            {
                Vector2 targetPosition = tiles.ConvertTo2D(m_posNew);
                m_pos = tiles.ConvertTo1D(Convert.ToInt32(targetPosition.X), Convert.ToInt32(targetPosition.Y));
                spr_enemy.SetPosition(tiles.ConvertTo2D(m_pos) * 64);
                m_timeSoFar = 0;
                m_moving = false;
            }
            else
            {
                //Get 2D position of player
                Vector2 position = tiles.ConvertTo2D(m_pos) * 64;
                Vector2 targetPosition = tiles.ConvertTo2D(m_posNew) * 64;

                m_timeSoFar += 1;

                float t = m_timeSoFar / spd;

                //Create new position (interpolation)
                Vector2 newPos = new Vector2();
                newPos.X = (targetPosition.X - position.X) * t + position.X;
                newPos.Y = (targetPosition.Y - position.Y) * t + position.Y;

                //Console.WriteLine(newPos);
                spr_enemy.SetPosition(newPos);
            }

            //Attacks
            if(m_atRange) //If at range then attack
            {
                //Check weapon for damage
                plr.Attack(0.1f);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spr_enemy.Draw(spriteBatch);
        }

        public void Attack(float damage)
        {
            //Checks for armour and stuff
            m_lifePoints -= damage;
        }
    }
}
