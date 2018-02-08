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

namespace GameCardLibrary
{
    /// <summary>
    /// Interface for a person object - a player or dealer
    /// </summary>
    public interface IPerson
    {
        Hand Hand { get; set; }
        int Score { get; set; }
        String Name { get; set; }
        bool IsThick { get; set; }

    }
}
