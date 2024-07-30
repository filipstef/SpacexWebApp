using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpacexWebApp.Server.Data.Models;
using System.Text.Json;

namespace SpacexWebApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LaunchesController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        public LaunchesController(HttpClient httpClient) 
        {
            _httpClient = httpClient;   
        }

        [HttpGet("Latest")]        
        public async Task<IActionResult> GetLatestLaunches()
        {
            try
            {
                //Since this returns only a single launch we cannot use the generic class GetData
                var response = await _httpClient.GetAsync("https://api.spacexdata.com/v5/launches/latest");
                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode(StatusCodes.Status503ServiceUnavailable, "The spacex API seems to not be working");
                }

                List<Launch> launches = new List<Launch>();
                var launch1 = JsonSerializer.Deserialize<Launch>(content, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                });
                launches.Add(launch1);

                var customLaunches = await CombineLaunchData(launches);

                // Workaround to only send a single element and not a list
                return Ok(customLaunches[0]);
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Upcoming")]
        public async Task<IActionResult> GetUpcomingLaunches()
        {
            try
            {
                List<Launch> launches = await GetDataFromUrl<Launch>("https://api.spacexdata.com/v5/launches/upcoming");

                if (launches == null)
                {
                    return StatusCode(StatusCodes.Status503ServiceUnavailable, "The spacex API seems to not be working");
                }

                var customLaunches = await CombineLaunchData(launches);           

                return Ok(customLaunches);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Past")]
        public async Task<IActionResult> GetPastLaunches()
        {
            try
            {
                List<Launch> launches = await GetDataFromUrl<Launch>("https://api.spacexdata.com/v5/launches/Past");

                if (launches == null)
                {
                    return StatusCode(StatusCodes.Status503ServiceUnavailable, "The spacex API seems to not be working");
                }

                var customLaunches = await CombineLaunchData(launches);

                return Ok(customLaunches);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }        

        public async Task<List<CustomLaunch>> CombineLaunchData(List<Launch> launches)
        {

            //Get all crew,launchpad and rocket data
            var crew = await GetDataFromUrl<Crew>("https://api.spacexdata.com/v4/crew");
            var launchpads = await GetDataFromUrl<Launchpad>("https://api.spacexdata.com/v4/launchpads");
            var rockets = await GetDataFromUrl<Rocket>("https://api.spacexdata.com/v4/rockets");

            List<CustomLaunch> customLaunches = new List<CustomLaunch>();

            foreach (var launch in launches)
            {
                //For each launch, match the crew, launchpad and rocket data.
                List<Crew> customCrew = new List<Crew>();
                foreach (var person in launch.Crew)
                    customCrew.Add(crew.Where(x => x.Id == person.Id).First());

                Launchpad launchPad = launchpads.Where(x => x.Id == launch.Launchpad).First();
                Rocket rocket = rockets.Where(x => x.Id == launch.Rocket).First();

                //Create new Custom launch with data that we're interested in
                CustomLaunch custom = new CustomLaunch
                {
                    FlightNumber = launch.FlightNumber,
                    Name = launch.Name,
                    DateUtc = launch.DateUtc,
                    Rocket = rocket,
                    Success = launch.Success,
                    Details = launch.Details,
                    Links = launch.Links,
                    Crew = customCrew,
                    Launchpad = launchPad,
                    Id = launch.Id

                };
                customLaunches.Add(custom);
            }

            return customLaunches;
        }

        public async Task<List<T>> GetDataFromUrl<T>(string url)
        {
            try
            {    
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
    
                var data = JsonSerializer.Deserialize<List<T>>(content, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                });
    
                return data;
            }catch(Exception ex)
            {
                return null;
            }
        }
    }
}
