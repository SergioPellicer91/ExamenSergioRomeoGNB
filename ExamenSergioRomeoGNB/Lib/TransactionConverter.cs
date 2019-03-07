using System.Collections.Generic;
using System.Linq;
using ExamenSergioRomeoGNB.Models;
using QuickGraph;
using System;
using QuickGraph.Algorithms;

namespace ExamenSergioRomeoGNB.Lib
{
    public static class TransactionConverter
    {
        public static AdjacencyGraph<string, Edge<string>> CreateCurrencyGraph(IQueryable<Rate> RateList)
        {
            //Create new Graph
            AdjacencyGraph<string, Edge<string>> graph = new AdjacencyGraph<string, Edge<string>>(true);

            //Add Vertices and Edges
            RateList.ToList().ForEach(
                r =>
                {
                    if (!graph.Vertices.ToList().Contains(r.From))
                    {
                        graph.AddVertex(r.From);
                    }
                    Edge<string> ed = new Edge<string>(r.From, r.To);
                    graph.AddEdge(ed);
                    var kv = new KeyValuePair<string, decimal>(r.From + "-" + r.To, r.RateVal);
                }

            );



            return graph;
        }

        public static IQueryable<Transaction> CalculateAlgorithm(IQueryable<Rate> RateList, IQueryable<Transaction> TransactionList, string target)
        {
            AdjacencyGraph<string, Edge<string>> graph = CreateCurrencyGraph(RateList);
            //Currency values
            var currencyList = new List<KeyValuePair<string, decimal>>();
            RateList.ToList().ForEach(
                r =>
                {
                    var kv = new KeyValuePair<string, decimal>(r.From + "-" + r.To, r.RateVal);
                    currencyList.Add(kv);
                }

            );

            //Add Edge weight (for our currencies, always 1)
            Func<Edge<string>, double> edgeCost = e => 1; // constant cost

            TransactionList.ToList().ForEach(x =>
            {
                // compute shortest paths
                var tryGetPaths = graph.ShortestPathsDijkstra(edgeCost, x.Currency);

                // query path for given vertices

                IEnumerable<Edge<string>> path;
                if (tryGetPaths(target, out path))
                {
                    //currencyList.ForEach(f => Console.WriteLine(f));
                    foreach (var edge in path)
                    {
                        //Console.WriteLine(edge);

                        var search = edge.Source + "-" + edge.Target;
                        var currencyVal = from val in currencyList where val.Key.Equals(search) select val.Value;
                        decimal convertedAmount = x.Amount * currencyVal.FirstOrDefault<decimal>();
                        //Rounding: Half to even
                        convertedAmount = Math.Round(convertedAmount, 2);
                        x.Amount = convertedAmount;
                        x.Currency = target;
                    }

                }
            });


            return TransactionList;



        }
    }
}
