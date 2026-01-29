using Terraria.ID;
using Terraria.ModLoader;

namespace WoS.Content.Core.ModSets
{
    [ReinitializeDuringResizeArrays]
    public static class WoSNPCsSets
    {
        public static bool[] Slimes = NPCID.Sets.Factory.CreateNamedSet("Slimes")
        .Description("Slimes group")
        .RegisterBoolSet(false,
        NPCID.FromNetId(NPCID.GreenSlime),
        NPCID.BlueSlime,
        //NPCID.RedSlime,
        //NPCID.PurpleSlime,
        //NPCID.YellowSlime,
        //NPCID.BlackSlime,
        NPCID.IceSlime,
        NPCID.SandSlime,
        //NPCID.JungleSlime,
        NPCID.SpikedIceSlime,
        NPCID.SpikedJungleSlime,
        NPCID.MotherSlime,
        //NPCID.BabySlime,
        NPCID.LavaSlime,
        NPCID.DungeonSlime,
        //NPCID.Pinky,
        NPCID.GoldenSlime,
        NPCID.SlimeSpiked,
        NPCID.UmbrellaSlime,
        NPCID.ShimmerSlime,
        NPCID.SlimeMasked,
        NPCID.SlimeRibbonYellow,
        NPCID.SlimeRibbonGreen,
        NPCID.SlimeRibbonRed,
        NPCID.SlimeRibbonWhite,
        NPCID.ToxicSludge,
        NPCID.CorruptSlime,
        //NPCID.Slimeling,
        NPCID.Slimer,
        //NPCID.Slimer2,
        NPCID.Crimslime,
        //NPCID.BigCrimslime,
        //NPCID.LittleCrimslime,
        NPCID.Gastropod,
        NPCID.IlluminantSlime,
        NPCID.RainbowSlime,
        NPCID.QueenSlimeMinionBlue,
        NPCID.QueenSlimeMinionPink,
        NPCID.FromNetId(NPCID.RedSlime),
        NPCID.FromNetId(NPCID.PurpleSlime),
        NPCID.FromNetId(NPCID.YellowSlime),
        NPCID.FromNetId(NPCID.BlackSlime),
        NPCID.FromNetId(NPCID.JungleSlime),
        NPCID.FromNetId(NPCID.BabySlime),
        NPCID.FromNetId(NPCID.Pinky),
        NPCID.FromNetId(NPCID.Slimeling),
        NPCID.FromNetId(NPCID.Slimer2),
        NPCID.FromNetId(NPCID.BigCrimslime),
        NPCID.FromNetId(NPCID.LittleCrimslime),
        NPCID.QueenSlimeMinionPurple
        );
        public static bool[] RingofFlamesEnemies = NPCID.Sets.Factory.CreateNamedSet("RingofFlamesEnemies")
       .Description("Enemies for Ring of Living Flames(And Upgrades) buff")
       .RegisterBoolSet(false,
       NPCID.LavaSlime,
       NPCID.Lavabat,
       NPCID.Hellbat
       ); 
    }
}
