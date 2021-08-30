using System;
using System.Collections.Generic;
using System.Text;

namespace ParcialScripting {
    class SearchPath {
        //struct Vector2 {
        //    public int x;
        //    public int y;

        //    public Vector2(int x, int y) {
        //        this.x = x;
        //        this.y = y;
        //    }
        //}
        class Node {
            public string name;
            public bool isExplored = false;
            public Node isExploredFrom;
            public int[] pos;

            public Node(string name) {
                this.name = name;
            }

            public int[] SetPos(int[] pos) {
                this.pos = pos;
                return pos;
            }
            public int[] GetPos() {

                return pos;
            }
        }
        

        private Node _startingPoint;
        private Node _endingPoint ;


        private Dictionary<int[], Node> _block = new Dictionary<int[], Node>();                           // For storing all the nodes with Node.cs
        private int[,] _directions = { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } };    // Directions to search in BFS
        private Queue<Node> _queue = new Queue<Node>();         // Queue for enqueueing nodes and traversing through them
        private Node _searchingPoint;                           // Current node we are searching
        private bool _isExploring = true;                       // If we are end then it is set to false

        private List<Node> _path = new List<Node>();            // For storing the path traversed
       

        public void CreateMaze() {
            var count = 0;
            string[,] maze = {
                { "X"," 0"," X"," 0"," X" },
                { "0"," 0"," 0"," 0"," 0" },
                { "0"," 0"," 0"," 0"," 0" },
                { "0"," X"," 0"," 0"," X" },
                { "0"," 0"," 0"," 0"," 0" } };

            foreach (var item in maze) {
                count += 1;
                Console.Write(item);
                if (count % 5 == 0) Console.WriteLine("");
            }


            for (int i = 0; i < 5; i++) {
                for (int t = 0; t < 5; t++) {
                    Node nodo = new Node("nodo:" + i + t);
                    int[] pos = new int[] { i, t };
                   _block.Add(pos,nodo );
                    nodo.SetPos(pos);
                    
                }
            }
            int count2 = 0;
            foreach (KeyValuePair<int[], Node> author in _block) {
                //Console.WriteLine("Key: {0},{1} , Value: {2}",
                  //  author.Key[0], author.Key[1], author.Value.name);
               
                if (count2 == 1) {
                   
                    _startingPoint = author.Value;
                    Console.WriteLine("Punto inicial:" + _startingPoint.pos[0] + "," + _startingPoint.pos[1]);
                } else if(count2 ==15){
                    _endingPoint = author.Value;
                    Console.WriteLine("Punto final:" + _endingPoint.pos[0] + "," + _endingPoint.pos[1]);

                }
                count2 += 1;
            }

        }




        // BFS; For finding the shortest path
        public void BFS() {
            _queue.Enqueue(_startingPoint);
            while (_queue.Count > 0 && _isExploring) {
                _searchingPoint = _queue.Dequeue();
                OnReachingEnd();
                ExploreNeighbourNodes();
            }
        }


        // To check if we've reached the Ending point
         void OnReachingEnd() {
            if (_searchingPoint == _endingPoint) {

                _isExploring = false;
            } else {
                _isExploring = true;
            }
        }


        // Searching the neighbouring nodes
        private void ExploreNeighbourNodes() {
            if (!_isExploring) { return; }

            for (int i = 0; i < 4; i++) {

                int[] neighbourPos = { _searchingPoint.pos[0] + _directions[i,0],
                _searchingPoint.pos[1] + _directions[i,1] };
                Console.WriteLine(neighbourPos[0] + neighbourPos[1]);
                if (_block.ContainsKey(neighbourPos))               // If the explore neighbour is present in the dictionary _block, which contians all the blocks with Node.cs attached
                {
                    Node node = _block[neighbourPos];

                    if (!node.isExplored) {
                        _queue.Enqueue(node);                       // Enqueueing the node at this position
                        node.isExplored = true;

                        node.isExploredFrom = _searchingPoint;      // Set how we reached the neighbouring node i.e the previous node; for getting the path
                    }
                }
            }
                
            
        }


        // Creating path using the isExploredFrom var of each node to get the previous node from where we got to this node
        public void CreatePath() {
            SetPath(_endingPoint);
            Node previousNode = _endingPoint.isExploredFrom;

            while (previousNode != _startingPoint) {
                SetPath(previousNode);
                previousNode = previousNode.isExploredFrom;
            }

            SetPath(_startingPoint);
            _path.Reverse();
           
        }

        // For adding nodes to the path
        private void SetPath(Node node) {
            _path.Add(node);
        }

        

    }
}
