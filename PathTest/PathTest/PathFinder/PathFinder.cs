using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathTest
{
    //Enums
    enum STATUS
    {
        INIT,
        HALTED,
        CALCULATING,
        FINISHED,
        NOROUTE
    };

    enum HEURISTIC
    {
        EUCLIDIAN,
        MANHATTAN
    };

    class PathFinder
    {
        public PathFinder(GameGrid gGrid)
        {
            m_status = STATUS.HALTED;
            m_gGrid = gGrid;
            Init();
        }

        public virtual void Init()
        {
            m_status = STATUS.INIT;
            m_gGrid.ResetTiles();

            Point start = RandomPoint();
            m_gGrid.GetTiles()[start.X, start.Y].SetSolid(false);

            Point end = RandomPoint();
            m_gGrid.GetTiles()[end.X, end.Y].SetSolid(false);


            Reset(true);

            m_startTile = m_gGrid.GetTiles()[start.X, start.Y];
            m_endTile = m_gGrid.GetTiles()[end.X, end.Y];
        }

        public virtual void Reset(bool init = false)
        {
            //Empty
        }

        public void Start()
        {
            if (m_status != STATUS.INIT)
                m_status = STATUS.CALCULATING;
        }

        public void Stop()
        {
            if (m_status != STATUS.INIT)
                m_status = STATUS.HALTED;
        }

        public virtual void Update() { }

        public virtual void Draw(SpriteBatch spriteBatch) { }

        protected Point RandomPoint() 
        {
            int x = Util.GetRandom(0, m_gGrid.GetCols());
            int y = Util.GetRandom(0, m_gGrid.GetRows());
            return new Point(x, y);
        }

        //Getters
        public STATUS GetStatus() { return m_status; }

        //Private Members
        protected GameGrid m_gGrid;
        protected List<Tile> m_path = new List<Tile>();

        protected Tile m_startTile;
        protected Tile m_endTile;
        protected STATUS m_status;
    }
}
