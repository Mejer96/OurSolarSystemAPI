namespace OurSolarSystemAPI.Utility {


    public static class UtilityGetRequest 
    {
        public static async Task<string> PerformRequest(string url, HttpClient client)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

    }

}