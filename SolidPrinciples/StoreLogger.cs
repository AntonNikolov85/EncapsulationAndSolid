using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidPrinciples
{
    public class StoreLogger : IStoreLogger, IStoreWriter
    {
        private readonly IStoreWriter writer;

        public StoreLogger(IStoreWriter writer)
        {
            this.writer = writer;
        }

        public void Save(int id, string message)
        {
            Log.Information("Saving message {id}.", id);
            this.writer.Save(id, message);
            Log.Information("Saved message {id}.", id);
        }

        public virtual void Reading(int id)
        {
            Log.Debug("Reading message {id}.", id);
        }

        public virtual void Returning(int id)
        {
            Log.Debug("Returing message {id}.", id);
        }

        public virtual void DidNotFind(int id)
        {
            Log.Debug("No message {id} found.", id);
        }
    }
}
