using EntryPoint;

namespace HomeTrack.CLI
{
    public class HomeTrackArguments : BaseCliArguments
    {
        public HomeTrackArguments() : base("HomeTrack") { }

        /// <summary>
        /// Path to config yaml file
        /// </summary>
        [Help("Путь к файлу конфигурации *.yaml.")]
        [OptionParameter(ShortName: 'c', LongName: "config")]
        public string ConfigPath { get; set; }
    }
}
