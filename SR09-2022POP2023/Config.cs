using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR09_2022POP2023
{
    public class Config
    {
        public static string CONNECTION_STRING { get; } = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=POPSR9/2022;Integrated Security=True;Connect Timeout=30;Encrypt=False";
    }
}
