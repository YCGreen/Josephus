using System;
using System.Text;

namespace Josephus3
{
    public class Sword
    {
        public String Name { get; set; }
        public int SwingPower { get; set; }
        public static int MaxSwingPower = 20;
        public static int MinSwingPower = 5;
        public Sword(String na, int SwP)
        {
            Name = na;
            SwingPower = SwP;
        }

        public void Swing(Soldier Enemy)
        {
            Enemy.LifeForce -= SwingPower;
        }

        override
        public String ToString()
        {
            return Name + ", swing power " + SwingPower;
        }

    }
}
