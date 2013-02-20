using Mono.Options;

namespace SocketFree
{
    public class ProgramOptions
    {
        private int _port;
        public int Port { get { return _port; } }
        public bool ShowHelp { get; private set; }

        public void Parse(string[] args)
        {
            var optionSet = new OptionSet
                                {
                                    { "?|h|help", v => ShowHelp = true },
                                    { "p=|port=", v => int.TryParse(v, out _port ) }
                                };
            optionSet.Parse(args);
        }
    }
}