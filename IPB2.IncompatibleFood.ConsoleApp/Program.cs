// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

Console.WriteLine("Hello, World!");

Console.WriteLine("Press Enter to call API...");
Console.ReadLine();

string baseUrl = "https://localhost:7274/api/IncompatibleFood";
HttpClient client = new HttpClient();
var response = await client.GetAsync($"{baseUrl}/List?PageNo=1&PageSize=10");

if (response.IsSuccessStatusCode)
{
    Console.WriteLine("API HttpGet - Success...");

    string json = await response.Content.ReadAsStringAsync();
    Console.WriteLine(json);
}
else
{
    Console.WriteLine("API HttpGet - Fail...");
}

CreateIncompatibleFoodRequest createRequest = new CreateIncompatibleFoodRequest
{
    FoodA = "Milk",
    FoodB = "Lemon",
    Description = "Milk and lemon can curdle when combined."
};

string createRequestJson = JsonConvert.SerializeObject(createRequest);
HttpContent content = new StringContent(createRequestJson, Encoding.UTF8, Application.Json);

response = await client.PostAsync(baseUrl, content);
if (response.IsSuccessStatusCode)
{
    Console.WriteLine("API HttpPost - Success...");

    string json = await response.Content.ReadAsStringAsync();
    Console.WriteLine(json);
}
else
{
    Console.WriteLine("API HttpPost - Fail...");
}

int id = 159;
UpdateIncompatibleFoodRequest updateRequest = new UpdateIncompatibleFoodRequest
{
    FoodA = "Milk 2",
    FoodB = "Lemon 2",
    Description = "Milk and lemon can curdle when combined. 2"
};

string updateRequestJson = JsonConvert.SerializeObject(updateRequest);
content = new StringContent(updateRequestJson, Encoding.UTF8, Application.Json);

response = await client.PutAsync($"{baseUrl}/Update/{id}", content);
if (response.IsSuccessStatusCode)
{
    Console.WriteLine("API HttpPut - Success...");

    string json = await response.Content.ReadAsStringAsync();
    Console.WriteLine(json);
}
else
{
    Console.WriteLine("API HttpPut - Fail...");
}


Console.ReadLine();

public class CreateIncompatibleFoodRequest
{
    public string FoodA { get; set; } = null!;
    public string FoodB { get; set; } = null!;
    public string Description { get; set; } = null!;
}

public class UpdateIncompatibleFoodRequest
{
    public int Id { get; set; }
    public string FoodA { get; set; } = null!;
    public string FoodB { get; set; } = null!;
    public string Description { get; set; } = null!;
}
