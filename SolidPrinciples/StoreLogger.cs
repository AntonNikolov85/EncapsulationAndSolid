using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidPrinciples
{
    public class StoreLogger
    {
        public void Saving(int id)
        {
            Log.Information("Saving message {id}.", id);
        }

        public void Saved(int id)
        {
            Log.Information("Saved message {id}.", id);
        }

        public void Reading(int id)
        {
            Log.Debug("Reading message {id}.", id);
        }

        public void Returning(int id)
        {
            Log.Debug("Returing message {id}.", id);
        }

        public void DidNotFind(int id)
        {
            Log.Debug("No message {id} found.", id);
        }
    }
}
