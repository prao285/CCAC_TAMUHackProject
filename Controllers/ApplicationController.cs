
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
       public ActionResult Index(string StartingLocation = "",string EndingLocation= "", int  NumConnections=0, string ConnectingLocations= "")
        {
            ViewBag.StartingLocation = StartingLocation;
            ViewBag.EndingLocation = EndingLocation;
            ViewBag.NumConnections = NumConnections;
            ViewBag.ConnectingLocations = ConnectingLocations;
            return View();
        }

        private int calculateRisk(List<int> positivityRates){
            //black box code 
            int risk = 100;

            return risk;
        }
        public async Task<ActionResult> RiskOutcome(string StartingLocation,string EndingLocation, int  NumConnections, string ConnectingLocations){
           
            //creates the client 
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.covidtracking.com/v1/states/current.json");
            String urlParameters = "";

            //query 
            HttpResponseMessage response = client.GetAsync(urlParameters).Result; 
            response.EnsureSuccessStatusCode();

            //gets response 
            string responseBody = await response.Content.ReadAsStringAsync();
            var stateResults = Newtonsoft.Json.JsonConvert.DeserializeObject<List<State>>(responseBody);;
            var startingInfo = (stateResults).Where(x=>x.state == StartingLocation).ToArray()[0];

            //add positivity rates 
            List<int> positivityRates = new List<int>();
            positivityRates.Add((int)(stateResults).Where(x=>x.state == aiportToStates[StartingLocation]).ToArray()[0].positiveIncrease);
            positivityRates.Add((int)(stateResults).Where(x=>x.state == aiportToStates[EndingLocation]).ToArray()[0].positiveIncrease);
            
            //split conections 
            Array connections = ConnectingLocations.Split(",");
            foreach(string s in connections){
                try{
                   positivityRates.Add((int)(stateResults).Where(x=>x.state == aiportToStates[s]).ToArray()[0].positiveIncrease);
                }
                catch{
                    Console.WriteLine("not a valid state");
                }
            }

            ViewBag.Description = calculateRisk(positivityRates);
            Console.WriteLine(positivityRates[0]);
            return View();
        }
    }
}