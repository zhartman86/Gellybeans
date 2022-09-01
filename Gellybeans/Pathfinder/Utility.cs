namespace Gellybeans.Pathfinder
{
    public class Utility
    {
        public Dictionary<string, string> IncreaseSizeByOne = new Dictionary<string, string>()
        {
            {   "1d1",      "1d2"   },
            {   "1d2",      "1d3"   },
            {   "1d3",      "1d4"   },
            {   "1d4",      "1d6"   },
            {   "1d6",      "1d8"   },
            {   "1d8",      "2d6"   },
            {   "2d4",      "2d6"   },
            {   "1d10",     "2d8"   },
            {   "1d12",     "3d6"   },
            {   "2d6",      "3d6"   },
            {   "3d4",      "3d6"   },
            {   "2d8",      "3d8"   },
            {   "2d10",     "4d8"   },
            {   "3d6",      "4d6"   },
            {   "3d8",      "4d8"   },
            {   "4d6",      "6d6"   },
            {   "4d8",      "6d8"   },
            {   "6d6",      "8d6"   },
            {   "6d8",      "8d8"   },
            {   "8d6",      "12d6"  },
            {   "8d8",      "12d8"  },
            {   "12d6",     "16d6"  },
            {   "12d8",     "16d6"  },
        };

        public Dictionary<string, string> DecreaseSizeByOne = new Dictionary<string, string>()
        {
            {   "1d2",      "1d1"   },
            {   "1d3",      "1d2"   },
            {   "1d4",      "1d3"   },
            {   "1d6",      "1d4"   },
            {   "1d8",      "1d6"   },
            {   "2d4",      "1d6"   },
            {   "1d10",     "1d8"   },
            {   "1d12",     "1d10"  },
            {   "2d6",      "2d4"   },
            {   "2d8",      "1d10"  },
            {   "3d6",      "1d12"  },  
            {   "3d8",      "2d8"   },
            {   "2d12",     "2d10"  },
            {   "4d8",      "2d10"  },
            {   "4d6",      "3d6"   },
            {   "6d6",      "4d6"   },
            {   "6d8",      "4d8"   },
            {   "8d6",      "6d6"   },
            {   "8d8",      "6d8"   },
            {   "12d6",     "8d6"   },
            {   "12d8",     "8d8"   },
            {   "16d6",     "12d6"  },
            {   "16d6",     "12d8"  },
        };

        public Dictionary<int, int> CritRangeDoubler = new Dictionary<int, int>()
        {
            {   20,     19  },
            {   19,     17  },
            {   18,     15  }
        };
    }
}
