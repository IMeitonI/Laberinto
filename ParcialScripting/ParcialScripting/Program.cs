using System;

namespace ParcialScripting {
    class Program {
        static void Main(string[] args) {

            SearchPath search = new SearchPath();
            search.CreateMaze();
            search.BFS();
            search.CreatePath();
            //search.Print();

        }
    }
}
