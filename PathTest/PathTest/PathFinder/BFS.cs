using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathTest
{
    class BFS : PathFinder
    {
        public BFS(GameGrid gGrid) : base(gGrid)
        {
            //Empty
        }

        public override void Init()
        {
            base.Init();

            m_queue.Enqueue(m_startTile);
            m_status = STATUS.HALTED;
        }

        public override void Reset(bool init = false)
        {
            m_status = STATUS.HALTED;
            //Clear
            m_queue.Clear();
            m_exploredNodes.Clear();
            m_nodeParents.Clear();
            m_path.Clear();

            if (!init)
                m_queue.Enqueue(m_startTile);
        }

        public override void Update()
        {
            if ((m_status == STATUS.CALCULATING) && (m_status != STATUS.INIT))
            {
                if (m_queue.Count != 0)
                {
                    Tile current = m_queue.Dequeue();
                  
                    if (current == m_endTile)
                    {
                        m_status = STATUS.FINISHED;
                        Console.WriteLine("Done");
                        return;
                    }

                    List<Tile> neighbours = current.GetNeighbours();
                    foreach (Tile neighbour in neighbours)
                    {
                        if (!neighbour.GetSolid())
                        {
                            if (!m_exploredNodes.Contains(neighbour))
                            {
                                m_exploredNodes.Add(neighbour);
                                neighbour.SetPrevTile(current);
                                m_nodeParents.Add(neighbour, current);
                                m_queue.Enqueue(neighbour);
                            }
                        }
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile tile in m_exploredNodes)
                tile.Draw(spriteBatch, Color.Red);


            foreach (Tile tile in m_queue)
                tile.Draw(spriteBatch, Color.Green);

            if (m_status == STATUS.FINISHED)
            {
                UpdatePath(m_endTile);

                foreach (Tile tile in m_path)
                    tile.Draw(spriteBatch, Color.Orange);
            }

            m_startTile.Draw(spriteBatch, Color.Pink);
            m_endTile.Draw(spriteBatch, Color.Coral);
        }

        private void UpdatePath(Tile current)
        {
            Tile curr = current;
            m_path.Clear();
            while (curr != m_startTile)
            {
                m_path.Add(curr);
                curr = m_nodeParents[curr];
            }
        }

        private Queue<Tile> m_queue = new Queue<Tile>();
        private HashSet<Tile> m_exploredNodes = new HashSet<Tile>();
        private Dictionary<Tile, Tile> m_nodeParents = new Dictionary<Tile, Tile>();
    }
}