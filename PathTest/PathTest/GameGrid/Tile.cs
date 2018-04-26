using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathTest
{
    class Tile
    {
        public Tile()
        {
            //Empty
        }

        public Tile(int i, int j, Vector2 size, GraphicsDevice gDevice)
        {
            if(Util.GetRandom(0, 100) < 10)
                m_solid = true;

            m_size = size;

            m_position = new Vector2(i * m_size.X, j * m_size.Y);

            ChangeColour(Color.White, gDevice);
        }

        public void Draw(SpriteBatch spriteBatch, Color col)
        {
            if (m_solid == true)
                spriteBatch.Draw(m_texture, m_position, Color.Black);
            else
                spriteBatch.Draw(m_texture, m_position, col);
        }

        public void Update(GraphicsDevice GDevice)
        {
            if (MouseClicked(ButtonState.Pressed))
            {
                if (m_released == true)
                {
                    m_released = false;
                    if (m_solid == true)
                        m_solid = false;
                    else
                        m_solid = true;
                }
            }
            if (MouseClicked(ButtonState.Released))
                m_released = true;
        }

        public Vector2 GetCentre()
        {
            Vector2 centre;
            centre.X = m_position.X + m_size.X / 2;
            centre.Y = m_position.Y + m_size.Y / 2;
            return centre;
        }

        public void AddNeighbours(GameGrid grid, bool diagonals = true)
        {
            int i = GetI();
            int j = GetJ();
            if (i < grid.GetCols() - 1)
                m_neighbours.Add(grid.GetTiles()[i + 1, j]);

            if (i > 0)
                m_neighbours.Add(grid.GetTiles()[i - 1, j]);

            if (j < grid.GetRows() - 1)
                m_neighbours.Add(grid.GetTiles()[i, j + 1]);

            if (j > 0)
                m_neighbours.Add(grid.GetTiles()[i, j - 1]);

            //Diagonals
            if (diagonals)
            {
                if ((i > 0) && (j > 0))
                    m_neighbours.Add(grid.GetTiles()[i - 1, j - 1]);

                if ((i < grid.GetCols() - 1) && (j > 0))
                    m_neighbours.Add(grid.GetTiles()[i + 1, j - 1]);

                if ((i > 0) && (j < grid.GetRows() - 1))
                    m_neighbours.Add(grid.GetTiles()[i - 1, j + 1]);

                if ((i < grid.GetCols() - 1) && (j < grid.GetRows() - 1))
                    m_neighbours.Add(grid.GetTiles()[i + 1, j + 1]);
            }
        }

        private bool MouseClicked(ButtonState bState)
        {
            var mouseState = Mouse.GetState();

            if (mouseState.LeftButton == bState)
                if (MouseInTile())
                    return true;

            return false;
        }

        private bool MouseInTile()
        {
            var mouseState = Mouse.GetState();
            var mousePosition = new Point(mouseState.X, mouseState.Y);
            if (((mousePosition.X < GetCentre().X + m_size.X / 2) && (mousePosition.X > GetCentre().X - m_size.X / 2))
                    && ((mousePosition.Y < GetCentre().Y + m_size.Y / 2) && (mousePosition.Y > GetCentre().Y - m_size.Y / 2)))
                return true;

            return false;
        }

        private void ChangeColour(Color Col, GraphicsDevice GDevice)
        {
            if(m_texture != null)
                m_texture.Dispose();
            m_texture = new Texture2D(GDevice, (int)m_size.X, (int)m_size.Y);
            Color[] data = new Color[(int)m_size.X * (int)m_size.Y];
            for (int i = 0; i < data.Length; ++i) data[i] = Col;
            m_texture.SetData(data);
        }

        //Getters
        public bool GetSolid() { return m_solid; }
        public int GetI() { return (int)m_position.X / (int)m_size.X; }
        public int GetJ() { return (int)m_position.Y / (int)m_size.Y; }
        public List<Tile> GetNeighbours() { return m_neighbours; }
        public Tile GetPrevTile() { return m_prevTile; }

        //Setters
        public void SetSolid(bool state) { m_solid = state; }
        public void SetPrevTile(Tile prevTile) { m_prevTile = prevTile; }

        //Privates
        private Texture2D m_texture = null;
        private Vector2 m_size;
        private Vector2 m_position;
        private bool m_released = true;
        private bool m_solid;
        private Tile m_prevTile = null;
        private List<Tile> m_neighbours = new List<Tile>();

        //Path
        public float f = 0; 
        public float g = 0;
        public float h = 0;
    }
}