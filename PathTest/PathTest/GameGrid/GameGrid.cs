using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathTest
{
    class GameGrid
    {
        public GameGrid(int rows, int cols, int tileWidth, int tileHeight, GraphicsDevice gDevice)
        {
            m_rows = rows;
            m_cols = cols;
            m_tileDims = new Point(tileWidth, tileHeight);

            m_tiles = new Tile[m_cols, m_rows];

            for (int i = 0; i < m_cols; i++)
                for (int j = 0; j < m_rows; j++)
                    m_tiles[i, j] = new PathTest.Tile(i, j, new Vector2(m_tileDims.X, m_tileDims.Y), gDevice);

            for (int i = 0; i < m_cols; i++)
                for (int j = 0; j < m_rows; j++)
                    m_tiles[i, j].AddNeighbours(this);
        }

        public void Update(GraphicsDevice gDevice)
        {
            for (int i = 0; i < m_cols; i++)
                for (int j = 0; j < m_rows; j++)
                    m_tiles[i, j].Update(gDevice);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < m_cols; i++)
                for (int j = 0; j < m_rows; j++)
                   m_tiles[i, j].Draw(spriteBatch, Color.White);
        }

        public void ResetTiles()
        {
            for (int i = 0; i < m_cols; i++)
                for (int j = 0; j < m_rows; j++)
                    m_tiles[i, j].SetPrevTile(null);
        }

        //Getters
        public int GetRows() { return m_rows; }
        public int GetCols() { return m_cols; }
        public Tile[,] GetTiles() { return m_tiles; }
        public Point GetTileDims() { return m_tileDims; }


        private Point m_tileDims = new Point(4, 4);
        private int m_cols = 240;
        private int m_rows = 160;
        private Tile[,] m_tiles;
    }
}