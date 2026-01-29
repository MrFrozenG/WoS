using Terraria;

namespace WoS.Content.Core.ModUtils
{
    public static class ItemValues
    {
        static int Copper = 1;
        static int Silver = 100;
        static int Gold = 10_000;
        static int Platinum = 1_000_000;

        ///<summary>
        ///Defines value of Item, Copper * 1, Silver * 100, Gold * 10_000, Platinum * 1_000_000
        ///</summary>
        public static int Cost(int copper, int silver, int gold, int platinum)
        {
            return (copper * Copper) + (silver * Silver) + (gold * Gold) + (platinum * Platinum);
        }
        ///<summary>
        ///Default value for Sparks Series (Hololive Content)
        ///</summary>
        public static int HololiveSeries()
        {
            return Cost(0, 50, 15, 0);
        }
       
    }
}
