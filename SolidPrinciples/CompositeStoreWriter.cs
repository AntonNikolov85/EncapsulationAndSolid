using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidPrinciples
{
    public class CompositeStoreWriter : IStoreWriter
    {
        private readonly IStoreWriter[] writers;

        public CompositeStoreWriter(params IStoreWriter[] writers)
        {
            this.writers = writers;
        }

        public void Save(int id, string message)
        {
            foreach (var w in this.writers)
            {
                w.Save(id, message);
            }
        }
    }
}
