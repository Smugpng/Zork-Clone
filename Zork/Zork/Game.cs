using System.IO;
using System;
using Newtonsoft.Json;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Text;
using Microsoft.DotNet.Cli.Utils;

namespace Zork
{
    public class Game
    {
        [JsonIgnore]
        public static Game Instance { get; private set; }

        public World World { get; set; }

        [JsonIgnore]
        public Player Player { get; private set; }

        [JsonIgnore]
        private bool IsRunning { get; }

        [JsonIgnore]
        public CommandManger CommandManger { get; }

        public Game(World world,Player player)
        {
            World = world;
            Player = player;
        }
        public Game()=> CommandManger = new CommandManger();

        public static void Start(string gameFilename)
        {
            if (!File.Exists(gameFilename))
            {
                throw new FileNotFoundException("Expected file", gameFilename);
            }

            while (Instance == null || Instance.mIsRestarting)
            {
                Instance = Load(gameFilename);
                Instance.LoadCommands();
                Instance.LoadScripts();
                Instance.DisplayWelcomMessage();
                Instance.Run();
            }
        }
        public void Run()
        {
            mIsRunning = true;
            Room previousRoom = null;
            while(mIsRunning)
            {
                Console.WriteLine(Player.Location);
                if(previousRoom != Player.Location)
                {
                    CommandManger.PerformCommand(this, "LOOK");
                    previousRoom = Player.Location;
                }
                Console.Write("\n> ");
                if(CommandManger.PerformCommand(this, Console.ReadLine().Trim()))
                {
                    Player.Moves++;
                }
                else
                {
                    Console.WriteLine("This is not a verb i recognize.");
                }
            }
        }
        public void Restart()
        {
            mIsRestarting = true;
            mIsRunning = false;
            Console.Clear();
        }
        public void Quit()=> mIsRunning = false;
        private void LoadCommands()
        {
            var commandMethods = (from type in Assembly.GetExecutingAssembly().GetTypes()
                                  from method in type.GetMethods()
                                  let attribute = method.GetCustomAttribute<CommandAttribute>()
                                  where type.IsClass && type.GetCustomAttribute<CommandClassAtribute>() != null
                                  where attribute != null
                                  select new Command(attribute.CommandName, attribute.Verbs,
                                  (Action<Game, CommandContext>)Delegate.CreateDelegate(typeof(Action<Game, CommandContext>),
                                  method)));

            CommandManger.Addcommnds(commandMethods);
        }

        private void LoadScripts()
        {
            foreach (string file in Directory.EnumerateFiles(ScriptDirectory, ScriptFileExtension))
            {
                try
                {
                    var scriptOptions = ScriptOptions.Default.AddReferences(Assembly.GetExecutingAssembly());
#if DEBUG
                    scriptOptions = scriptOptions.WithEmitDebugInformation(true)
                        .WithFilePath(new FileInfo(file).FullName)
                        .WithFileEncoding(Encoding.UTF8);
#endif
                    string script = File.ReadAllText(file);
                    CSharpScript.RunAsync(script, scriptOptions).Wait();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error compiling script: {file} Error: {ex.Message}");
                }
            }
        }
        public bool ConfirmAction(string prompt)
        {
            Console.Write(prompt);
            while (true)
            {
                string response = Console.ReadLine().Trim().ToUpper();
                if(response =="YES" || response == "Y")
                {
                    return true;
                }
                else if (response == "NO" || response == "N") 
                { 
                    return false;
                }
                else
                {
                    Console.Write("please answer yes or no");
                }
            }
        }

        private void DisplacewelcomeMessage() => Console.WriteLine(WelcomeMessage);

        public static readonly Random random = new Random();
        private static readonly string ScriptDirectory = "Scripts";
        private static readonly string ScriptFileExtension = "*.csx";

        [JsonProperty]
        private string WelcomeMessage = null;

        private bool mIsRunning;
        private bool mIsRestarting;
    }
}