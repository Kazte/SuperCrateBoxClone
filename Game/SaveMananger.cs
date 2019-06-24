using System.IO;

namespace Game
{
    public class SaveMananger
    {
        private static SaveMananger instance = null;

        private int currentScore;
        private string path = "save.csv";
        private const char splitter = '-';

        protected SaveMananger()
        {

        }

        public static SaveMananger Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SaveMananger();
                }
                return instance;
            }
        }

        public void Save(int currentScore)
        {
            this.currentScore = currentScore;
        }

        public string GetCsv()
        {
            string csv = GameMananger.Score.ToString() + "-" + GameMananger.HighScore.ToString();
            return csv;
        }

        public void SaveCsv()
        {
            var csv = GetCsv();
            File.WriteAllText(path, csv);
            Engine.Debug("Save Csv ");
        }

        public void LoadCsv()
        {
            if (File.Exists(path))
            {
                string csv = File.ReadAllText(path);
                string[] data = csv.Split(splitter);

                // Datos del jugador
                //Engine.Debug(data[0] + data[1]);

                GameMananger.HighScore = int.Parse(data[1]);


                Engine.Debug("Load Csv");

            }
            else
            {
                Engine.Debug("Archivo no encontrado");
            }
        }

        public bool CsvExists()
        {
            return File.Exists(path) ? true : false;
        }

        public void DeleteCsv()
        {
            File.Delete(path);
        }
    }
}
