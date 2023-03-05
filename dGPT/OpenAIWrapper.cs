using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;

namespace dGPT
{
    public class OpenAIWrapper
    {
        public static string API_KEY { get; set; }

        public OpenAIWrapper(string apiKey)
        {
            API_KEY = apiKey;
        }

        public class APIRequest
        {
            public APIRequest(string sModel, string sPrompt)
            {
                Prompt = sPrompt;
                Model = sModel;
            }

            public string Prompt { get; set; }
            public string Model { get; set; }
        }

        public string SendRequest(APIRequest APIR)
        {
            System.Net.ServicePointManager.SecurityProtocol =
                System.Net.SecurityProtocolType.Tls12 |
                System.Net.SecurityProtocolType.Tls11 |
                System.Net.SecurityProtocolType.Tls;
            string apiEndpoint = (APIR.Model == "gpt-3.5-turbo" ? "https://api.openai.com/v1/chat/completions" : "https://api.openai.com/v1/completions");
            var request = WebRequest.Create(apiEndpoint);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", "Bearer " + Settings1.Default.APIKey);

            int iMaxTokens = 4000;
            string sModel = APIR.Model;

            double dTemperature = Settings1.Default.temperature;

            string sUserId = "1";

            string data = "{";
            data += " \"model\":\"" + sModel + "\",";
            data += (APIR.Model.Contains("3.") ? (" \"messages\": [{\"role\":\"user\",\"content\":\"" + PadQuotes(APIR.Prompt) + "\"}],") : (" \"prompt\":\"" + PadQuotes(APIR.Prompt) + "\","));
            data += " \"max_tokens\": " + iMaxTokens + ",";
            data += " \"user\": \"" + sUserId + "\", ";
            data += " \"temperature\": " + dTemperature + ", ";
            data += " \"top_p\":" + (Settings1.Default.topProbability / 100) + ", ";
            data += " \"frequency_penalty\": " + Settings1.Default.frequencyPenalty + ", ";
            data += " \"presence_penalty\": " + Settings1.Default.presencePenalty + "}";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(data);
                streamWriter.Flush();
                streamWriter.Close();
            }

            string sJson;
            string respS = "[ERROR]";
            try
            {
                using (WebResponse response = (WebResponse)request.GetResponse())
                {
                    var streamReader = new StreamReader(response.GetResponseStream());
                    sJson = streamReader.ReadToEnd();
                }
                JObject JO = JObject.Parse(sJson);
                respS = (APIR.Model.Contains("3.5") ? ((string)JO.SelectToken("choices[0].message.content")).Trim() : ((string)JO.SelectToken("choices[0].text")).Trim());
            }
            catch (Exception e) { }
            return respS;
        }

        private string PadQuotes(string s)
        {
            if (s.IndexOf("\\") != -1)
                s = s.Replace("\\", @"\\");

            if (s.IndexOf("\n\r") != -1)
                s = s.Replace("\n\r", @"\n");

            if (s.IndexOf("\r") != -1)
                s = s.Replace("\r", @"\r");

            if (s.IndexOf("\n") != -1)
                s = s.Replace("\n", @"\n");

            if (s.IndexOf("\t") != -1)
                s = s.Replace("\t", @"\t");

            if (s.IndexOf("\"") != -1)
                return s.Replace("\"", @"""");
            else
                return s;
        }
    }
}