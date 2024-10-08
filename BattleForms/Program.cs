using BattleEngine.common;

namespace BattleForms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            Global.Init();

            Global.Schematiker.SaveSchematic(new Actor("actor"));
            Global.Schematiker.SaveSchematic(new Actor("supporting"));
            Global.Schematiker.SaveSchematic((Schematics.EnemySchematic)new Enemy("enemy"));
            Global.Schematiker.SaveSchematic((Schematics.EnemySchematic)new Enemy("annoyance"));
            Global.Schematiker.RefreshSchematics();

            ApplicationConfiguration.Initialize();
            Application.Run(new TestWindow());
        }
    }
}