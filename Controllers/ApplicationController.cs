
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
            positivityRates.Add((int)(stateResults).Where(x=>x.state == StartingLocation).ToArray()[0].positiveIncrease);
            positivityRates.Add((int)(stateResults).Where(x=>x.state == EndingLocation).ToArray()[0].positiveIncrease);
            
            //split conections 
            Array connections = ConnectingLocations.Split(",");
            foreach(string s in connections){
                try{
                   positivityRates.Add((int)(stateResults).Where(x=>x.state == s).ToArray()[0].positiveIncrease);
                }
                catch{
                    Console.WriteLine("not a valid state");
                }
            }

            ViewBag.Description = calculateRisk(positivityRates);
            return View();
        }
    }
}