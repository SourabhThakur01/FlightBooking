using System;
using System.Collections.Generic;
using System.IO;
using Traveless.Manager.Abstract;
using Traveless.Manager.Entities;

namespace Traveless.Manager
{
    /// <summary>
    /// Manages flights
    /// </summary>
    public class MyFlightManager : FlightManager
    {
        /// <summary>
        /// Path to flights.csv file
        /// </summary>
        public static readonly string FLIGHTS_FILE = "Data/flights.csv";

        /// <summary>
        /// Populates list with Flight instances from file
        /// </summary>
        protected override void LoadFlights()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FLIGHTS_FILE);
            // Open StreamReader at path
            StreamReader reader = File.OpenText(path);

            string? line = reader.ReadLine();
            // Loop through each line in the file
            while (line != null)
            {
                //  Transform line into cells using a comma (,)
                string[] cells = line.Split(',');

                if (cells.Length != 7)
                {
                    //  Check number of cells is not 7
                    //      Do next iteration of loop if incorrect number of cells
                    continue;
                }

                //  Create Flight instance from cells
                Flight flight = new Flight();
                flight.Code = cells[0].ToString();
                flight.From = cells[1].ToString();
                flight.To = cells[2].ToString();
                flight.WeekDay = cells[3].ToString();
                flight.Time = cells[4].ToString();
                flight.TotalSeats = Convert.ToInt32(cells[5]);
                flight.CostPerSeat = Convert.ToDecimal(cells[6]);

                //  Add Flight instance to _flights list
                _flights.Add(flight);

                line = reader.ReadLine();
            }
            reader.Close();
        }

        /// <summary>
        /// Finds flight with code
        /// </summary>
        /// <param name="code">Flight code argument</param>
        /// <returns>Flight instance (or null if not found)</returns>
        public override Flight? FindFlightByCode(string code)
        {

            // Loop through each flight in Flights
            foreach (var flight in _flights)
            {
                //  Check current flight code exactly matches code argument
                if (flight.Code == code)
                {
                    //      Return current Flight instance to calling method
                    LoadFlights();
                }
            }
            return null;
        }
    }
}
