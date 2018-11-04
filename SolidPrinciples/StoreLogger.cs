using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidPrinciples
{
    public class StoreLogger : IStoreWriter, IStoreReader
    {
        private readonly IStoreWriter writer;
        private readonly IStoreReader reader;

        public StoreLogger(IStoreWriter writer, IStoreReader reader)
        {
            this.writer = writer;
            this.reader = reader;
        }

        public void Save(int id, string message)
        {
            Log.Information("Saving message {id}.", id);
            this.writer.Save(id, message);
            Log.Information("Saved message {id}.", id);
        }

        public Maybe<string> Read(int id)
        {
            Log.Debug("Reading message {id}.", id);
            Maybe<string> returnValue = this.reader.Read(id);
            if (returnValue.Any())
            {
                Log.Debug("Returing message {id}.", id);
            }
            else
            {
                Log.Debug("No message {id} found.", id);
            }

            return returnValue;
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
