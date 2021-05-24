using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApiDTO.DataAccess
{
    public static class InitialData
    {
        public static List<TodoItem> GenerateTodoItems()
        {
            var items = new List<TodoItem>();

            items.Add(new TodoItem { Name = "Job",  });
            items.Add(new TodoItem { Name = "Analyze"});
            

            return items;
        }
    }
}
