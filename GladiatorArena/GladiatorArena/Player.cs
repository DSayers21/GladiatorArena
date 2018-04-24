using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GladiatorArena
{
    public class Player
    {
        public enum DIR{ Up = 0, UpRight = 1, Right = 2, DownRight = 3, Down = 4, DownLeft= 5, Left=6, TopLeft = 7}
        public int m_id { get; private set; }
        public bool m_isHuman { get; private set; }
        public float m_lifePoints { get; private set; }
        public DIR m_dirFacing { get; private set; }
        public int[] m_weaponId { get; private set; }
        public int m_specialMoveId { get; private set; }

        public int m_pos { get; private set; }
        public int m_posNew { get; private set; }


        public float m_speed = 12.0f;
        public float m_runningSpeed = 16.0f;
        private bool m_isRunning = false;
        private bool m_moving = false;
        private float m_timeSoFar = 0.0f;



        public Entity spr_player;
        private MusicMan m_musicMan;

        /// <summary>
        /// Default constructor
        /// </summary>
        public Player(Texture2D playerSprite, TileMap tiles, Vector2 startPosition, float speed, float runningSpeed)
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
            m_runningSpeed = runningSpeed;

            //Setup player sprite
            spr_player = new Entity(playerSprite, new Vector2(startPosition.X * 64, startPosition.Y * 64));

            m_musicMan = new MusicMan();
        }

        public void Input(TileMap tiles)
        {
            if (!m_moving)
            {
                //Get Keyboard state
                KeyboardState state = Keyboard.GetState();

                if (state.IsKeyDown(Keys.LeftShift))
                {
                    if (m_isRunning == false)
                        m_isRunning = true;
                    else
                        m_isRunning = false;
                    //Console.WriteLine("Running = ", m_isRunning);
                }


                //Get 2D position of player
                Vector2 position = tiles.ConvertTo2D(m_pos);

                bool pressed = false;

                if (state.IsKeyDown(Keys.Left))
                {
                    position = Move(DIR.Left, position);
                    pressed = true;
                }
                if (state.IsKeyDown(Keys.Right))
                {
                    position = Move(DIR.Right, position);
                    pressed = true;
                }
                if (state.IsKeyDown(Keys.Up))
                {
                    position = Move(DIR.Up, position);
                    pressed = true;
                }
                if (state.IsKeyDown(Keys.Down))
                {
                    position = Move(DIR.Down, position);
                    pressed = true;
                }

                if (pressed == true)
                {
                    //Check if movement it allowed before moving
                    if (tiles.CheckMap(position))
                    {
                        m_timeSoFar = 0.0f;
                        m_moving = true;
                        m_posNew = tiles.ConvertTo1D(Convert.ToInt32(position.X), Convert.ToInt32(position.Y));
                    }
                    //else
                        //Console.WriteLine(tiles.CheckMap(position));
                }

                //Console.WriteLine("Player is on tileNo : " + m_pos);
            }
        }

        public void Update(TileMap tiles, GameTime gameTime)
        {

            float spd = 0.0f;
            if (m_isRunning == false)
                spd = m_speed;
            else
                spd = m_runningSpeed;

            if (m_timeSoFar >= spd)
            {
                Vector2 targetPosition = tiles.ConvertTo2D(m_posNew);
                m_pos = tiles.ConvertTo1D(Convert.ToInt32(targetPosition.X), Convert.ToInt32(targetPosition.Y));
                spr_player.SetPosition(tiles.ConvertTo2D(m_pos) * 64);
                m_timeSoFar = 0;
                m_moving = false;
            }
            else
            {
                //Get 2D position of player
                Vector2 position = tiles.ConvertTo2D(m_pos)*64;
                Vector2 targetPosition = tiles.ConvertTo2D(m_posNew) * 64;

                m_timeSoFar += 1;

                float t = m_timeSoFar / spd;

                //Create new position (interpolation)
                Vector2 newPos = new Vector2();
                newPos.X = (targetPosition.X - position.X) * t + position.X;
                newPos.Y = (targetPosition.Y - position.Y) * t + position.Y;

                //Console.WriteLine(newPos);
                spr_player.SetPosition(newPos);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spr_player.Draw(spriteBatch);
        }

        private Vector2 Move(DIR direction, Vector2 position)
        {
            //testing sounds
            m_musicMan.playMoveSound("sand01");

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

        public void Attack(float damage)
        {
            m_lifePoints -= damage;
            Console.WriteLine(m_lifePoints);
        }
    }
}