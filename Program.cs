using Newtonsoft.Json;

var apiClient = new HttpClient();
bool goAnother = true;

Console.Write("Type access key: ");
var accessKey = Console.ReadLine();

while (goAnother)
{
    var request = new HttpRequestMessage();
    request.Method = HttpMethod.Get;

    Console.Write("Type city: ");
    var cityName = Console.ReadLine();

    var uriString = $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/";
    uriString += cityName;
    uriString += $"?unitGroup=metric&include=current&key=";
    uriString += accessKey;
    uriString += $"&contentType=json";
    request.RequestUri = new Uri(uriString);
    var response = await apiClient.SendAsync(request);

    if (response.IsSuccessStatusCode)
    {
        var body = await response.Content.ReadAsStringAsync();
        //Console.WriteLine(body);
        dynamic weather = JsonConvert.DeserializeObject(body);

        string output = "Actual tepmerature in ";
        output += weather.resolvedAddress;
        output += " is ";
        output += weather.currentConditions.temp;
        output += " and humidity is ";
        output += weather.currentConditions.humidity;

        Console.WriteLine(output);
    }
    else
    {
        Console.WriteLine("Error while sending request");
    }

    Console.WriteLine("Check another city? (y/n)");
    char choice;
    char[] validChoice = { 'y', 'Y', 'n', 'N' };

    do
    {
        choice = Console.ReadKey(true).KeyChar;
    }
    while (!Array.Exists<char>(validChoice, x => x == choice));

    if (choice != 'Y' && choice != 'y')
        goAnother = false;
}