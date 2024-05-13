using System;
using System.Collections.Generic;
using System.Text;

namespace Josephus3
{
    public class Soldier
    {
        public enum Race
        {
            Man,
            Elf,
            Giant,
            Hobbit,
            Dwarf,
        }

        public String Name { get; set; }
        public int Location { get; set; }
        public Soldier Right { get; set; }
        public Soldier Left { get; set; }
        public int LifeForce;
        public Race RaceType { get; set; }
        public Sword Sword { get; set; }
        public Soldier(String na, int lo, int ra, Sword sw)
        {
            Name = na;
            Location = lo;
            Sword = sw;
            Right = null;
            Left = null;
            RaceType = (Race)ra;
            LifeForce = GetLifeForce();

        }

        private int GetLifeForce()
        {
            int lf = 0;
            switch (RaceType)
            {
                case Race.Man:
                    lf = 70;
                    break;
                case Race.Elf:
                    lf = 100;
                    break;
                case Race.Giant:
                    lf = 150;
                    break;
                case Race.Hobbit:
                    lf = 50;
                    break;
                case Race.Dwarf:
                    lf = 90;
                    break;
            }
            return lf;
        }
        public void SetRightNeighbor(Soldier Neighbor)
        {
            Right = Neighbor;
            Neighbor.Left = this;
        }

        public void KillRightNeighbor()
        {
            while(Right.LifeForce > 0)
            {
                Sword.Swing(Right);
            }

            Console.WriteLine(ToString() + ", kills " + Right.ToString());
            Right = Right.Right;

        }

        public void CommenceGroupSuicide(Soldier Starter, int SkipVal)
        {
            Soldier CurrSoldier = Starter;

            while (!CurrSoldier.Right.Equals(CurrSoldier))
            {

                for (int i = 1; i < SkipVal; i++)
                {
                    CurrSoldier = CurrSoldier.Right;
                }
                CurrSoldier.KillRightNeighbor();

            }

                Console.WriteLine(CurrSoldier.ToString() + " remains");
        }

        override
        public String ToString()
        {
            return "Soldier " + Name + ", " + RaceType + ", bearer of " + Sword.Name + ", location " + Location;
        }
      
        public bool Equals(Soldier Other)
        {
            bool returnVal = false;
            if(Location == Other.Location)
            {
                returnVal = true;
            }
            return returnVal;
        }

    }
   
}
