using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GladiatorArena
{
    class Player
    {
        public int m_id { get; private set; }
        public bool m_isHuman { get; private set; }
        public float m_lifePoints { get; private set; }
        public int m_dirFacing { get; private set; }
        public int[] m_weaponId { get; private set; }
        public int m_specialMoveId { get; private set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Player()
        {
            m_id = -1;
            m_isHuman = true;
            m_lifePoints = 100.0f;
            //refers to surrounding cells starting from topLeft in clockwise direction
            m_dirFacing = 7;
            foreach (int weapon in m_weaponId) { m_weaponId[weapon] = 0; }
            m_specialMoveId = -1;
        }
    }
}
