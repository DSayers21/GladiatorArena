using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathTest
{
    class AStar : PathFinder
    {
        public AStar(GameGrid gGrid) : base(gGrid)
        {
            m_openSet.Add(m_startTile);
            m_status = STATUS.HALTED;
        }

        public override void Init()
        {
            base.Init();

            m_openSet.Add(m_startTile);
        }

        public override void Reset(bool init = false)
        {
            m_status = STATUS.HALTED;
            //Clear
            m_openSet.Clear();
            m_closedSet.Clear();
            m_path.Clear();

            if (!init)
                m_openSet.Add(m_startTile);
        }

        public override void Update()
        {
            if ((m_status == STATUS.CALCULATING) && (m_status != STATUS.INIT))
            {
                if (m_openSet.Count > 0)
                {
                    int Lowest = 0;
                    for (int i = 0; i < m_openSet.Count; i++)
                        if (m_openSet[i].f < m_openSet[Lowest].f)
                            Lowest = i;

                    Tile Current = m_openSet[Lowest];

                    UpdatePath(Current);

                    if (Current == m_endTile)
                    {
                        m_status = STATUS.FINISHED;
                        Console.WriteLine("Done");
                        return;
                    }

                    m_closedSet.Add(Current);
                    m_openSet.Remove(Current);

                    List<Tile> Neighbours = Current.GetNeighbours();

                    for (int i = 0; i < Neighbours.Count; i++)
                    {
                        Tile Neighbour = Neighbours[i];
                        if ((!m_closedSet.Contains(Neighbour)) && (!Neighbour.GetSolid()))
                        {
                            float TempG = Current.g + 1;
                            bool newPath = false;


                            if (m_openSet.Contains(Neighbour))
                            {
                                if (TempG < Neighbour.g)
                                {
                                    Neighbour.g = TempG;
                                    newPath = false;
                                }
                            }
                            else
                            {
                                newPath = true;
                                Neighbour.g = TempG;
                                m_openSet.Add(Neighbour);
                            }
                            if (newPath)
                            {
                                Neighbour.SetPrevTile(Current);
                                Neighbour.h = Heuristic(Neighbour, m_endTile);
                                Neighbour.f = Neighbour.g + Neighbour.h;
                            }
                        }
                    }
                }
                else
                {
                    m_status = STATUS.NOROUTE;
                    Console.WriteLine("No Solution");
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile tile in m_openSet)
                tile.Draw(spriteBatch, Color.Green);

            foreach (Tile tile in m_closedSet)
                tile.Draw(spriteBatch, Color.Red);

            foreach (Tile tile in m_path)
                tile.Draw(spriteBatch, Color.Orange);

            m_startTile.Draw(spriteBatch, Color.Pink);
            m_endTile.Draw(spriteBatch, Color.Coral);
        }

        private void UpdatePath(Tile current)
        {
            Tile temp = current;
            m_path.Clear();
            while (temp.GetPrevTile() != null)
            {
                m_path.Add(temp.GetPrevTile());
                temp = temp.GetPrevTile();
            }
        }

        private float Heuristic(Tile A, Tile B, HEURISTIC heuristic = HEURISTIC.MANHATTAN)
        {
            float distance = 0;
            switch (heuristic)
            {
                case HEURISTIC.EUCLIDIAN:
                    distance = (float)Math.Sqrt((float)Math.Pow((A.GetI() - B.GetI()), 2) + (float)Math.Pow((A.GetJ() - B.GetJ()), 2));
                    break;
                case HEURISTIC.MANHATTAN:
                    distance = Math.Abs(A.GetI() - B.GetI()) + Math.Abs(A.GetJ() - B.GetJ());
                    break;
            }
            return distance;
        }

        //Member Variables
        private List<Tile> m_closedSet = new List<Tile>();
        private List<Tile> m_openSet = new List<Tile>();
    }
}