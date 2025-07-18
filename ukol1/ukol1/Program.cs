using System.Net;
using System.Net.Mail;
using System.Text.Json;


namespace ukol1
{
    class Program
    {
        public static List<Flag> ReadCsv(string filePath)
        {
            List<Flag> flagList = new();
            FileInfo f = new(filePath);
            if (f.Exists)
            {
                try
                {
                    StreamReader sr = f.OpenText();
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        string[] columns = s.Split(',');
                        if (columns.Length == 30)
                        {
                            Flag flag = new();
                            var properties = typeof(Flag).GetProperties();

                            for (int i = 0; i < 30; i++)
                            {
                                var property = properties[i];
                                var columnValue = columns[i];

                                var convertedValue = Convert.ChangeType(columnValue, property.PropertyType);
                                property.SetValue(flag, convertedValue);
                            }
                            flagList.Add(flag);
                        }
                        else
                        {
                            Console.WriteLine("Wrong number of values in row: " + s);
                            sr.Close();
                        }
                    }
                    sr.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error while reading file: " + ex.Message);
                };
            }
            else
            {
                Console.WriteLine("Could not read file, file does not exist or not found");
            }

            return flagList;
        }

        static async Task DownloadCsv(string resource, string fileName)
        {
            using (HttpClient httpClient = new())
            {
                HttpResponseMessage response = await httpClient.GetAsync(resource);

                if (response.IsSuccessStatusCode)
                {
                    byte[] fileBytes = await response.Content.ReadAsByteArrayAsync();
                    File.WriteAllBytes(fileName, fileBytes);
                }
                else
                {
                    throw new HttpRequestException($"Failed to download csv file. Status code: {response.StatusCode}");
                }
            }
        }

        static async Task Main()
        {
            // Download csv file
            string fileName = "csv";
            string resource = "https://archive.ics.uci.edu/ml/machine-learning-databases/flags/flag.data";

            Console.WriteLine("Downloading csv file");

            try
            {
                await DownloadCsv(resource, fileName);
                Console.WriteLine("Downloaded csv file");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            // Read csv file
            List<Flag> flagList = ReadCsv(fileName);

            // Restrict
            List<Flag> countriesList = flagList
                .Where(flag => flag.Language == 1 && flag.Blue == 1 && flag.SunStars > 0)
                .ToList();

            // Order
            List<Flag> orderedCountriesList = countriesList.OrderBy(flag => flag.SunStars).ToList();

            Console.WriteLine("Ordered english speaking countries with blue color in flag:");
            foreach (var flag in orderedCountriesList)
            {
                Console.WriteLine($"Country: {flag.Name} Stars: {flag.SunStars}");
            }

            // Select data for JSON file
            var jsonData = orderedCountriesList
                .Select(flag => new {flag.Name, flag.SunStars, flag.Colours, flag.Population});

            // Create JSON file
            string filePath = "results.json";

            try
            {
                string json = JsonSerializer.Serialize(jsonData);    
                File.WriteAllText(filePath, json);
                Console.WriteLine($"Data saved to JSON file");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            // Send JSON file
            string sender = "blabla@upol.cz";
            string senderPassword = "somepsswd";
            string recipient = "radek.janostik@upol.cz";

            string subject = "PNE -- výsledky -- kaczova";
            string body = "Programoval nekdo v F# aniz bych ho videl?";

            MailMessage mail = new(sender, recipient)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            };

            Attachment attachment = new(filePath, "application/json");
            mail.Attachments.Add(attachment);

            SmtpClient smtpClient = new("smtp.office365.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(sender, senderPassword),
                EnableSsl = true
            };

            try
            {
                smtpClient.Send(mail);
                Console.WriteLine("Email sent");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                mail.Dispose();
                attachment.Dispose();
            }
        }
    }
}