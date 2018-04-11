using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GladiatorArena
{
    class SpecialMove
    {
        public int m_id { get; private set; }
        public string m_name { get; private set; }
        public int m_range { get; private set; }
        public int m_duration { get; private set; }
        public int m_cooldown { get; private set; }

        public SpecialMove()
        {
            m_id = -1;
            m_name = "";
            m_range = 1;
            m_duration = 1;
            m_cooldown = 60;
        }
    }
}
