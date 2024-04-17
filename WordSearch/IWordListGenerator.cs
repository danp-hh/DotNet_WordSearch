using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearch
{
    public interface IWordListGenerator
    {
        public abstract string[] Generate(int dimension);
    }
}
