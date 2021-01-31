
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Numerics;
namespace hackathon.Controllers{

    public class ApplicationController: Controller{
    Dictionary<string, string> aiportToStates = new Dictionary<string, string>(){
                {"Hartsfield–Jackson Atlanta International Airport", "GA"},
                {"Los Angeles International Airport", "CA"},
                {"O'Hare International Airport", "IL"},
                {"Dallas/Fort Worth International Airport", "TX"},
                {"Denver International Airport", "CO"},
                {"John F. Kennedy International Airport", "NY"},
                {"San Francisco International Airport", "CA"},
                {"Seattle–Tacoma International Airport", "WA"},
                {"McCarran International Airport", "NV"},
                {"Orlando International Airport", "FL"},
                {"Charlotte Douglas International Airport", "NC"},
                {"Newark Liberty International Airport", "NJ"},
                {"Phoenix Sky Harbor International Airport", "AZ"},
                {"George Bush Intercontinental Airport", "TX"},
                {"Miami International Airport", "FL"},
                {"General Edward Lawrence Logan International Airport", "MA"},
                {"Minneapolis–Saint Paul International Airport", "MN"},
                {"Detroit Metropolitan Airport", "MI"},
                {"Fort Lauderdale–Hollywood International Airport", "FL"},
                {"Philadelphia International Airport", "PA"},
                {"LaGuardia Airport", "NY"},
                {"Baltimore–Washington International Airport", "MD"},
                {"Salt Lake City International Airport", "UT"},
                {"San Diego International Airport", "CA"},
                {"Washington Dulles International Airport", "VA"},
                {"Ronald Reagan Washington National Airport", "VA"},
                {"Tampa International Airport", "FL"},
                {"Chicago Midway International Airport", "IL"},
                {"Daniel K. Inouye International Airport", "HI"},
                {"Portland International Airport", "OR"},
                {"Nashville International Airport", "TN"},
                {"Austin–Bergstrom International Airport", "TX"},
                {"Dallas Love Field", "TX"},
                {"St. Louis Lambert International Airport", "MO"},
                {"Norman Y. Mineta San José International Airport", "CA"},
                {"William P. Hobby Airport", "TX"},
                {"Raleigh-Durham International Airport", "NC"},
                {"Louis Armstrong New Orleans International Airport", "LA"},
                {"Oakland International Airport", "CA"},
                {"Sacramento International Airport", "CA"},
                {"Kansas City International Airport", "MO"},
                {"John Wayne Airport", "CA"},
                {"Southwest Florida International Airport", "FL"},
                {"San Antonio International Airport", "TX"},
                {"Cleveland Hopkins International Airport", "OH"},
                {"Pittsburgh International Airport", "PA"},
                {"Indianapolis International Airport", "IN"},
                {"Luis Muñoz Marín International Airport", "PR"},
                {"Cincinnati/Northern Kentucky International Airport", "OH"},
                {"John Glenn Columbus International Airport", "OH"}
            };
            Dictionary <string, int> statePopulations = new Dictionary <string, int>() {
                {"CA", 39512223},
                {"TX", 28995881},
                {"FL", 21477737},
                {"NY", 19453561},
                {"IL", 12671821},
                {"PA", 12801989},
                {"OH", 11689100},
                {"GA", 10617423},
                {"NC", 10488084},
                {"MI", 9986857},
                {"NJ", 8882190},
                {"VA", 8535519},
                {"WA", 7614893},
                {"AZ", 7278717},
                {"MA", 6949503},
                {"TN", 6833174},
                {"IN", 6732219},
                {"MO", 6137428},
                {"MD", 6045680},
                {"WI", 5822434},
                {"CO", 5758736},
                {"MN", 5639632},
                {"SC", 5148714},
                {"AL", 4903185},
                {"LA", 4648794},
                {"KY", 4467673},
                {"OR", 4217737},
                {"OK", 3956971},
                {"CT", 3565287},
                {"UT", 3205958},
                {"IA", 3155070},
                {"NV", 3080156},
                {"AR", 3017825},
                {"MS", 2976149},
                {"KS", 2913314},
                {"NM", 2096829},
                {"NE", 1934408},
                {"WV", 1792147},
                {"ID", 1787065},
                {"HI", 1415872},
                {"NH", 1359711},
                {"ME", 1344212},
                {"MT", 1068778},
                {"RI", 1059361},
                {"DE", 973764},
                {"SD", 884659},
                {"ND", 762062},
                {"AK", 731545},
                {"DC", 705749},
                {"VT", 623989},
                {"WY", 578759}
            };
            //Based on 2019 Data
            Dictionary<string, string> airportToDailyPersons = new Dictionary<string, string>(){
                {"Hartsfield–Jackson Atlanta International Airport", "146591"},
                {"Los Angeles International Airport", "117641"},
                {"O'Hare International Airport", "111976"},
                {"Dallas/Fort Worth International Airport", "98023"},
                {"Denver International Airport", "92035"},
                {"John F. Kennedy International Airport", "85032"},
                {"San Francisco International Airport", "76107"},
                {"Seattle–Tacoma International Airport", "68498"},
                {"McCarran International Airport", "67749"},
                {"Orlando International Airport", "67294"},
                {"Charlotte Douglas International Airport", "66301"},
                {"Newark Liberty International Airport", "63454"},
                {"Phoenix Sky Harbor International Airport", "61462"},
                {"George Bush Intercontinental Airport", "60015"},
                {"Miami International Airport", "58688"},
                {"General Edward Lawrence Logan International Airport", "56711"},
                {"Minneapolis–Saint Paul International Airport", "52583"},
                {"Detroit Metropolitan Airport", "49707"},
                {"Fort Lauderdale–Hollywood International Airport", "49180"},
                {"Philadelphia International Airport", "43853"},
                {"LaGuardia Airport", "42174"},
                {"Baltimore–Washington International Airport", "36396"},
                {"Salt Lake City International Airport", "35180"},
                {"San Diego International Airport", "34653"},
                {"Washington Dulles International Airport", "32559"},
                {"Ronald Reagan Washington National Airport", "31768"},
                {"Tampa International Airport", "30078"},
                {"Chicago Midway International Airport", "27621"},
                {"Daniel K. Inouye International Airport", "27366"},
                {"Portland International Airport", "26842"},
                {"Nashville International Airport", "25020"},
                {"Austin–Bergstrom International Airport", "23790"},
                {"Dallas Love Field", "23036"},
                {"St. Louis Lambert International Airport", "21772"},
                {"Norman Y. Mineta San José International Airport", "21449"},
                {"William P. Hobby Airport", "19368"},
                {"Raleigh-Durham International Airport", "18957"},
                {"Louis Armstrong New Orleans International Airport", "18403"},
                {"Oakland International Airport", "17973"},
                {"Sacramento International Airport", "17683"},
                {"Kansas City International Airport", "15779"},
                {"John Wayne Airport", "14118"},
                {"Southwest Florida International Airport", "14094"},
                {"San Antonio International Airport", "13761"},
                {"Cleveland Hopkins International Airport", "13409"},
                {"Pittsburgh International Airport", "12920"},
                {"Indianapolis International Airport", "12901"},
                {"Luis Muñoz Marín International Airport", "12575"},
                {"Cincinnati/Northern Kentucky International Airport", "12091"},
                {"John Glenn Columbus International Airport", "11430"}
            };
            
       private string mapInfo(List<State> stateResults){
           string ans = "";
           var val = statePopulations.Keys.ToList();
           val.Sort();
           foreach(string key in val){
               var st = (stateResults).Where(x=>x.state ==key).ToArray()[0];
               var startingInfo = (st.total*130)/statePopulations[key];
               ans+=key+","+startingInfo+"-";
           }
           return ans;
       }     
       public ActionResult Index(string StartingLocation = "",string EndingLocation= "", int  NumConnections=0, string ConnectingLocations= "")
        {
            ViewBag.StartingLocation = StartingLocation;
            ViewBag.EndingLocation = EndingLocation;
            ViewBag.NumConnections = NumConnections;
            ViewBag.ConnectingLocations = ConnectingLocations;
            return View();
        }

        private double calculateRisk(List<int> positivityRates, List<String> airports){
            //black box code
            double totalRisk = 0;
            for (int i = 0; i < airports.Count; i ++) {
                double population = double.Parse(airportToDailyPersons[airports[i]]);
                double totalInfected = positivityRates[i];
                double numOfPeopleContacted = 40;

                BigInteger numerator = (BigInteger)Math.Pow(population - totalInfected, numOfPeopleContacted);
                BigInteger denom = (BigInteger)Math.Pow(population, numOfPeopleContacted);



                Console.WriteLine(numerator);
                Console.WriteLine(denom);
                Console.WriteLine(factorialDivision(numerator, denom));


                totalRisk += (1 - ((double) (numerator) / (double) (denom)));
            }

            return totalRisk;
        }

        private decimal factorialDivision(BigInteger num, BigInteger denom) {
            decimal result = 1;

            for(BigInteger i = (num + 1); i <= denom; i ++) {
                result *= ((decimal)i) / 1000000;
            }
            
            return(result);
        }


        public async Task<ActionResult> RiskOutcome(string StartingLocation, string EndingLocation, int  NumConnections, string ConnectingLocations){
           
            //creates the client 
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.covidtracking.com/v1/states/current.json");
            String urlParameters = "";

            //query 
            HttpResponseMessage response = client.GetAsync(urlParameters).Result; 
            response.EnsureSuccessStatusCode();

            //gets response 
            string responseBody = await response.Content.ReadAsStringAsync();
            var stateResults = Newtonsoft.Json.JsonConvert.DeserializeObject<List<State>>(responseBody);
            var startingInfo = (stateResults).Where(x=>x.state == aiportToStates[StartingLocation]).ToArray()[0];
            ViewBag.MapInfo = mapInfo(stateResults);
            ViewBag.Message = mapInfo(stateResults);
            //add positivity rates 
            List<int> positivityRates = new List<int>();
            List<String> airports = new List<string>();
            positivityRates.Add((int)(stateResults).Where(x=>x.state == aiportToStates[StartingLocation]).ToArray()[0].positiveIncrease);
            airports.Add(StartingLocation);
            
            //split conections 
            Array connections = ConnectingLocations.Split(",");
            foreach(string s in connections){
                try{
                   airports.Add(s);
                   positivityRates.Add((int)(stateResults).Where(x=>x.state == aiportToStates[s]).ToArray()[0].positiveIncrease);
                }
                catch{
                    Console.WriteLine("not a valid state");
                }
            }

            positivityRates.Add((int)(stateResults).Where(x=>x.state == aiportToStates[EndingLocation]).ToArray()[0].positiveIncrease);
            airports.Add(EndingLocation);


            // ViewBag.Description = calculateRisk(positivityRates, airports);
             ViewBag.Description = 100;

            //Console.WriteLine(positivityRates[0]);
            return View();
        }
    }
}