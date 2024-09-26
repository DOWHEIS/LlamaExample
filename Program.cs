using System;
using System.Diagnostics;

namespace LlamaExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Sample prompt with fake data
            const string prompt = @"
                Summarize the asset price change for AAPL:
                The price changed from 150 to 155 (a 3.3% change) on 2024-09-17.

                Related news: 
                Title: Apple Releases New iPhone Model Amidst Strong Market Demand
                Summary: Apple's stock rose after the release of their latest iPhone model, which saw strong initial sales and positive reviews. This, coupled with a favorable retail environment, has driven the stock price higher.
                Published by TechNews on 2024-09-17.

                Title: Global Semiconductor Shortage Eases, Benefiting Apple Suppliers
                Summary: A reduction in the global semiconductor shortage has led to better supply chain performance for Apple’s hardware production, contributing to increased investor confidence.
                Published by MarketWatch on 2024-09-16.

                Title: Apple's Stock Benefits from Strong Retail Sales Data in the U.S.
                Summary: Strong U.S. retail sales data showed increased consumer spending, especially in technology products, which pushed Apple’s stock price higher.
                Published by Reuters on 2024-09-15.

                In a single summary, explain why Apple's stock price increased on 2024-09-17.";

            // Setup Python process
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "/app/venv/bin/python3", //venv python path
                Arguments = $"llama_script.py \"{prompt}\"",
                RedirectStandardOutput = true,  //get output
                RedirectStandardError = true,   //get errors
                UseShellExecute = false,
                CreateNoWindow = true
            };

            try
            {
                using (Process process = Process.Start(psi))
                {
                    if (process == null) return;

                    var result = process.StandardOutput.ReadToEnd();
                    var error = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    Console.WriteLine("LLaMA Response (Standard Output): " + result);
                    Console.WriteLine("LLaMA Error (Standard Error): " + error);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
    }
}