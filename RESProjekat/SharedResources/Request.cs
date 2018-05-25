﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources
{
    public class Request : IRequest
    {
        public string Verb { get; set; }

        public string Noun { get; set; }

        public string Query { get; set; }

        public string Fields { get; set; }

        public Request()
        {

        }
    }
}