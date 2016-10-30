using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    enum DifficultyLevel
    {
        Multiplayer,
        Easy,
        Medium,
        Hard
    }

    class SetupModel
    {
        string playerNameX;
        string playerNameO;

        public string PlayerNameX
        {
            get { return playerNameX; }
            set
            {
                if (value == null || value == "")
                    playerNameX = "Player 1";
                else
                    playerNameX = value;
            }
        }

        public string PlayerNameO
        {
            get { return playerNameO; }
            set
            {
                if (value == null || value == "")
                    playerNameO = "Player 2";
                else
                    playerNameO = value;
            }
        }
        public DifficultyLevel Level { get; set; }
    }
}
