using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GladiatorArena
{
    public class Tile
    {
        //Members
        public int m_tileID;
        private int m_type;
        private Entity m_texEntity;

        //0 = above tile, 1 = on tile, 2 = below tile
        private int[] m_occupants;

        //default constructor
        public Tile(Entity texEntity, int ID)
        {
            m_tileID = ID;
            m_type = -1;
            //-1 is means no entity present, else it stores the id of the entity present
            m_occupants = new int[] { -1, -1, -1 };
            m_texEntity = texEntity;
        }

        //overloaded constructor
        public Tile(int tileID, int type, int[] occupants)
        {
            m_tileID = tileID;
            m_type = type;
            m_occupants = occupants;
        }

        public Tile()
        {
        }

        public void RenderTile(SpriteBatch spriteBatch)
        {
            m_texEntity.Draw(spriteBatch);
        }
    }
}
