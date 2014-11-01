using System;
using System.Collections.Generic;
using System.Text;

namespace InRixDataManager
{
    /// <summary>
    /// Data format for INRIX data on each line.
    /// </summary>
    public sealed class InrixData
    {
        public String TMCCode;
        public int Average;
        public int Speed;
        public int Reference;
        public int Score;
        public double TravelTimeMinutes;

        public InrixData() {
            TMCCode = "";
            Average = -1;
            Speed = -1;
            Reference = -1;
            Score = -1;
            TravelTimeMinutes = -1.0;
        }

        public InrixData(String TMC, int Spd, int Scr, double TTM, int Avg, int Ref)
        {
            TMCCode = TMC;
            Average = Avg;
            Speed = Spd;
            Reference = Ref;
            Score = Scr;
            TravelTimeMinutes = TTM;
        }

        public InrixData(String TMC, int Spd, int Scr, double TTM)
        {
            TMCCode = TMC;
            Speed = Spd;
            Score = Scr;
            TravelTimeMinutes = TTM;
        }

        public InrixData(String TMC, int Spd, int Scr, double TTM, int Avg)
        {
            TMCCode = TMC;
            Average = Avg;
            Speed = Spd;
            Score = Scr;
            TravelTimeMinutes = TTM;
        }
    }
}
