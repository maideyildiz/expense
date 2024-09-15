using System.IO;
using Newtonsoft.Json.Linq;

namespace ExpenseTracker.Infrastructure.Services;

    public class ConfigReader
    {
        private readonly string _filePath;

        public ConfigReader(string filePath)
        {
            _filePath = filePath;
        }

        public JObject ReadConfig()
        {
            if (!File.Exists(_filePath))
                throw new FileNotFoundException("Config file not found", _filePath);

            string json = File.ReadAllText(_filePath);
            return JObject.Parse(json);
        }
    }

