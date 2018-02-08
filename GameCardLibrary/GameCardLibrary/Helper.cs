using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLibrary
{
    public static class Helper
    {
        public static String translateValue(int value)
        {
            String colorValue = "";
            if (value <= 10)
            {
                return null;
            }
            else
            {
                switch (value)
                {
                    case 11:
                        colorValue = "j";
                        break;
                    case 12:
                        colorValue = "q";
                        break;
                    case 13:
                        colorValue = "k";
                        break;
                }

                return colorValue;
            }
        }

        public static String getStatus(int status)
        {
            String condition = "";

            if (status == 0)
            {
                // PLAYER LOST
                condition = "LOSE";
            }
            else if (status == 1)
            {
                // PLAYER WON
                condition = "WIN";
            }
            else
            {
                // NOTHING HAPPENS
                condition = "CONTINUE";
            }
            return condition;
        }
    }
}
