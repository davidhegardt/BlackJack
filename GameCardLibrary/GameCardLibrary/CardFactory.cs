/// <summary>
/// Game Card Library
/// Assignment 1 - C# III Course
/// David Hegardt
/// 2017-10-09
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace GameCardLibrary
{
    /// <summary>
    /// Factory pattern used to create cards
    /// </summary>
    public static class CardFactory
    {
        public static Card CreateCard(int value,SuiteEnum suite)
        {
            String suiteLetter = GetSuite(suite);
            String valueLetter = "";
            
            if (value <= 10)
            {
                valueLetter = value.ToString();
            } else
            {
                //valueLetter = translateValue(value);
                valueLetter = Helper.translateValue(value);
            }

            String cardImage = suiteLetter + valueLetter + ".png";

            Card newCard = new Card(suite, value, cardImage);
            newCard.FaceDown = "b1fv.png";

            return newCard;
            
        }
        /// <summary>
        /// Get the letter of the current suite
        /// </summary>
        /// <param name="currSuite"></param>
        /// <returns></returns>
        private static String GetSuite(SuiteEnum currSuite)
        {
            string letter = "";
            switch (currSuite)
            {
                case SuiteEnum.Hearts: letter = "h";
                    break;
                case SuiteEnum.Diamonds: letter = "d";
                    break;
                case SuiteEnum.Spades: letter = "s";
                    break;
                case SuiteEnum.Clubs: letter = "c";
                    break;
            }

            return letter;
        }

    }
}
