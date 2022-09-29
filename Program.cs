using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Marbles
{
    internal class Program
    {
        static void Main(string[] args) //this is the Johnny Sorter Server Method
        {
            // create JSON object for marbles (coming from api/db to get our payload)
            // this would be another server call
            string strMarbles = @"[
                { 'id': 1, 'color': 'blue', 'name': 'Bob', 'weight': 0.5 },
                { 'id': 2, 'color': 'red', 'name': 'John Smith', 'weight': 0.25 },
                { 'id': 3, 'color': 'violet', 'name': 'Bob O\'Bob', 'weight': 0.5 },
                { 'id': 4, 'color': 'indigo', 'name': 'Bob Dad-Bob', 'weight': 0.75 },
                { 'id': 5, 'color': 'yellow', 'name': 'John', 'weight': 0.5 },
                { 'id': 6, 'color': 'orange', 'name': 'Bob', 'weight': 0.25 },
                { 'id': 7, 'color': 'blue', 'name': 'Smith', 'weight': 0.5 },
                { 'id': 8, 'color': 'blue', 'name': 'Bob', 'weight': 0.25 },
                { 'id': 9, 'color': 'green', 'name': 'Bobb Ob', 'weight': 0.75 },
                { 'id': 10, 'color': 'blue', 'name': 'Bob', 'weight': 0.5 },
                { 'id': 11, 'color': 'orange', 'name': 'B', 'weight': 0.65 }
            ]";
            strMarbles = strMarbles.Replace(@"'", "\"");
            //convert JSON object to our clsMarbles class
            List<clsMarbles> marbles = JsonConvert.DeserializeObject<List<clsMarbles>>(strMarbles);

            //Test Case -duplicate marbles to reach one million marbles
            // No notable difference in time to get result back compared to the original marble list of 11 marbles
            //Random randomNumber = new Random();
            //while (marbles.Count < 1000000)
            //{
            //    marbles.Add(marbles[randomNumber.Next(0, marbles.Count - 1)]);
            //}

            List<clsMarbles> redMarbles = new List<clsMarbles>();
            List<clsMarbles> orangeMarbles = new List<clsMarbles>();
            List<clsMarbles> yellowMarbles = new List<clsMarbles>();
            List<clsMarbles> greenMarbles = new List<clsMarbles>();
            List<clsMarbles> blueMarbles = new List<clsMarbles>();
            List<clsMarbles> indigoMarbles = new List<clsMarbles>();
            List<clsMarbles> violetMarbles = new List<clsMarbles>();

            List<clsMarbles> roygbivMarbles = new List<clsMarbles>();

            foreach (clsMarbles marble in marbles)
            {
                if (marble.checkWeight() && marble.checkPalindrome())
                {
                    switch (marble.color)
                    {
                        case "red":
                            redMarbles.Add(marble);
                            break;
                        case "orange":
                            orangeMarbles.Add(marble);
                            break;
                        case "yellow":
                            yellowMarbles.Add(marble);
                            break;
                        case "green":
                            greenMarbles.Add(marble);
                            break;
                        case "blue":
                            blueMarbles.Add(marble);
                            break;
                        case "indigo":
                            indigoMarbles.Add(marble);
                            break;
                        case "violet":
                            violetMarbles.Add(marble);
                            break;
                        default:
                            //do nothing because this color is not in roygbiv
                            break;
                    }
                }
            }

            // Populate final marble list with the color marble lists
            roygbivMarbles.AddRange(redMarbles);
            roygbivMarbles.AddRange(orangeMarbles);
            roygbivMarbles.AddRange(yellowMarbles);
            roygbivMarbles.AddRange(greenMarbles);
            roygbivMarbles.AddRange(blueMarbles);
            roygbivMarbles.AddRange(indigoMarbles);
            roygbivMarbles.AddRange(violetMarbles);

            Console.Write(JsonConvert.SerializeObject(roygbivMarbles));
            Console.ReadLine();
        }

        public class clsMarbles
        {
            public int id { get; set; }
            public string color { get; set; }
            public string name { get; set; }
            public float weight { get; set; }

            public bool checkWeight()
            {
                if (this.weight >= 0.5)
                {
                    return true;
                }
                return false;
            }

            public bool checkPalindrome()
            {
                //convert name into lowercase and remove all non-alphanumeric characters
                string tempName = Regex.Replace(this.name.ToLower(), @"[^0-9a-zA-Z]+", "");
                int tempCount = 0;

                //check if name is null or empty
                if (tempName == "")
                {
                    return false;
                }
                //check if name length is odd or even
                if ((tempName.Length) % 2 == 0)
                {
                    tempCount = tempName.Length / 2;
                }
                else
                {
                    //if name length is 1 then it is a palindrome
                    if (tempName.Length == 1)
                    {
                        return true;
                    }
                    //if name length is odd then ignore the middle character
                    else
                    {
                        tempCount = (tempName.Length - 1) / 2;
                    }
                }

                // need the index of the character position so use for instead of forEach
                for (var i = 0; i < tempCount; i++)
                {
                    if (tempName[i] != tempName.ToCharArray()[tempName.Length - (i + 1)])
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}