using Boredtopia.Controllers;

namespace Boredtopia.Models
{
    public class RockPaperScissor
    {
        public string UserId { get; set; }
        public int Id { get; set; }
        public int ScissorWins { get; set; }
        public int RockWins { get; set; }
        public int PaperWins { get; set; }
        public int Highscore { get; set; }
        public int TotalGames { get; set; }
        public double WinPercentDecimal { get; set; }

        // public RockPaperScissor()
        // {
        //     ScissorWins = 0;
        //     RockWins = 0;
        //     PaperWins = 0;
        //     Highscore = 0;
        // }
    }
}
