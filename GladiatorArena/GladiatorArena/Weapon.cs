using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GladiatorArena
{
    class Weapon
    {
        public int m_id { get; private set; }
        public string m_name { get; private set; }
        public int m_damagePoints { get; private set; }
        public int m_speed { get; private set; }
        public int m_durability { get; private set; }
        public int m_range { get; private set; }

        public Weapon()
        {
            m_id = -1;
            m_name = "";
            m_damagePoints = 10;
            m_speed = 1;
            m_durability = 100;
            m_range = 1;
        }
    }
}
