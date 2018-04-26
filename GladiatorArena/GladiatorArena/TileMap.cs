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
        //Enum
        public enum tileType { grass = 0, sand = 1, tree = 2, water = 3, dirt = 4 }

        //member variables
        public Vector2 m_mapSize;
        public Vector2 m_tileDims;
        private List<Tile> m_tileList = new List<Tile>();

        //default constructor
        public TileMap()
        {
            m_mapSize = new Vector2(11, 17);
            m_tileDims = new Vector2(64, 64);
        }

        //Overloaded Constructor
        public TileMap(int[,] levelData, Vector2 mapSize, Vector2 tileDims, Dictionary<tileType, Texture2D> tileTextures)
        {
            m_mapSize = mapSize;
            m_tileDims = tileDims;
            for (int i = 0; i < levelData.GetLength(1); i++)
            {
                for (int j = 0; j < levelData.GetLength(0); j++)
                {
                    Tile test = new Tile();
                    //Console.WriteLine((tileType)levelData[j, i]);
                    test = new Tile(new Entity(tileTextures[(tileType)levelData[j, i]], new Vector2(i * tileDims.X, j * tileDims.Y)), levelData[j, i]);
                    
                    m_tileList.Add(test);
                }
            }
        }

        public void RenderTileMap(SpriteBatch spriteBatch)
        {
            for (int index = 0; index < m_tileList.Count; index++)
                m_tileList[index].RenderTile(spriteBatch);
        }

        public int ConvertTo1D(int x, int y)
        {
            return (x * Convert.ToInt32(m_mapSize.X)) + y;
        }

        public Vector2 ConvertTo2D(int pos)
        {
            int x = pos / Convert.ToInt32(m_mapSize.X);
            int y = pos % Convert.ToInt32(m_mapSize.X);
            return new Vector2(x, y);
        }

        public bool CheckMap(Vector2 pos)
        {
            int position = ConvertTo1D(Convert.ToInt32(pos.X), Convert.ToInt32(pos.Y));
            if ((tileType)m_tileList[position].m_tileID == tileType.tree)
                return false;
            return true;
        }

        public int CheckTileAt(int pos)
        {
            return m_tileList[pos].m_tileID;
        }
    }
}