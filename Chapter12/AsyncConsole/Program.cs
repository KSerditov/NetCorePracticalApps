HttpClient httpClient = new();
HttpResponseMessage responseMessage = await httpClient.GetAsync("https://apple.com");
Console.WriteLine(responseMessage.Content.Headers.ContentLength);