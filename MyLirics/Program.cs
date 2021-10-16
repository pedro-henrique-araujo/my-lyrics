using MyLyrics.Logic;
using MyLyrics.Logic.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLyrics
{
    class Program
    {

        static MyLyricsLogic logic;

        static void Main()
        {
            RunAsync().GetAwaiter().GetResult();
        }


        static async Task RunAsync()
        {
            SetUp();
            List<SearchDocument> documents = await AskForSearchAsync();
            bool thereAreResults = CheckIfThereAreResultsAndDisplayThem(documents);
            if (!thereAreResults)
                return;

            string chosenOption = AskForOption(documents);

            (bool valid, string errorMessage, int noSignIndex) validationResult = ValidateOption(chosenOption, documents);


            await DisplayLyricis(documents, validationResult.noSignIndex);

        }

        private static async Task DisplayLyricis(List<SearchDocument> documents, int noSignIndex)
        {
            SearchDocument chosenDocument = documents[noSignIndex]; 
            Song song = await logic.GetLyricsAsync(chosenDocument);
            Console.WriteLine("Generating file");
            logic.GenerateDocument(song);
            Console.WriteLine("File generated");
        }

        private static (bool valid, string errorMessage, int noSignIndex) ValidateOption(string chosenOption, List<SearchDocument> documents)
        {
            bool successfulConversion = int.TryParse(chosenOption, out int chosenOptionIndex);

            if (!successfulConversion)
            {
                string message = "Invalid input, insert a number";
                return (false, message, 0);
            }

            int noSignIndex = Math.Abs(chosenOptionIndex);
            if (noSignIndex >= documents.Count)
            {
                string message = "Invalid option";
                return (false, message, 0);
            }

            return (true, string.Empty, noSignIndex);
        }

        private static string AskForOption(List<SearchDocument> documents)
        {
            Console.WriteLine("Choose one option...");

            for (int i = 0; i < documents.Count; i++)
            {
                SearchDocument document = documents[i];
                Console.WriteLine($"{i} - {document.Title} - {document.Band}");
            }

            string chosenOption = Console.ReadLine();
            return chosenOption;
        }

        private static bool CheckIfThereAreResultsAndDisplayThem(List<SearchDocument> documents)
        {
            bool thereAreResults = documents.Count > 0;
            Console.WriteLine("Number of results: " + documents.Count);
            return thereAreResults;
        }

        private static async Task<List<SearchDocument>> AskForSearchAsync()
        {
            Console.Write("Search: ");
            string searchTerm = Console.ReadLine();
            List<SearchDocument> documents = await logic.SearchSongsAsync(searchTerm);
            return documents;
        }

        private static void SetUp()
        {
            logic = new MyLyricsLogic();
        }
    }
}