using System.Net.Http.Json;
using ServiceRequestApp.Maui.Models;

namespace ServiceRequestApp.Maui;

public partial class MainPage : ContentPage {
   private readonly HttpClient _httpClient;
   // For Android/iOS emulator use this one
   //private const string ApiBase = "http://10.0.2.2:5000";
   
   // For Windows version use this one
   private const string ApiBase = "http://localhost:5000";

   public MainPage()  {
    	InitializeComponent();
    	_httpClient = new HttpClient  {
        BaseAddress = new Uri(ApiBase + "/")
     };
   }
   private async void OnLoadClicked(object? sender, EventArgs e)  {
     	try  {
          	StatusLabel.Text = "Loading...";
         	var requests = await _httpClient.GetFromJsonAsync<List<ServiceRequestDto>>("api/servicerequests");
         	RequestsCollection.ItemsSource = requests;
         	StatusLabel.Text = $"Loaded {requests?.Count ?? 0} record(s).";
     	} catch (Exception ex) { StatusLabel.Text = $"Error: {ex.Message}"; }
  }
   private async void OnCreateClicked(object? sender, EventArgs e)  {
         var title = TitleEntry.Text?.Trim();
         var description = DescriptionEntry.Text?.Trim();
         if (string.IsNullOrWhiteSpace(title))  {
            StatusLabel.Text = "Title is required."; 
            return;
         }
        try  {
           StatusLabel.Text = "Creating...";
           var newRequest = new ServiceRequestDto  {
        	   Title = title, Description = description ?? ""
           };
           var response = await _httpClient.PostAsJsonAsync("api/servicerequests", newRequest);
           if (response.IsSuccessStatusCode)  {
 				TitleEntry.Text = ""; DescriptionEntry.Text = "";
 				StatusLabel.Text = "Created successfully. Reloading...";
 				await Task.Delay(500);
 				await LoadRequestsAsync();
           } else { 
				StatusLabel.Text = $"API error: {(int)response.StatusCode}"; 
		   }
        }catch (Exception ex) { 
			StatusLabel.Text = $"Error: {ex.Message}"; 
		}
  }

  private async Task LoadRequestsAsync()  {
     var requests = await _httpClient.GetFromJsonAsync<List<ServiceRequestDto>>("api/servicerequests");
     RequestsCollection.ItemsSource = requests;
     StatusLabel.Text = $"Loaded {requests?.Count ?? 0} record(s).";
  }
}
