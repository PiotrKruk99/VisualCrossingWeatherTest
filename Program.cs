using Newtonsoft.Json;

var apiClient = new HttpClient();

Console.Write("Type access key: ");
var accessKey = Console.ReadLine();

var request = new HttpRequestMessage();
request.Method = HttpMethod.Get;
var uriString = $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/nowa%20wie%C5%9B%20brze%C5%BAnio?unitGroup=metric&include=current&key=";
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