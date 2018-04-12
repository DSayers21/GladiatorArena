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
    public class TileMap
    {
        enum m_tileType { grass, sand, dirt, tree, wall }

        //member variables
        private Vector2 m_mapSize;
        private List<Tile> m_tileList;

        //default constructor
        public TileMap()
        {
            m_mapSize = new Vector2(17, 11);
        }

        //Overloaded constructor
        public TileMap(List<Tile> tiles, Vector2 mapSize)
        {
            m_mapSize = new Vector2(mapSize.X, mapSize.Y);
            m_tileList = tiles;
        }

        public void RenderTileMap(SpriteBatch spriteBatch)
        {
            for (int index = 0; index < m_tileList.Count; index++)
            {
                m_tileList[index].RenderTile(spriteBatch);
            }
        }

        public int ConvertTo1D(int x, int y)
        {
            int pos = x * Convert.ToInt32(m_mapSize.X )+ y;

            return pos;
        }

        public Vector2 ConvertTo2D(int pos)
        {
            int y = pos % Convert.ToInt32(m_mapSize.X);
            int x = pos / Convert.ToInt32(m_mapSize.X);
            return new Vector2(x, y);
        }

        public bool CheckMap(Vector2 pos)
        {
            int position = ConvertTo1D(Convert.ToInt32(pos.Y), Convert.ToInt32(pos.X));
            if (m_tileList[position].m_tileID == 2)
                return false;

            return true;
        }
    }
}