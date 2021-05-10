using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Model
{
    public class CreateTodoItemFailed: Exception
    {
        public long id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public bool IsCompleted
        {
            get;
            set;
        }

        public CreateTodoItemFailed(string message): base(message)
        {

        }
    }
}
