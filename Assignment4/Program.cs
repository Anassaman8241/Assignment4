namespace Assignment4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string selection = string.Empty;
            List<Client> clients = new();

            LoadClientList(clients);

            Console.WriteLine("/-----------------------------------------/");
            Console.WriteLine("           Personal Training App");
            Console.WriteLine("/-----------------------------------------/\n");

            MainMenu();

            do
            {
                selection = PromptString("\nEnter menu selection: ");

                switch (selection.ToLower())
                {
                    case "l":
                        ClientList(clients);
                        MainMenu();
                        break;
                    case "f":
                        ClientSearch(clients);
                        MainMenu();
                        break;
                    case "n":
                        ClientCreate(clients);
                        MainMenu();
                        break;
                    case "e":
                        ClientEdit(clients);
                        MainMenu();
                        break;
                    case "r":
                        ClientRemove(clients);
                        MainMenu();
                        break;
                    case "s":
                        ShowClientBMIInfo(clients);
                        MainMenu();
                        break;
                    case "q":
                        SaveSalesFile(clients);
                        break;
                }
            } while (selection.ToLower() != "r");

            Console.WriteLine("\nProgram ended");
        }

        static void MainMenu()
        {
            Console.WriteLine("\nMenu Options");
            Console.WriteLine("============");
            Console.WriteLine("[L]ist all clients");
            Console.WriteLine("[F]ind client");
            Console.WriteLine("[N]ew client");
            Console.WriteLine("[E]dit client");
            Console.WriteLine("[R]emove client");
            Console.WriteLine("[S]how client BMI Info");
            Console.WriteLine("[Q]uit");
        }

        static void LoadClientList(List<Client> clients)
        {
            string filename = "client.csv";

            if (File.Exists(filename))
            {
                StreamReader reader = null;

                try
                {
                    reader = new(filename);

                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] list = line.Split(',');

                        if (list.Length == 4)
                        {
                            Client client = new(list[0], list[1], int.Parse(list[2]), int.Parse(list[3]));
                            clients.Add(client);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Writing Error: {ex.Message}");
                }
                finally
                {
                    reader.Close();
                }
            }
            else
            {
                Console.WriteLine($"File {filename} does not exist");
            }
        }

        static void ClientCreate(List<Client> clients)
        {
            bool success;

            do
            {
                try
                {
                    Client newClient = new(
                        PromptString("\nEnter client's first name: "),
                        PromptString("Enter client's last name: "),
                        PromptInt("Enter client's weight in pounds: "),
                        PromptInt("Enter client's height in inches: "));
                    clients.Add(newClient);

                    success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n{ex.Message}");
                    success = false;
                }
            } while (!success);

            Console.WriteLine("\nClient successfully created.");
        }

        static void ClientList(List<Client> clients)
        {
            int id = 1;

            Console.WriteLine("\nClient List");
            Console.WriteLine("===========");

            foreach (var client in clients)
            {
                Console.WriteLine($"[{id}] {client.FullName}");

                id++;
            }
        }

        static void ClientEdit(List<Client> clients)
        {
            string selection = string.Empty;
            int i;

            ClientList(clients);

            do
            {
                try
                {
                    i = PromptInt("\nEnter the Id of the client you want to edit: ");

                    if (i < 1 || i > clients.Count)
                    {
                        Console.WriteLine("\nWrong Id.");
                        i = -1;
                    }
                }
                catch
                {
                    Console.WriteLine("\nWrong Id.");
                    i = -1;
                }
            } while (i < 0);

            Console.WriteLine("\nEdit Client");
            Console.WriteLine("============");
            Console.WriteLine("[F]irst name");
            Console.WriteLine("[L]ast name");
            Console.WriteLine("[H]eight");
            Console.WriteLine("[W]eight");
            Console.WriteLine("[R]eturn");

            do
            {
                try
                {
                    selection = PromptString("\nWhat would you like to edit? ");

                    switch (selection.ToLower())
                    {
                        case "f":
                            clients[i - 1].FirstName = PromptString("\nEnter the new first name: ");

                            break;
                        case "l":
                            clients[i - 1].LastName = PromptString("\nEnter the new last name: ");

                            break;
                        case "h":
                            clients[i - 1].Height = PromptInt("\nEnter the new height: ");

                            break;
                        case "w":
                            clients[i - 1].Weight = PromptInt("\nEnter the new weight: ");

                            break;
                        case "r":
                            Console.WriteLine("\nReturning to the main menu ...");

                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n{ex.Message}");
                }
            } while (selection.ToLower() != "r");
        }

        static string PromptString(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        static int PromptInt(string message)
        {
            Console.Write(message);
            return int.Parse(Console.ReadLine());
        }

        static void ClientSearch(List<Client> clients)
        {
            string searchClient;
            List<Client> results = new();

            Console.WriteLine("\nFind Client");
            Console.WriteLine("=====================");
            searchClient = PromptString("\nEnter client's first name or last name: ");

            foreach (var client in clients)
            {
                if (client.FullName.Contains(searchClient.Trim().ToUpper()))
                {
                    results.Add(client);
                }
            }

            if (results.Count > 0)
            {
                Console.WriteLine("\nFound clients:");

                foreach (var client in results)
                {
                    Console.WriteLine($"\n{client.FullName}");
                }
            }
            else
            {
                Console.WriteLine($"\nNo client found.");
            }
        }

        static void ClientRemove(List<Client> clients)
        {
            int i;

            ClientList(clients);

            do
            {
                try
                {
                    i = PromptInt("\nEnter the Id of the client you want to remove: ");

                    if (i < 1 || i > clients.Count)
                    {
                        Console.WriteLine("\nWrong Id.");
                        i = -1;
                    }
                }
                catch
                {
                    Console.WriteLine("\nWrong Id.");
                    i = -1;
                }
            } while (i < 0);

            clients.RemoveAt(i - 1);
            Console.WriteLine("\nClinet successfully removed.");
        }

        static void ShowClientBMIInfo(List<Client> clients)
        {
            int i;

            ClientList(clients);

            do
            {
                try
                {
                    i = PromptInt("\nEnter the Id of the client you want to remove: ");

                    if (i < 1 || i > clients.Count)
                    {
                        Console.WriteLine("\nTry again.");
                        i = -1;
                    }
                }
                catch
                {
                    Console.WriteLine("\nTry again.");
                    i = -1;
                }
            } while (i < 0);

            Console.WriteLine("\n=== Client BMI Information ===");
            Console.WriteLine($"Client Name: {clients[i - 1].FullName}");
            Console.WriteLine($"BMI Sciore: {clients[i - 1].BMIScore:N2}");
            Console.WriteLine($"BMI Status: {clients[i - 1].BMIStatus}");
        }

        static void SaveSalesFile(List<Client> clients)
        {
            string filename = "ClienList.csv";

            using (StreamWriter writer = new(filename))
            {
                foreach (var client in clients)
                {
                    writer.WriteLine($"{client.FirstName},{client.LastName},{client.Weight},{client.Height}");
                }
            }
        }
    }
}
