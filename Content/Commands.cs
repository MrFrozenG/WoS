using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace WoS.Content
{
    public class Commands : ModCommand
    {
        public override CommandType Type => CommandType.World;
        public override string Command => "world";
        public override string Usage => "/world time <ticks|day|night> or /world difficulty <journey|classic|expert|master>";
        public override string Description => "Manipulate world time or difficulty";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (args.Length < 2)
                throw new UsageException("Not enough arguments. Usage: " + Usage);

            string subCommand = args[0].ToLower();
            string argument = args[1].ToLower();

            switch (subCommand)
            {
                case "time":
                    HandleTimeCommand(argument, caller);
                    break;

                case "difficulty":
                    HandleDifficultyCommand(argument);
                    break;

                default:
                    throw new UsageException("Unknown subcommand: " + subCommand);
            }
        }

        private void HandleTimeCommand(string arg, CommandCaller caller)
        {
            // Полный цикл (день + ночь)
            const double cycleLength = Main.dayLength + Main.nightLength;
            double fullTime = Main.time + (Main.dayTime ? 0 : Main.dayLength);

            if (arg == "day")
            {
                fullTime = 0; // Устанавливаем утро
                Main.dayTime = true;
                caller.Reply("World difficulty set to " + arg + ".", Color.Gold);
            }
            else if (arg == "night")
            {
                fullTime = Main.dayLength + 1;
                Main.dayTime = false;
                caller.Reply("World difficulty set to " + arg + ".", Color.Gold);
            }
            else if (int.TryParse(arg, out int deltaTime))
            {
                fullTime += deltaTime;
                fullTime %= cycleLength;
                if (fullTime < 0)
                    fullTime += cycleLength;

                Main.dayTime = fullTime < Main.dayLength;

                if (!Main.dayTime)
                    fullTime -= Main.dayLength;

                Main.time = fullTime;
            }
            else
            {
                throw new UsageException("Invalid time value: " + arg);
            }

            if (Main.netMode == NetmodeID.Server)
                NetMessage.SendData(MessageID.WorldData);
        }
        private void HandleDifficultyCommand(string arg)
        {
            byte newDifficulty;

            switch (arg)
            {
                case "journey":
                    newDifficulty = 3;
                    break;
                case "classic":
                case "normal":
                    newDifficulty = 0;
                    break;
                case "expert":
                    newDifficulty = 1;
                    break;
                case "master":
                    newDifficulty = 2;
                    break;
                default:
                    throw new UsageException("Invalid difficulty. Use journey, classic, expert, or master.");
            }

            if (Main.GameMode == newDifficulty)
            {
                Main.NewText("World difficulty is already set to " + arg + ".", 200, 200, 0);
                return;
            }

            Main.GameMode = newDifficulty;
            Main.NewText("World difficulty set to " + arg + ". Saving world...", 255, 240, 20);

            if (Main.netMode == NetmodeID.Server)
                NetMessage.SendData(MessageID.WorldData);
        }
    }
}
