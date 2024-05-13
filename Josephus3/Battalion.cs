using System;
using System.Text;
using System.Configuration;

namespace Josephus3
{
    class Battalion

    {
        private int SoldierCt;
        private int SkipVal;
        private int DesignatedStarterLoc;
        private Soldier DesignatedStarter;
        private Soldier Captain;
        private readonly int CaptainPos = 1;

        public Battalion()
        {
            SoldierCt = int.Parse(ConfigurationManager.AppSettings["SoldierCt"]); 
            SkipVal = int.Parse(ConfigurationManager.AppSettings["SkipVal"]);
            DesignatedStarterLoc = int.Parse(ConfigurationManager.AppSettings["DesignatedStarterLoc"]);
            Captain = new Soldier(
                ConfigurationManager.AppSettings["Name1"], CaptainPos, int.Parse(ConfigurationManager.AppSettings["Race1"]), 
                new Sword(ConfigurationManager.AppSettings["Sword1"], Sword.MaxSwingPower));
            DesignatedStarter = Captain;

            CreateBattalion();
            Console.WriteLine(ToString());
            Captain.CommenceGroupSuicide(DesignatedStarter, SkipVal);
        }

        public void CreateBattalion()
        {
            String[] Names = PullItems("Name");
            String[] Swords = PullItems("Sword");
            int[] Races = Array.ConvertAll(PullItems("Race"), int.Parse);
            Random rnd = new Random();

            Soldier CurrSoldier = Captain;

            for (int i = 1; i < SoldierCt; i++)
            {
                CurrSoldier.SetRightNeighbor(new Soldier(
                    Names[i], i + 1, Races[i], new Sword(Swords[i], 
                    rnd.Next(Sword.MinSwingPower, Sword.MaxSwingPower))));

                if (i == DesignatedStarterLoc)
                {
                    DesignatedStarter = CurrSoldier;
                }

                CurrSoldier = CurrSoldier.Right;
            }

            CurrSoldier.SetRightNeighbor(Captain);
        }

        //precondition: SoldierCt <= 14 (as specified in app.config), unless I add more people to app.config. If ct > 14, names/sword names/races will be null
        public String[] PullItems(String Item)
        {
            String[] Items = new String[SoldierCt];

            for(int i = 0; i < SoldierCt; i++)
            {
                String key = Item + (i+1).ToString();
                Items[i] = ConfigurationManager.AppSettings[key];
            }

            return Items;
        }

        override
        public String ToString()
        {
            StringBuilder BattalionString = new StringBuilder("Original Battalion:\n" + Captain.ToString() + "\n");
            Soldier CurrSoldier = Captain.Right;
            while (!CurrSoldier.Equals(Captain))
            {
                BattalionString.AppendLine(CurrSoldier.ToString());
                CurrSoldier = CurrSoldier.Right;
            }
            return BattalionString.ToString();
        }

        

    }
}
