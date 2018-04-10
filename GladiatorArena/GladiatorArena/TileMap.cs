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
            m_mapSize = new Vector2(11, 11);
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
    }
}