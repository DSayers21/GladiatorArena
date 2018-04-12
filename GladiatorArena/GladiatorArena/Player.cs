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
    class Player
    {
        public enum DIR{ Up = 0, UpRight = 1, Right = 2, DownRight = 3, Down = 4, DownLeft= 5, Left=6, TopLeft = 7}
        public int m_id { get; private set; }
        public bool m_isHuman { get; private set; }
        public float m_lifePoints { get; private set; }
        public DIR m_dirFacing { get; private set; }
        public int[] m_weaponId { get; private set; }
        public int m_specialMoveId { get; private set; }

        public int m_pos { get; private set; }
        public Entity spr_player;

        /// <summary>
        /// Default constructor
        /// </summary>
        public Player(Texture2D playerSprite)
        {
            m_id = -1;
            m_isHuman = true;
            m_lifePoints = 100.0f;
            //refers to surrounding cells starting from topLeft in clockwise direction
            m_dirFacing = DIR.Left;
            m_weaponId = new int[12]; 
            foreach (int weapon in m_weaponId) { m_weaponId[weapon] = 0; }
            m_specialMoveId = -1;
            m_pos = 18;
            spr_player = new Entity(playerSprite, new Vector2(5 * 64, 8 * 64));
        }

        public void Update(TileMap tiles)
        {
            //Update sprite position

            Vector2 position = tiles.ConvertTo2D(m_pos);

            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Left))
                position = Move(DIR.Left, position);
            if (state.IsKeyDown(Keys.Right))
                position = Move(DIR.Right, position);
            if (state.IsKeyDown(Keys.Up))
                position = Move(DIR.Up, position);
            if (state.IsKeyDown(Keys.Down))
                position = Move(DIR.Down, position);


            if (tiles.CheckMap(position))
            {
                m_pos = tiles.ConvertTo1D(Convert.ToInt32(position.X), Convert.ToInt32(position.Y));
                spr_player.SetPosition(tiles.ConvertTo2D(m_pos) * 64);
            }
            else
                Console.WriteLine(tiles.CheckMap(position));

            Console.WriteLine("Player is on tileNo : "+m_pos);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spr_player.Draw(spriteBatch);
        }



        private Vector2 Move(DIR direction, Vector2 position)
        {
            if (direction == DIR.Down)
            {
                if (position.Y < 10)
                    position.Y += 1;
            }
            if (direction == DIR.Up)
            {
                if (position.Y > 0)
                    position.Y -= 1;
            }
            if (direction == DIR.Left)
            {
                if (position.X > 0)
                    position.X -= 1;
            }
            if (direction == DIR.Right)
            {
                if (position.X < 16)
                    position.X += 1;
            }
            return position;
        }
    }
}
