using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;

namespace WoS.Content
{
    public class DebugContentDumper : ModSystem
    {
        public override void PostSetupContent()
        {
            if (!ModLoader.TryGetMod("ThoriumMod", out Mod thorium))
                return;

            string path = Path.Combine(Main.SavePath, "Thorium_ContentDump.txt");
            using StreamWriter writer = new StreamWriter(path, false);

            writer.WriteLine("=== ThoriumMod: ModItems ===");

            foreach (var modItem in thorium.GetContent<ModItem>())
            {
                writer.WriteLine($"Item: {modItem.Name} | Type: {modItem.Type}");
            }

            writer.WriteLine();
            writer.WriteLine("=== ThoriumMod: ModProjectiles ===");

            foreach (var modProj in thorium.GetContent<ModProjectile>())
            {
                writer.WriteLine($"Projectile: {modProj.Name} | Type: {modProj.Type}");
            }

            writer.WriteLine();
            writer.WriteLine("=== Завершено успешно ===");
        }
    }
}
